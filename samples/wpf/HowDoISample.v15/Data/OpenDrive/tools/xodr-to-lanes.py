"""ASAM OpenDRIVE (.xodr) -> lane GeoJSON + a route for the demo.

OpenDRIVE describes a road by a reference line (lines, arcs, spirals, cubic
and parametric-cubic pieces) and lanes as width polynomials stacked outward
from it, with the paint on each lane's outer edge. This evaluates that model
into plain polylines and polygons: every lane as a polygon, every road mark
as a line with its type, junction connecting roads as intersections, and one
drive threaded through the lane links.

Output uses the same layers and files as av2-to-lanes.py, so the demo reads
both with one code path:

  <Name>Lanes.geojson   lane / drivable / lane_edge / lane_marking_* / crossing
  <Name>Route.json      points [lon, lat, laneOrdinal], lanes [{id,index,count,intersection}]

Usage:
  python tools\\xodr-to-lanes.py <file.xodr> <out-dir> <Name> [--step 1.0] [--start ROADID]

Tested on the DLR / 3D Mapping Solutions Brunswick map (OpenDRIVE 1.5,
CC BY 4.0): https://doi.org/10.5281/zenodo.7071846
"""
import json
import math
import os
import sys
import xml.etree.ElementTree as ET

from pyproj import Transformer
from shapely.geometry import Polygon
from shapely.ops import unary_union
from shapely.strtree import STRtree

LAYER_FOR_MARK = {
    ("solid", "standard"): "lane_marking_solid",
    ("solid", "white"): "lane_marking_solid",
    ("solid solid", "standard"): "lane_marking_solid",
    ("solid solid", "white"): "lane_marking_solid",
    ("solid broken", "standard"): "lane_marking_solid",
    ("broken solid", "standard"): "lane_marking_solid",
    ("broken", "standard"): "lane_marking_dashed",
    ("broken", "white"): "lane_marking_dashed",
    ("broken broken", "standard"): "lane_marking_dashed",
    ("solid", "yellow"): "lane_marking_yellow",
    ("solid solid", "yellow"): "lane_marking_yellow_double",
    ("solid broken", "yellow"): "lane_marking_yellow",
    ("broken solid", "yellow"): "lane_marking_yellow",
    ("broken", "yellow"): "lane_marking_yellow_dashed",
    ("curb", "standard"): "lane_edge",
    ("edge", "standard"): "lane_edge",
    ("grass", "standard"): "lane_edge",
    ("botts dots", "standard"): "lane_marking_dashed",
}
ASPHALT = {"driving", "bidirectional", "roadWorks", "biking", "parking", "shoulder", "restricted", "tram", "entry", "exit", "onRamp", "offRamp", "stop", "mwyEntry", "mwyExit", "bus", "taxi"}
DRIVING = {"driving", "bidirectional", "roadWorks", "entry", "exit", "onRamp", "offRamp", "mwyEntry", "mwyExit"}


def f(node, name, default=0.0):
    v = node.get(name)
    return float(v) if v is not None else default


def poly(a, b, c, d, x):
    return a + b * x + c * x * x + d * x * x * x


def dpoly(b, c, d, x):
    return b + 2 * c * x + 3 * d * x * x


