using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace CSHowDoISamples
{
    public partial class Samples : Form
    {
        enum CodeType
        {
            CSharp,
            VisualBasic
        }

        public static readonly string RootDirectory = @"..\";

        private readonly string mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent).FullName + "\\";
        private const string preStart = "<body oncontextmenu='return false;'><div class='divbody'><pre name='code' class='c-sharp:nocontrols'>";
        private const string preEndFormat = "</pre></div><link type='text/css' rel='stylesheet' href='{0}\\SyntaxHighlighter\\SyntaxHighlighter.css'></link><script language='javascript' src='{0}\\SyntaxHighlighter\\shCore.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter/shBrushCSharp.js'></script><script language='javascript' src='{0}\\SyntaxHighlighter\\shBrushXml.js'></script><script language='javascript'>dp.SyntaxHighlighter.HighlightAll('code');</script></body>";
        private string currentSample;

        public Samples()
        {
            InitializeComponent();
        }

        private void Samples_Load(object sender, EventArgs e)
        {
            treeViewLeft.Nodes["RootNode"].Expand();
            adRotator.Url = new Uri(mainFolder + "bannerad_offline.html");
            if (IsNetworkAlive())
            {
                tmrRotator.Start();
            }
        }

        private void treeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tabctrlShow.SelectedTab = tabpageSample;
            currentSample = e.Node.Name;
            pnlOption.Controls.Clear();

            switch (e.Node.Name)
            {
                case "UseQualityFamilyAreaStyle":
                    pnlOption.Controls.Add(new UseQualityFamilyAreaStyle());
                    break;
                case "UseHueFamilyAreaStyle":
                    pnlOption.Controls.Add(new UseHueFamilyAreaStyle());
                    break;
                case "LoadAMapFromStreams":
                    pnlOption.Controls.Add(new LoadAMapFromStreams());
                    break;
                case "DrawUsingZedGraphStyle":
                    pnlOption.Controls.Add(new DrawUsingZedGraphStyle());
                    break;
                case "DisplayASimpleMap":
                    pnlOption.Controls.Add(new DisplayShapeMap());
                    break;
                case "ZoomInAndOutOfTheMap":
                    pnlOption.Controls.Add(new ZoomInAndOutOfTheMap());
                    break;
                case "DisplayASatelliteImage":
                    pnlOption.Controls.Add(new DisplayASatelliteImage());
                    break;
                case "UsingTileCache":
                    pnlOption.Controls.Add(new UsingTileCache());
                    break;
                case "PanAroundTheMap":
                    pnlOption.Controls.Add(new PanAroundTheMap());
                    break;
                case "HideOrShowAFeatureLayer":
                    pnlOption.Controls.Add(new HideOrShowAFeatureLayer());
                    break;
                case "HideOrShowAnImageLayer":
                    pnlOption.Controls.Add(new HideOrShowARasterLayer());
                    break;
                case "ChangeTheLookOfAFeatureLayer":
                    pnlOption.Controls.Add(new ChangeTheLookOfAFeatureLayer());
                    break;
                case "SqlQueryAFeatureLayer":
                    pnlOption.Controls.Add(new SqlQueryAFeatureLayer());
                    break;
                case "ConvertScreenCoordinatesToWorldCoordinates":
                    pnlOption.Controls.Add(new ConvertScreenCoordinatesToWorldCoordinates());
                    break;
                case "FindTheFeatureAUserClickedOn":
                    pnlOption.Controls.Add(new FindTheFeatureAUserClickedOn());
                    break;
                case "LoadAPostgreSqlFeatureLayer":
                    pnlOption.Controls.Add(new LoadAPostgreSqlFeatureLayer());
                    break;
                case "LoadMsSql2008FeatureLayer":
                    pnlOption.Controls.Add(new LoadMsSql2008FeatureLayer());
                    break;
                case "LoadAnOracleFeatureLayer":
                    pnlOption.Controls.Add(new LoadAnOracleFeatureLayer());
                    break;
                case "DrawFeaturesBasedOnDotDensity":
                    pnlOption.Controls.Add(new DrawFeaturesBasedOnDotDensity());
                    break;
                case "PlotALatitudeAndLongitudePointOnTheMap":
                    pnlOption.Controls.Add(new PlotALatitudeAndLongitudePointOnTheMap());
                    break;
                case "ZoomToACertainScale":
                    pnlOption.Controls.Add(new ZoomToACertainScale());
                    break;
                case "ZoomToACertainZoomLevel":
                    pnlOption.Controls.Add(new ZoomToACertainZoomLevel());
                    break;
                case "ZoomToAFeatureOrFeatures":
                    pnlOption.Controls.Add(new ZoomToAFeatureOrFeatures());
                    break;
                case "DisplayFeatureLayerAtCertainScale":
                    pnlOption.Controls.Add(new DisplayFeatureLayerAtCertainScale());
                    break;
                case "LoadAnEcwImage":
                    pnlOption.Controls.Add(new LoadAnEcwImage());
                    break;
                case "LoadAMrSidImage":
                    pnlOption.Controls.Add(new LoadAMrSidImage());
                    break;
                case "Load a Geotiff image":
                    pnlOption.Controls.Add(new LoadAGeoTiffImage()); 
                    break;
                case "FindFeaturesWithinDistance":
                    pnlOption.Controls.Add(new FindFeaturesWithinDistance());
                    break;
                case "CreateAnInMemoryFeatureLayer":
                    pnlOption.Controls.Add(new CreateAnInMemoryFeatureLayer());
                    break;
                case "DrawUsingPredefinedCommonStyle":
                    pnlOption.Controls.Add(new DrawUsingPredefinedCommonStyle());
                    break;
                case "DrawAndLabelANiceLookingRoad":
                    pnlOption.Controls.Add(new DrawAndLabelANiceLookingRoad());
                    break;
                case "DrawAndLabelWaterFeatures":
                    pnlOption.Controls.Add(new DrawAndLabelWaterFeatures());
                    break;
                case "DrawAndLabelPointsOfInterests":
                    pnlOption.Controls.Add(new DrawAndLabelPointsOfInterests());
                    break;
                case "DrawAFeatureBasedOnAValue":
                    pnlOption.Controls.Add(new DrawAFeatureBasedOnAValue());
                    break;
                case "DrawThematicFeatures":
                    pnlOption.Controls.Add(new DrawThematicFeatures());
                    break;
                case "DrawFeaturesBasedOnValues":
                    pnlOption.Controls.Add(new DrawFeaturesBasedOnValues());
                    break;
                case "DrawFeaturesBasedRegularExpression":
                    pnlOption.Controls.Add(new DrawFeaturesBasedRegularExpression());
                    break;
                case "DrawFeaturesBasedExternalData":
                    pnlOption.Controls.Add(new DrawFeaturesBasedExternalData());
                    break;
                case "ChangeTheWidthOrColorOfALine":
                    pnlOption.Controls.Add(new ChangeTheWidthOrColorOfALine());
                    break;
                case "ChangeTheFillColorAndOutlineOfAArea":
                    pnlOption.Controls.Add(new ChangeTheFillColorAndOutlineOfAArea());
                    break;
                case "DrawAPointUsingABitmap":
                    pnlOption.Controls.Add(new DrawAPointUsingABitmap());
                    break;
                case "DrawAPointUsingASymbol":
                    pnlOption.Controls.Add(new DrawAPointUsingASymbol());
                    break;
                case "ChangeTheLabelPlacementForPoints":
                    pnlOption.Controls.Add(new ChangeTheLabelPlacementForPoints());
                    break;
                case "ChangeTheLabelPosition":
                    pnlOption.Controls.Add(new ChangeTheLabelPosition());
                    break;
                case "DetermineTheAreaOfAnAreaFeature":
                    pnlOption.Controls.Add(new DetermineTheAreaOfAnAreaFeature());
                    break;
                case "DetermineTheLengthOfALineFeature":
                    pnlOption.Controls.Add(new DetermineTheLengthOfALineFeature());
                    break;
                case "FindFeaturesCountInAFeatureLayer":
                    pnlOption.Controls.Add(new FindFeaturesCountInAFeatureLayer());
                    break;
                case "GetAllFeaturesFromAFeatureLayer":
                    pnlOption.Controls.Add(new GetAllFeaturesFromAFeatureLayer());
                    break;
                case "GetAColumnDataInAFeatureLayer":
                    pnlOption.Controls.Add(new GetAColumnDataInAFeatureLayer());
                    break;
                case "MoveAFeature":
                    pnlOption.Controls.Add(new MoveAFeature());
                    break;
                case "ScaleUpAndDownAFeature":
                    pnlOption.Controls.Add(new ScaleUpAndDownAFeature());
                    break;
                case "FindTheDistanceBetweenTwoFeatures":
                    pnlOption.Controls.Add(new FindTheDistanceBetweenTwoFeatures());
                    break;
                case "CreateABufferAroundAFeature":
                    pnlOption.Controls.Add(new CreateABufferAroundAFeature());
                    break;
                case "FindUnionBetweenTwoFeatures":
                    pnlOption.Controls.Add(new FindUnionBetweenTwoFeatures());
                    break;
                case "FindDifferenceBetweenTwoFeatures":
                    pnlOption.Controls.Add(new FindDifferenceBetweenTwoFeatures());
                    break;
                case "FindShortestLineBetweenTwoFeatures":
                    pnlOption.Controls.Add(new FindShortestLineBetweenTwoFeatures());
                    break;
                case "ConvertAFeatureToAndFromWkb":
                    pnlOption.Controls.Add(new ConvertAFeatureToAndFromWkb());
                    break;
                case "CreateASpatialIndexForAShapeFileFeatureLayer":
                    pnlOption.Controls.Add(new CreateASpatialIndexForAShapeFileFeatureLayer());
                    break;
                case "ConvertWorldCoordinatesToScreenCoordinates":
                    pnlOption.Controls.Add(new ConvertWorldCoordinatesToScreenCoordinates());
                    break;
                case "UseGeographicAndStandardColors":
                    pnlOption.Controls.Add(new UseGeographicAndStandardColors());
                    break;
                case "FindAFeatureFromItsId":
                    pnlOption.Controls.Add(new FindAFeatureFromItsId());
                    break;
                case "HighlightAFeatureOnTheMap":
                    pnlOption.Controls.Add(new HighlightAFeatureOnTheMap());
                    break;
                case "DetermineWhereUserClickInWorldCoordinate":
                    pnlOption.Controls.Add(new DetermineWhereUserClickInWorldCoordinate());
                    break;
                case "ShowTemporaryShapesOnTheMap":
                    pnlOption.Controls.Add(new ShowTemporaryShapesOnTheMap());
                    break;
                case "ConvertAFeatureToAndFromWkt":
                    pnlOption.Controls.Add(new ConvertAFeatureToAndFromWkt());
                    break;
                case "LoadAShapeFileFeatureLayer":
                    pnlOption.Controls.Add(new LoadAShapeFileFeatureLayer());
                    break;
                case "DetermineTheEnvelopeOfAFeature":
                    pnlOption.Controls.Add(new DetermineTheEnvelopeOfAFeature());
                    break;
                case "FindOutColumnDataInAFeatureLayer":
                    pnlOption.Controls.Add(new FindOutColumnDataInAFeatureLayer());
                    break;
                case "UseRotationProjectionForAFeatureLayer":
                    pnlOption.Controls.Add(new UseRotationProjectionForAFeatureLayer());
                    break;
                case "UseADifferentProjectionForAFeatureLayer":
                    pnlOption.Controls.Add(new UseADifferentProjectionForAFeatureLayer());
                    break;
                case "StopCertainFeaturesFromDrawing":
                    pnlOption.Controls.Add(new StopCertainFeaturesFromDrawing());
                    break;
                case "AddMyOwnCustomDataToAFeatureLayer":
                    pnlOption.Controls.Add(new AddMyOwnCustomDataToAFeatureLayer());
                    break;
                case "AddFeaturesFromAFeatureLayer":
                    pnlOption.Controls.Add(new AddFeaturesFromAFeatureLayer());
                    break;
                case "EditFeaturesFromAFeatureLayer":
                    pnlOption.Controls.Add(new EditFeaturesFromAFeatureLayer());
                    break;
                case "DeleteFeaturesFromAFeatureLayer":
                    pnlOption.Controls.Add(new DeleteFeaturesFromAFeatureLayer());
                    break;
                case "AddAdditionalCustomPropertiesAndMethods":
                    pnlOption.Controls.Add(new AddAdditionalCustomPropertiesAndMethods());
                    break;
                case "EfficientlyMoveAPlaneImage":
                    pnlOption.Controls.Add(new EfficientlyMoveAPlaneImage());
                    break;
                case "GetAGroupOfColorsInHueFamily":
                    pnlOption.Controls.Add(new GetAGroupOfColorsInHueFamily());
                    break;
                case "GetAGroupOfColorsInQualityFamily":
                    pnlOption.Controls.Add(new GetAGroupOfColorsInQualityFamily());
                    break;
                case "ConvertGeoColorToOtherColors":
                    pnlOption.Controls.Add(new ConvertAGeoColorToAndFromOleWin32HtmlArgbColors());
                    break;
                case "SpatialQueryAFeatureLayer":
                    pnlOption.Controls.Add(new SpatialQueryAFeatureLayer());
                    break;
                case "LoadAGridFeatureLayer":
                    pnlOption.Controls.Add(new LoadAGridFeatureLayer());
                    break;
                case "LoadAHeatLayer":
                    pnlOption.Controls.Add(new LoadAHeatLayer());
                    break;
                case "LoadAWmsImage":
                    pnlOption.Controls.Add(new LoadAWmsImage());
                    break;
                case "CreateARestrictionLayer":
                    pnlOption.Controls.Add(new CreateARestrictionLayer());
                    break;
                case "CreateAScaleLineAdornmentLayer":
                    pnlOption.Controls.Add(new CreateAScaleLineAdornmentLayer());
                    break;
                case "CreateAGraticuleAdornmentLayer":
                    pnlOption.Controls.Add(new CreateAGraticuleAdornmentLayer());
                    break;
                case "TrackAndEditShapes":
                    pnlOption.Controls.Add(new TrackAndEditShapes());
                    break;
                case "DrawCurvedLabels":
                    pnlOption.Controls.Add(new DrawCurvedLabels());
                    break;
                case "UseMapSimplification":
                    pnlOption.Controls.Add(new UseMapSimplification());
                    break;
                case "UseMicrosoftMapsLayer":
                    pnlOption.Controls.Add(new UseMicrosoftMapsLayer());
                    break;
                case "LoadSdfFeatureLayer":
                    pnlOption.Controls.Add(new LoadSdfFeatureLayer());
                    break;
                //case "LoadGeoDatabaseFeatureLayer":
                //    pnlOption.Controls.Add(new LoadGeoDatabaseFeatureLayer());
                //    break;
                case "LoadTabFileFeatureLayer":
                    pnlOption.Controls.Add(new LoadTabFileFeatureLayer());
                    break;
                case "LoadOgrFeatureLayer":
                    pnlOption.Controls.Add(new LoadOgrFeatureLayer());
                    break;
                //case "LoadGdalRasterLayer":
                //    pnlOption.Controls.Add(new LoadGdalRasterLayer());
                //    break;
                case "LoadWfsFeatureLayer":
                    pnlOption.Controls.Add(new LoadWfsFeatureLayer());
                    break;
                case "AddEventInOleDbFeatureLayer":
                    pnlOption.Controls.Add(new AddEventInOleDbFeatureLayer());
                    break;
                case "UsePdfExtension":
                    pnlOption.Controls.Add(new UsePdfExtension());
                    break;
                case "DrawUsingFleeBooleanStyle":
                    pnlOption.Controls.Add(new DrawUsingFleeBooleanStyle());
                    break;
                case "RefreshPointsRandomly":
                    pnlOption.Controls.Add(new RefreshPointsRandomly());
                    break;
                case "AddSimpleMarkers":
                    pnlOption.Controls.Add(new AddSimpleMarkers());
                    break;
                case "AddLabelOnMarkers":
                    pnlOption.Controls.Add(new AddLabelOnMarkers());
                    break;
                case "AddAnInMemoryMarkerOverlay":
                    pnlOption.Controls.Add(new AddAnInMemoryMarkerOverlay());
                    break;
                case "AddAFeatureSourceMarkerOverlay":
                    pnlOption.Controls.Add(new AddAFeatureSourceMarkerOverlay());
                    break;
                case "UseDraggableMarker":
                    pnlOption.Controls.Add(new UseDraggableMarker());
                    break;
                case "DragToCopyAMarker":
                    pnlOption.Controls.Add(new DragToCopyAMarker());
                    break;
                case "GetDynamicDistanceBetweenTwoPoints":
                    pnlOption.Controls.Add(new GetDynamicDistanceBetweenTwoPoints());
                    break;
                default:
                    WebBrowser webBrower = new WebBrowser();
                    if (e.Node.Level != 2)
                    {
                        webBrower.Url = new Uri(mainFolder + "MS3 Samples - Welcome Page.html");
                    }
                    else
                    {
                        webBrower.Url = new Uri(mainFolder + "MS3 Samples - Sample Not Available in Beta.html");
                    }
                    webBrower.Dock = DockStyle.Fill;
                    pnlOption.Controls.Add(webBrower);
                    break;
            }
            this.Text = "Winform C# Sample Application - " + e.Node.Text;
        }

        private void tabpageCSCode_Enter(object sender, EventArgs e)
        {
            string uri;
            if (currentSample == "RootNode")
            {
                uri = mainFolder + "MS3 Samples - Welcome Page.html";
            }
            else
            {
                uri = GetHtmlPath(currentSample + ".cs", CodeType.CSharp);
            }
            Uri webUri = new Uri(@uri);
            cSharpBrowser.Url = webUri;
        }

        private void miViewCode_Click(object sender, EventArgs e)
        {
            tabctrlShow.SelectedTab = tabpageCSCode;
        }

        private void tabctrlShow_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Image image = new Bitmap(Properties.Resources.TabButtonOn);
            for (int i = 0; i < tabctrlShow.TabPages.Count; i++)
            {
                g.DrawImage(image, tabctrlShow.GetTabRect(i));
                Rectangle rectSample = tabctrlShow.GetTabRect(i);
                rectSample.Offset(25, 4);
                g.DrawString(tabctrlShow.TabPages[i].Text, new Font("Microsoft Sans Serif", 8.25f), new SolidBrush(Color.Black), rectSample);
            }
        }

        private void tmrRotator_Tick(object sender, EventArgs e)
        {
            adRotator.Navigate("http://gis.thinkgeo.com/Default.aspx?tabid=640&random=" + Guid.NewGuid().ToString());
        }

        private string GetHtmlPath(string filename, CodeType codeType)
        {
            if (String.Compare(filename, "Load a Geotiff image.cs", true, CultureInfo.InvariantCulture) == 0)
            {
                filename = "LoadAStandardImageWithWorldFile.cs";
            }
            else if (String.Compare(filename, "ConvertGeoColorToOtherColors.cs", true, CultureInfo.InvariantCulture) == 0)
            {
                filename = "ConvertAGeoColorToAndFromOleWin32HtmlArgbColors.cs";
            }
            string myDocuments = Environment.GetEnvironmentVariable("TEMP");

            string path = myDocuments;
            if (codeType == CodeType.CSharp)
            {
                path = path + "\\" + "MapSuiteCSharpHowDoISamples";
            }
            else
            {
                path = path + "\\" + "MapSuiteVBHowDoISamples";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] files = Directory.GetFiles(mainFolder, filename, SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                return mainFolder + "MS3 Samples - Source Code Not Available in Beta.html";
            }

            string tmpFileName = Path.GetFileNameWithoutExtension(new FileInfo(files[0]).Name) + ".htm";
            string htmlFileName = path + "\\" + tmpFileName;
            if (File.Exists(htmlFileName))
            {
                return htmlFileName;
            }

            string text;
            try
            {
                using (StreamReader streamReader = new StreamReader(files[0]))
                {
                    text = streamReader.ReadToEnd();
                }
                text = preStart + RemoveRegion(text, codeType) + string.Format(preEndFormat, mainFolder);

                using (StreamWriter streamWriter = new StreamWriter(htmlFileName))
                {
                    streamWriter.Write(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }

            return htmlFileName;
        }

        private static string RemoveRegion(string inputText, CodeType codeType)
        {
            string regionPattern, endregionPattern;
            if (codeType == CodeType.CSharp)
            {
                regionPattern = "#region Component Designer generated code";
                endregionPattern = "#endregion";
            }
            else
            {
                regionPattern = "#Region \"Component Designer generated code\"";
                endregionPattern = "#End Region";
            }

            int indexOfRegion = inputText.IndexOf(regionPattern, StringComparison.Ordinal);
            if (indexOfRegion == -1)
            {
                return inputText;
            }
            string firstPart = inputText.Substring(0, indexOfRegion).Trim();

            int indexOfEndregion = inputText.IndexOf(endregionPattern, StringComparison.Ordinal);
            if (indexOfEndregion == -1 || indexOfEndregion <= indexOfRegion)
            {
                return inputText;
            }
            string secondPart = inputText.Substring(indexOfEndregion + endregionPattern.Length);
            return firstPart + secondPart;
        }

        private static bool IsNetworkAlive()
        {
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_NetworkAdapter where NetConnectionStatus=2");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(objectQuery);
            return (searcher.Get().Count > 0) ? true : false;
        }

        private void lnkProductInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=674");
        }

        private void lnkDiscussionForums_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=143");
        }

        private void lnkOnlineStore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/store/");
        }

        private void lnkFAQ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://wiki.thinkgeo.com/wiki/Map_Suite_Desktop_Edition#Frequently_Asked_Questions");
        }

        private void lnkTestimonials_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=130");
        }

        private void lnkContactUs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=147");
        }

        private void adRotator_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}


