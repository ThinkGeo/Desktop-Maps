using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace ShapefileEncryption
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            // encrypt each file
            EncryptFile(@"..\..\Data\USStates.shp", @"..\..\Data\encrypted.shp");
            EncryptFile(@"..\..\Data\USStates.shx", @"..\..\Data\encrypted.shx");
            EncryptFile(@"..\..\Data\USStates.dbf", @"..\..\Data\encrypted.dbf");
            EncryptFile(@"..\..\Data\USStates.idx", @"..\..\Data\encrypted.idx");
            EncryptFile(@"..\..\Data\USStates.ids", @"..\..\Data\encrypted.ids");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-180, 90, 180, -90);

            ShapeFileFeatureLayer countryLayer = new ShapeFileFeatureLayer(@"encrypted.shp");
            ((ShapeFileFeatureSource)countryLayer.FeatureSource).StreamLoading += new EventHandler<StreamLoadingEventArgs>(MainForm_StreamLoading);
            countryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            countryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;

            LayerOverlay testOverlay = new LayerOverlay();
            testOverlay.Layers.Add("CountryLayer", countryLayer);
            winformsMap1.Overlays.Add("testOverlay", testOverlay);

            winformsMap1.Refresh();
        }

        // The encryption is just a sample (simply adds 1 on each byte)
        private void EncryptFile(string sourceFileName, string encryptedFileName)
        {
            Stream stream = File.Open(sourceFileName, FileMode.Open);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] - 1);
            }

            if (File.Exists(encryptedFileName)) File.Delete(encryptedFileName);

            Stream encryptedStream = File.Create(encryptedFileName);
            encryptedStream.Write(bytes, 0, bytes.Length);
            encryptedStream.Close();
        }

        // This is the Decrypt method
        private Stream DecryptFile(string encryptedFileName)
        {
            Stream stream = File.Open(encryptedFileName, FileMode.Open);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] + 1);
            }

            return new MemoryStream(bytes);
        }

        void MainForm_StreamLoading(object sender, StreamLoadingEventArgs e)
        {
            string fileName = Path.GetFileName(e.AlternateStreamName);
            // The DecryptFile method will return a stream which was loaded from the encrypted file and decrypted it as 
            // a stream.
            e.AlternateStream = DecryptFile(@"..\..\Data\" + fileName);
        }

      
        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

    }
}