class Road:
    """The reference line sampled at fixed spacing, with everything else
    evaluated by interpolating along it."""

    def __init__(self, node, step):
        self.id = node.get("id")
        self.length = f(node, "length")
        self.junction = node.get("junction", "-1")
        self.node = node
        self.samples = []  # (s, x, y, hdg)
        for g in node.find("planView").findall("geometry"):
            self._sample_geometry(g, step)
        # Always end exactly at the road length.
        last = node.find("planView").findall("geometry")[-1]
        self._append(last, f(last, "length"))
        self.lane_offsets = [(f(o, "s"), f(o, "a"), f(o, "b"), f(o, "c"), f(o, "d")) for o in node.findall("lanes/laneOffset")]
        self.elevations = [(f(e, "s"), f(e, "a"), f(e, "b"), f(e, "c"), f(e, "d")) for e in node.findall("elevationProfile/elevation")]
        self.sections = node.findall("lanes/laneSection")
        self.section_starts = [f(sec, "s") for sec in self.sections]
        link = node.find("link")
        self.pred = link.find("predecessor").attrib if link is not None and link.find("predecessor") is not None else None
        self.succ = link.find("successor").attrib if link is not None and link.find("successor") is not None else None

    def _sample_geometry(self, g, step):
        length = f(g, "length")
        n = max(1, int(math.ceil(length / step)))
        for i in range(n):
            self._append(g, length * i / n)

    def _append(self, g, ds):
        s0, x0, y0, h0 = f(g, "s"), f(g, "x"), f(g, "y"), f(g, "hdg")
        length = f(g, "length")
        kind = g[0]
        if kind.tag == "line":
            x, y, h = x0 + ds * math.cos(h0), y0 + ds * math.sin(h0), h0
        elif kind.tag == "arc":
            k = f(kind, "curvature")
            h = h0 + k * ds
            if abs(k) < 1e-9:
                x, y = x0 + ds * math.cos(h0), y0 + ds * math.sin(h0)
            else:
                x = x0 + (math.sin(h) - math.sin(h0)) / k
                y = y0 - (math.cos(h) - math.cos(h0)) / k
        elif kind.tag == "spiral":
            k0, k1 = f(kind, "curvStart"), f(kind, "curvEnd")
            # Heading is quadratic in s; integrate numerically - exact enough at
            # 0.1 m and free of Fresnel special functions.
            x, y, h = x0, y0, h0
            steps = max(1, int(ds / 0.1))
            for i in range(steps):
                sa = ds * i / steps
                sb = ds * (i + 1) / steps
                ha = h0 + k0 * sa + (k1 - k0) * sa * sa / (2 * length)
                hb = h0 + k0 * sb + (k1 - k0) * sb * sb / (2 * length)
                hm = (ha + hb) / 2
                x += (sb - sa) * math.cos(hm)
                y += (sb - sa) * math.sin(hm)
                h = hb
        elif kind.tag == "poly3":
            a, b, c, d = f(kind, "a"), f(kind, "b"), f(kind, "c"), f(kind, "d")
            u, v = ds, poly(a, b, c, d, ds)
            dv = dpoly(b, c, d, ds)
            x = x0 + u * math.cos(h0) - v * math.sin(h0)
            y = y0 + u * math.sin(h0) + v * math.cos(h0)
            h = h0 + math.atan2(dv, 1.0)
        elif kind.tag == "paramPoly3":
            p = ds if kind.get("pRange", "normalized") == "arcLength" else (ds / length if length > 0 else 0.0)
            au, bu, cu, du = f(kind, "aU"), f(kind, "bU"), f(kind, "cU"), f(kind, "dU")
            av, bv, cv, dv = f(kind, "aV"), f(kind, "bV"), f(kind, "cV"), f(kind, "dV")
            u, v = poly(au, bu, cu, du, p), poly(av, bv, cv, dv, p)
            tu, tv = dpoly(bu, cu, du, p), dpoly(bv, cv, dv, p)
            x = x0 + u * math.cos(h0) - v * math.sin(h0)
            y = y0 + u * math.sin(h0) + v * math.cos(h0)
            h = h0 + math.atan2(tv, tu)
        else:
            raise ValueError("geometry " + kind.tag)
        self.samples.append((s0 + ds, x, y, h))

    def elevation(self, s):
        cur = None
        for e in self.elevations:
            if e[0] <= s + 1e-9:
                cur = e
            else:
                break
        return 0.0 if cur is None else poly(cur[1], cur[2], cur[3], cur[4], s - cur[0])

    def nearest_s(self, x, y):
        best, best_s = float("inf"), 0.0
        for s, sx, sy, _ in self.samples:
            d = (sx - x) * (sx - x) + (sy - y) * (sy - y)
            if d < best:
                best, best_s = d, s
        return best_s

    def lane_offset(self, s):
        cur = (0.0, 0.0, 0.0, 0.0, 0.0)
        for o in self.lane_offsets:
            if o[0] <= s + 1e-9:
                cur = o
            else:
                break
        return poly(cur[1], cur[2], cur[3], cur[4], s - cur[0])

    def section_index(self, s):
        idx = 0
        for i, start in enumerate(self.section_starts):
            if start <= s + 1e-9:
                idx = i
        return idx

    def section_range(self, i):
        start = self.section_starts[i]
        end = self.section_starts[i + 1] if i + 1 < len(self.section_starts) else self.length
        return start, end

    def samples_in(self, start, end):
        out = [sm for sm in self.samples if start - 1e-9 <= sm[0] <= end + 1e-9]
        # Guarantee both ends so short sections still get a polygon.
        for edge in (start, end):
            if not out or abs(out[0][0] - edge) > 1e-6 and abs(out[-1][0] - edge) > 1e-6:
                out.append(self.interpolate(edge))
        out.sort(key=lambda sm: sm[0])
        return out

    def interpolate(self, s):
        sm = self.samples
        if s <= sm[0][0]:
            return sm[0]
        for i in range(1, len(sm)):
            if sm[i][0] >= s:
                a, b = sm[i - 1], sm[i]
                span = (b[0] - a[0]) or 1e-9
                u = (s - a[0]) / span
                return (s, a[1] + (b[1] - a[1]) * u, a[2] + (b[2] - a[2]) * u, a[3] + (b[3] - a[3]) * u)
        return sm[-1]


