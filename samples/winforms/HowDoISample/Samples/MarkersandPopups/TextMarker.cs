using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using DrawingBrush = System.Drawing.Brush;
using DrawingColor = System.Drawing.Color;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class TextMarker : UserControl
    {
        public TextMarker()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a text-based image to use as a map marker.
            // The method returns a tuple containing:
            //   - markerImageSource: an ImageSource representing the rendered text with styling
            //   - imageSize: the actual pixel dimensions (width and height) of the generated image
            var (markerImageSource, imageSize) = MarkerImageFactory.CreateTextImageSource(
                "Hello, World!",
                new Font("Segoe UI", 20, FontStyle.Regular),
                System.Drawing.Color.DarkBlue,
                System.Drawing.Color.FromArgb(180, System.Drawing.Color.White));

            // Create the marker
            var marker = new Marker(0, 0)
            {
                Width = imageSize.Width,
                Height = imageSize.Height,
                ImageSource = markerImageSource,
                YOffset = -imageSize.Height / 2,
            };

            // Add the marker to the overlay
            var markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.Markers.Add(marker);
            markerOverlay.DragMode = MarkerDragMode.Drag;
            mapView.Overlays.Add(markerOverlay);

            // Set the map extent
            mapView.CurrentExtent = MaxExtents.SphericalMercator;

            _ = mapView.RefreshAsync();
        }


        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Dock = DockStyle.Fill;
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            // 
            // NavigationMap
            // 
            Controls.Add(mapView);
            Name = "NavigationMap";
            Size = new Size(1257, 669);
            Load += Form_Load;
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }


    public static class MarkerImageFactory
    {
        public static (ImageSource Image, Size Size) CreateTextImageSource(string text, Font font, DrawingColor textColor, DrawingColor backColor, Size? imageSize = null)
        {
            Size size;
            using (var tempBitmap = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(tempBitmap))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                var format = new StringFormat
                {
                    FormatFlags = StringFormatFlags.NoWrap
                };

                var measuredSize = g.MeasureString(text, font, 1000, format);
                size = imageSize ?? new Size((int)Math.Ceiling(measuredSize.Width), (int)Math.Ceiling(measuredSize.Height));
            }

            using (Bitmap bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(DrawingColor.Transparent);

                    using (DrawingBrush brush = new SolidBrush(textColor))
                    {
                        var format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                            FormatFlags = StringFormatFlags.NoWrap
                        };
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        g.DrawString(text, font, brush, new RectangleF(0, 0, size.Width, size.Height), format);
                    }
                }

                return (ConvertBitmapToImageSource(bmp), size);
            }
        }

        private static ImageSource ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad; 
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); 

                return bitmapImage;
            }
        }
    }
}
