using System;
using System.Collections.ObjectModel;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    [Serializable]
    public class ImageryOptimizedWorldStreetsVectorLayer : WorldStreetsLayer
    {
        private ShapeFileFeatureLayer countriesOutlineLayer;
        private string countriesOutlineLayerPathFilename;

        public ImageryOptimizedWorldStreetsVectorLayer(string connectionString, string countriesOutlineLayerPathFilename)
            : base(connectionString, "worldmapkit", "id", "geometry", 3857)
        {
            this.countriesOutlineLayerPathFilename = countriesOutlineLayerPathFilename;
        }

        public string CountriesOutlineLayerPathFilename
        {
            get { return countriesOutlineLayerPathFilename; }
            set { countriesOutlineLayerPathFilename = value; }
        }

        protected override void OpenCore()
        {
            base.OpenCore();

            Collection<string> keys = this.Layers.GetKeys();

            foreach (string key in keys)
            {
                if (!key.Contains("linestring") && !key.Contains("point") && !key.Contains("ne_state5m_polygon") && !key.Contains("ne_baseland30m_polygon") || key.Contains("ne_country10m_linestring"))
                {
                    this.Layers.Remove(key);
                }
            }

            countriesOutlineLayer = new ShapeFileFeatureLayer(countriesOutlineLayerPathFilename, GeoFileReadWriteMode.Read);
            countriesOutlineLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(255, 177, 154, 194), 1), new GeoSolidBrush(GeoColors.Transparent));
            countriesOutlineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level07;
            countriesOutlineLayer.Transparency = 155;

            if (this.Srid != 4326)
            {
                countriesOutlineLayer.FeatureSource.Projection = new Proj4Projection(4326, this.Srid);
                countriesOutlineLayer.FeatureSource.Projection.Open();
            }

            countriesOutlineLayer.Open();
        }

        protected override void DrawBackgroundCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            base.DrawCore(canvas, labelsInAllLayers);

            countriesOutlineLayer.Draw(canvas, labelsInAllLayers);
        }

        protected override void CloseCore()
        {
            base.CloseCore();

            if (countriesOutlineLayer != null)
            {
                if (countriesOutlineLayer.FeatureSource.Projection != null)
                    countriesOutlineLayer.FeatureSource.Projection.Close();

                countriesOutlineLayer.Close();
            }
        }

        protected override TextStyle GetGeneralPurposeTextStyle(string labelColumnName, float fontSize)
        {
            TextStyle defaultTextStyle = base.GetGeneralPurposeTextStyle(labelColumnName, fontSize + 3);
            defaultTextStyle.TextLineSegmentRatio = 1.3;

            return defaultTextStyle;
        }

        protected override TextStyle GetPoiLabelTextStyle(string labelColumnName, float fontSize)
        {
            return base.GetPoiLabelTextStyle(labelColumnName, fontSize + 1);
        }

        protected override PointStyle GetImagePointStyle(string filename)
        {
            PointStyle background = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColors.White), new GeoPen(GeoColors.SlateGray, 1), 20);
            background.CustomPointStyles.Add(new PointStyle()
            {
                PointType = PointType.Bitmap,
                Image = new GeoImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", filename)),
                DrawingLevel = DrawingLevel.LabelLevel
            });

            return background;
        }

        protected override AreaStyle GetBaseLandAreaStyle()
        {
            return new AreaStyle(new GeoPen(GeoColors.Transparent), new GeoSolidBrush(GeoColors.Transparent));
        }

        protected override LineStyle GetMotorwayOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 225, 183, 122), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetMotorwayFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 247, 195, 128), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetTrunkOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 225, 205, 168), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetTrunkFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 245, 231, 202), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetPrimaryOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 240, 234, 208), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetPrimaryFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 250, 246, 225), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetRoadOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(100, 225, 225, 225), width - 0.9f)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetRoadFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(100, 255, 255, 255), width - 0.9f)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetMotorwayLinkOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 225, 182, 120), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetMotorwayLinkFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 247, 221, 185), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetTrunkLinkOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 226, 208, 172), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetTrunkLinkFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 250, 242, 225), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetPrimaryLinkOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 204, 204, 204), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetPrimaryLinkFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 250, 246, 225), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetMinorRoadOutlineLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 204, 204, 204), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }

        protected override LineStyle GetMinorRoadFillLineStyle(float width)
        {
            return new LineStyle(new GeoPen(new GeoColor(125, 240, 239, 238), width)
            {
                StartCap = DrawingLineCap.Round,
                EndCap = DrawingLineCap.Round
            });
        }
    }
}