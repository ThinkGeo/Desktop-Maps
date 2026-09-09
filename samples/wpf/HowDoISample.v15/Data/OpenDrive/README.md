# OpenDRIVE lane map for the Lane-Level Navigation sample

A lane-level HD map evaluated from ASAM OpenDRIVE into GeoJSON by
`tools/xodr-to-lanes.py`: every lane as a polygon, every road mark as a line
with its paint type, curbs, ground symbols, and the vertical level of each road
section where one road passes over another. `XodrBrunswickRoute.json` is the
drive the sample follows: lane centerline points tagged with the lane they lie in.

| files | source | license |
|---|---|---|
| `XodrBrunswick*` | DLR "ViVre research track" HD map of Brunswick, surveyed by 3D Mapping Solutions. https://doi.org/10.5281/zenodo.7071846 | CC BY 4.0 (`LICENSE-Brunswick-CC-BY-4.0.txt`, `CITATION-Brunswick.cff`) |

The DLR map is prototypic, surveyed in 2021, and published for research and
development; its authors guarantee neither completeness nor correctness. It is
sample data here, not navigation data.

To convert another georeferenced `.xodr`:

```powershell
pip install pyproj shapely
python tools\xodr-to-lanes.py C:\data\your-map.xodr . YourName   # -> YourNameLanes.geojson + YourNameRoute.json
```