def width_at(lane, ds):
    cur = None
    for w in lane.findall("width"):
        if f(w, "sOffset") <= ds + 1e-9:
            cur = w
        else:
            break
    if cur is None:
        return 0.0
    return max(0.0, poly(f(cur, "a"), f(cur, "b"), f(cur, "c"), f(cur, "d"), ds - f(cur, "sOffset")))


def offset_point(sample, t):
    s, x, y, h = sample
    return (x - t * math.sin(h), y + t * math.cos(h))


def simplify(points, tolerance):
    """Douglas-Peucker on (x, y) tuples."""
    if len(points) < 3:
        return points
    ax, ay = points[0]
    bx, by = points[-1]
    dx, dy = bx - ax, by - ay
    norm = math.hypot(dx, dy) or 1e-12
    best, index = 0.0, 0
    for i in range(1, len(points) - 1):
        px, py = points[i]
        dist = abs(dy * px - dx * py + bx * ay - by * ax) / norm
        if dist > best:
            best, index = dist, i
    if best > tolerance:
        return simplify(points[:index + 1], tolerance)[:-1] + simplify(points[index:], tolerance)
    return [points[0], points[-1]]


def main():
    args = [a for a in sys.argv[1:] if not a.startswith("--")]
    path, out_dir, name = args[0], args[1], args[2]
    step = float(sys.argv[sys.argv.index("--step") + 1]) if "--step" in sys.argv else 1.0
    start_override = sys.argv[sys.argv.index("--start") + 1] if "--start" in sys.argv else None

    root = ET.parse(path).getroot()
    header = root.find("header")
    geo = header.find("geoReference")
    proj = (geo.text or "").strip() if geo is not None else None
    if not proj:
        raise SystemExit("no <geoReference> in header - the map is not georeferenced")
    # Only the horizontal datum matters here; a vertical geoid grid named in
    # the header (RoadRunner writes +geoidgrids=egm96_15.gtx) is usually not
    # installed and would make PROJ refuse the whole definition.
    proj = " ".join(p for p in proj.split() if not p.startswith(("+geoidgrids=", "+vunits=", "+geoid_crs=")))
    to_wgs84 = Transformer.from_proj(proj, "EPSG:4326", always_xy=True)

    def lonlat(pt):
        lon, lat = to_wgs84.transform(pt[0], pt[1])
        return [round(lon, 7), round(lat, 7)]

    roads = {}
    for node in root.findall("road"):
        roads[node.get("id")] = Road(node, step)
    junctions = {j.get("id"): j for j in root.findall("junction")}
    print(f"{len(roads)} roads, {len(junctions)} junctions, {sum(r.length for r in roads.values())/1000:.1f} km", flush=True)

    features = []
    counts = {}
    # Lane geometry, keyed by (road, section, lane id): inner/outer boundaries
    # in s order, so the route can reuse them for centrelines.
    lane_geom = {}
    lane_type = {}

    # Which road section each feature belongs to, so the vertical level worked
    # out below can be stamped on it afterwards.
    feature_section = []
    current_section = [None]
    section_polys = {}   # (road id, section index) -> shapely polygon of its asphalt, metres
    section_span = {}    # (road id, section index) -> (s start, s end)

    def emit(layer, geometry_type, coords, props):
        props = dict(props, layer=layer)
        features.append({"type": "Feature", "properties": props, "geometry": {"type": geometry_type, "coordinates": coords}})
        feature_section.append(current_section[0])
        counts[layer] = counts.get(layer, 0) + 1

    for road in roads.values():
        is_junction = road.junction != "-1"
        for si, sec in enumerate(road.sections):
            start, end = road.section_range(si)
            samples = road.samples_in(start, end)
            if len(samples) < 2:
                continue
            current_section[0] = (road.id, si)
            section_span[(road.id, si)] = (start, end)
            asphalt_rings = []
            centre = [offset_point(sm, road.lane_offset(sm[0])) for sm in samples]
            # Centre lane paint (the line between the two directions).
            centre_lane = sec.find("center/lane")
            if centre_lane is not None:
                emit_marks(centre_lane, samples, [road.lane_offset(sm[0]) for sm in samples], start, is_junction, emit, lonlat)

            for side, sign in (("left", 1.0), ("right", -1.0)):
                side_node = sec.find(side)
                if side_node is None:
                    continue
                lanes = sorted(side_node.findall("lane"), key=lambda l: abs(int(l.get("id"))))
                inner_t = [road.lane_offset(sm[0]) for sm in samples]
                for lane in lanes:
                    lid = int(lane.get("id"))
                    ltype = lane.get("type")
                    outer_t = [inner_t[i] + sign * width_at(lane, sm[0] - start) for i, sm in enumerate(samples)]
                    inner_pts = [offset_point(sm, inner_t[i]) for i, sm in enumerate(samples)]
                    outer_pts = [offset_point(sm, outer_t[i]) for i, sm in enumerate(samples)]
                    key = (road.id, si, lid)
                    lane_geom[key] = (inner_pts, outer_pts)
                    lane_type[key] = ltype
                    if ltype in ASPHALT:
                        ring = simplify(inner_pts, 0.03) + simplify(outer_pts, 0.03)[::-1]
                        asphalt_rings.append(ring)
                        ring = [lonlat(p) for p in ring]
                        ring.append(ring[0])
                        if ltype in DRIVING:
                            emit("lane", "Polygon", [ring], {"id": f"{road.id}/{si}/{lid}", "type": ltype, "intersection": is_junction})
                        else:
                            emit("drivable", "Polygon", [ring], {"id": f"{road.id}/{si}/{lid}", "type": ltype})
                    emit_marks(lane, samples, outer_t, start, is_junction, emit, lonlat)
                    inner_t = outer_t

            if asphalt_rings:
                polys = [Polygon(r).buffer(0) for r in asphalt_rings if len(r) >= 3]
                merged = unary_union([p for p in polys if not p.is_empty])
                if not merged.is_empty:
                    section_polys[(road.id, si)] = merged

    current_section[0] = None

    # Road-mark objects (stop lines, arrows, zebra stripes) with outlines.
    for road in roads.values():
        for obj in road.node.findall("objects/object"):
            if obj.get("type") != "roadMark":
                continue
            outline = obj.find("outline")
            if outline is None:
                continue
            s0, t0, hdg = f(obj, "s"), f(obj, "t"), f(obj, "hdg")
            base = road.interpolate(s0)
            ring = []
            for corner in outline:
                if corner.tag == "cornerRoad":
                    ring.append(offset_point(road.interpolate(f(corner, "s")), f(corner, "t")))
                elif corner.tag == "cornerLocal":
                    u, v = f(corner, "u"), f(corner, "v")
                    h = base[3] + hdg
                    bx, by = offset_point(base, t0)
                    ring.append((bx + u * math.cos(h) - v * math.sin(h), by + u * math.sin(h) + v * math.cos(h)))
            if len(ring) >= 3:
                ring = [lonlat(p) for p in ring]
                ring.append(ring[0])
                emit("crossing", "Polygon", [ring], {"id": obj.get("id"), "name": obj.get("name", "")})

    # ---- vertical levels: which road section passes over which ----
    # Sections of different, unconnected roads whose asphalt overlaps are a
    # grade separation; the one whose elevation profile is higher at the
    # overlap goes a level up, so the style can draw it - and its edges - over
    # the road beneath. Level 0 is ground, 1 a bridge, 2 a bridge over a bridge.
    def linked(a, b):
        ra, rb = roads[a], roads[b]
        for x, y in ((ra, rb), (rb, ra)):
            for link in (x.pred, x.succ):
                if link is None:
                    continue
                if link.get("elementType") == "road" and link.get("elementId") == y.id:
                    return True
                if link.get("elementType") == "junction" and link.get("elementId") == y.junction:
                    return True
        return ra.junction != "-1" and ra.junction == rb.junction

    keys = list(section_polys.keys())
    tree = STRtree([section_polys[k] for k in keys])
    above = []
    for i, key in enumerate(keys):
        poly_i = section_polys[key]
        for j in tree.query(poly_i):
            j = int(j)
            if j <= i or keys[j][0] == key[0] or linked(key[0], keys[j][0]):
                continue
            overlap = poly_i.intersection(section_polys[keys[j]])
            if overlap.is_empty or overlap.area < 6.0:
                continue
            pt = overlap.representative_point()
            za = roads[key[0]].elevation(roads[key[0]].nearest_s(pt.x, pt.y))
            zb = roads[keys[j][0]].elevation(roads[keys[j][0]].nearest_s(pt.x, pt.y))
            if abs(za - zb) < 1.0:
                continue
            above.append((key, keys[j]) if za > zb else (keys[j], key))

    level = {}
    for _ in range(4):
        changed = False
        for upper, lower in above:
            want = min(2, level.get(lower, 0) + 1)
            if level.get(upper, 0) < want:
                level[upper] = want
                changed = True
        if not changed:
            break
    for feat, section in zip(features, feature_section):
        feat["properties"]["level"] = str(level.get(section, 0)) if section is not None else "0"
    print(f"grade separations: {len(above)} overlapping section pairs, {sum(1 for v in level.values() if v)} raised sections", flush=True)

    print("features by layer:", counts, flush=True)

    # ---- the drive: follow lane links, straightest choice at every fork ----
    def centreline(key, direction):
        inner, outer = lane_geom[key]
        pts = [((a[0] + b[0]) / 2, (a[1] + b[1]) / 2) for a, b in zip(inner, outer)]
        return pts if direction > 0 else pts[::-1]

    def heading(pts, at_end):
        a, b = (pts[-2], pts[-1]) if at_end else (pts[0], pts[1])
        return math.atan2(b[1] - a[1], b[0] - a[0])

    def turn(a, b):
        return (math.degrees(b - a) + 180) % 360 - 180

    def lane_node(road, si, lid):
        side = "left" if lid > 0 else "right"
        for lane in road.sections[si].findall(f"{side}/lane"):
            if int(lane.get("id")) == lid:
                return lane
        return None

    def enter_road(road, contact, lid):
        """Entering `road` at its start (travel +s, right lanes) or end (travel -s, left lanes)."""
        if contact == "start":
            return (road.id, 0, lid, +1)
        return (road.id, len(road.sections) - 1, lid, -1)

    def successors(state):
        road_id, si, lid, direction = state
        road = roads[road_id]
        lane = lane_node(road, si, lid)
        if lane is None:
            return []
        link = lane.find("link")
        link_tag = "successor" if direction > 0 else "predecessor"
        next_lids = [int(l.get("id")) for l in link.findall(link_tag)] if link is not None else []
        next_si = si + direction
        if 0 <= next_si < len(road.sections):
            return [(road_id, next_si, n, direction) for n in next_lids if lane_type.get((road_id, next_si, n)) in DRIVING]
        road_link = road.succ if direction > 0 else road.pred
        if road_link is None:
            return []
        out = []
        if road_link.get("elementType") == "road":
            nxt = roads.get(road_link.get("elementId"))
            if nxt is not None:
                for n in next_lids:
                    st = enter_road(nxt, road_link.get("contactPoint", "start"), n)
                    if lane_type.get(st[:3]) in DRIVING:
                        out.append(st)
        elif road_link.get("elementType") == "junction":
            j = junctions.get(road_link.get("elementId"))
            if j is not None:
                for conn in j.findall("connection"):
                    if conn.get("incomingRoad") != road_id:
                        continue
                    connecting = roads.get(conn.get("connectingRoad"))
                    if connecting is None:
                        continue
                    for ll in conn.findall("laneLink"):
                        if int(ll.get("from")) == lid:
                            st = enter_road(connecting, conn.get("contactPoint", "start"), int(ll.get("to")))
                            if lane_type.get(st[:3]) in DRIVING:
                                out.append(st)
        return out

    def lanes_across(state):
        road_id, si, lid, direction = state
        side = "left" if lid > 0 else "right"
        ids = sorted(int(l.get("id")) for l in roads[road_id].sections[si].findall(f"{side}/lane")
                     if l.get("type") in DRIVING)
        # In travel direction the innermost lane (|id| = 1) is on the left.
        ordered = sorted(ids, key=abs)
        return ordered.index(lid) + 1, len(ordered)

    def piece_length(state):
        p = centreline(state[:3], state[3])
        return sum(math.hypot(p[i + 1][0] - p[i][0], p[i + 1][1] - p[i][1]) for i in range(len(p) - 1))

    # Scored by length x lanes across, so the drive prefers the wide roads: a
    # lane-level view has more to show on three lanes than on one.
    def walk(start):
        path, seen = [start], {start}
        score = piece_length(start) * lanes_across(start)[1]
        while True:
            cur = path[-1]
            options = [s for s in successors(cur) if s not in seen and len(centreline(s[:3], s[3])) >= 2]
            if not options:
                return score, path
            h = heading(centreline(cur[:3], cur[3]), True)
            best = min(options, key=lambda s: abs(turn(h, heading(centreline(s[:3], s[3]), False))))
            path.append(best)
            seen.add(best)
            score += piece_length(best) * lanes_across(best)[1]

    # Starts: the middle lane of any multi-lane section on a non-junction road,
    # so the drive opens on the widest stretch the map has; single-lane roads
    # at their entry end only if nothing wider exists.
    candidates, fallback = [], []
    for key, ltype in lane_type.items():
        road_id, si, lid = key
        if ltype not in DRIVING or roads[road_id].junction != "-1":
            continue
        state = (road_id, si, lid, +1 if lid < 0 else -1)
        index, count = lanes_across(state)
        middle = (count + 1) // 2 + (1 if count % 2 == 0 else 0)
        if count >= 2 and index == middle:
            candidates.append((count, state))
        elif (lid < 0 and si == 0) or (lid > 0 and si == len(roads[road_id].sections) - 1):
            fallback.append(state)
    # Three lanes across if the map has them (four and five are usually short
    # turn pockets), else the widest there is.
    wanted = min(3, max((c for c, _ in candidates), default=0))
    starts = [s for c, s in candidates if c >= wanted] or fallback
    if start_override:
        starts = [s for s in starts if s[0] == start_override] or [s for s in fallback if s[0] == start_override] or starts
    print(f"walking from {len(starts)} start lanes", flush=True)
    score, path = max((walk(s) for s in starts), key=lambda r: r[0])
    while len(path) > 1 and roads[path[0][0]].junction != "-1":
        path.pop(0)
    total = sum(piece_length(s) for s in path)
    print(f"route: {total:.0f} m over {len(path)} lane pieces, start road {path[0][0]} lane {path[0][2]}, "
          f"lanes across: {dict(sorted(__import__('collections').Counter(lanes_across(s)[1] for s in path).items()))}", flush=True)

    route_points, route_lanes = [], []
    route_keys = set()
    for ordinal, state in enumerate(path):
        index, count = lanes_across(state)
        route_lanes.append({"id": "/".join(map(str, state[:3])), "index": index, "count": count,
                            "intersection": roads[state[0]].junction != "-1"})
        route_keys.add(state[:3])
        pts = simplify(centreline(state[:3], state[3]), 0.03)
        for p in (pts if ordinal == 0 else pts[1:]):
            route_points.append(lonlat(p) + [ordinal])

    for feat in features:
        props = feat["properties"]
        if props["layer"] == "lane" and tuple(props["id"].split("/")) in {tuple(map(str, k)) for k in route_keys}:
            props["layer"] = "route_lane"

    os.makedirs(out_dir, exist_ok=True)
    attribution = header.get("vendor", "") + " / OpenDRIVE " + header.get("revMajor", "") + "." + header.get("revMinor", "")
    with open(os.path.join(out_dir, name + "Lanes.geojson"), "w", encoding="utf-8", newline="\r\n") as fh:
        json.dump({"type": "FeatureCollection", "attribution": attribution, "features": features}, fh, separators=(",", ":"))
    with open(os.path.join(out_dir, name + "Route.json"), "w", encoding="utf-8", newline="\r\n") as fh:
        json.dump({"attribution": attribution, "points": route_points, "lanes": route_lanes}, fh, separators=(",", ":"))
    print("wrote", name + "Lanes.geojson", "and", name + "Route.json")


def emit_marks(lane, samples, t_values, section_start, is_junction, emit, lonlat):
    marks = lane.findall("roadMark")
    if not marks:
        return
    for i, mark in enumerate(marks):
        s_from = section_start + f(mark, "sOffset")
        s_to = section_start + f(marks[i + 1], "sOffset") if i + 1 < len(marks) else float("inf")
        layer = LAYER_FOR_MARK.get((mark.get("type"), mark.get("color", "standard")))
        if layer is None:
            continue
        if is_junction and layer == "lane_edge":
            continue
        pts = [offset_point(sm, t_values[k]) for k, sm in enumerate(samples) if s_from - 1e-9 <= sm[0] <= s_to + 1e-9]
        if len(pts) < 2:
            continue
        coords = [lonlat(p) for p in simplify(pts, 0.03)]
        emit(layer, "LineString", coords, {"mark": mark.get("type"), "color": mark.get("color", "standard"), "width": mark.get("width", "")})


if __name__ == "__main__":
    main()
