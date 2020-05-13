/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Layers;

namespace  DraggingIconAdvanced
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-10609794, 4718077, -10603493, 4713167);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //EditInteractiveOverlay used because it already has the logic for dragging.
            EditInteractiveOverlay editInteractiveOverlay = new EditInteractiveOverlay();

            //Sets the property IsActive for DragControlPointsLayer to false so that the control point (as four arrows) is not visible.
            editInteractiveOverlay.DragControlPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.IsActive = false;
            editInteractiveOverlay.DragControlPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Sets the property IsActive for all the Styles of EditShapesLayer because we are using a ValueStyle instead.
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.IsActive = false;
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.IsActive = false;
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.IsActive = false; 

            //ValueStyle used for displaying the feature according to the value of the column "Type" for displaying with a flag or unknown icon.
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "Type";

            PointStyle carPointStyle = new PointStyle(new GeoImage(@"..\..\Data\locale.png"));
            carPointStyle.PointType = PointType.Bitmap;
            PointStyle busPointStyle = new PointStyle(new GeoImage(@"..\..\Data\unknown.png"));
            busPointStyle.PointType = PointType.Bitmap;

            valueStyle.ValueItems.Add(new ValueItem("Flag", carPointStyle));
            valueStyle.ValueItems.Add(new ValueItem("Unknown", busPointStyle));

            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            editInteractiveOverlay.EditShapesLayer.Open();
            editInteractiveOverlay.EditShapesLayer.Columns.Add(new FeatureSourceColumn("Type"));
            editInteractiveOverlay.EditShapesLayer.Close();

            Feature carFeature = new Feature(new PointShape(-10606621, 4715142));
            carFeature.ColumnValues["Type"] = "Flag";

            Feature busFeature = new Feature(new PointShape(-10608959, 4715629));
            busFeature.ColumnValues["Type"] = "Unknown";

            editInteractiveOverlay.EditShapesLayer.InternalFeatures.Add("Flag", carFeature);
            editInteractiveOverlay.EditShapesLayer.InternalFeatures.Add("Unknown", busFeature);

            //Sets the properties of EditInteractiveOverlay to have the appropriate behavior.
            //Make sure CanDrag is set to true.
            editInteractiveOverlay.CanAddVertex = false;
            editInteractiveOverlay.CanDrag = true;
            editInteractiveOverlay.CanRemoveVertex = false;
            editInteractiveOverlay.CanResize = false;
            editInteractiveOverlay.CanRotate = false;
            editInteractiveOverlay.CalculateAllControlPoints();

            winformsMap1.EditOverlay = editInteractiveOverlay;

            winformsMap1.Refresh();

            //We disable the double click modes in order to not have second effect behavior 
            //of the map when adding or removing an icon by double clicking
            winformsMap1.ExtentOverlay.DoubleLeftClickMode = MapDoubleLeftClickMode.Disabled;
            winformsMap1.ExtentOverlay.DoubleRightClickMode = MapDoubleRightClickMode.Disabled;
        }

        //In the MouseDoubleClick event, we allow adding a new draggable icon by left double clicking on the map and 
        //removing an icon by right double clicking.
        private void winformsMap1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            winformsMap1.EditOverlay.EditShapesLayer.Open();
            PointShape clickedPointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, e.X, e.Y, winformsMap1.Width, winformsMap1.Height);

            //For removing the icon clicked on.
            if (e.Button == MouseButtons.Right)
            {
                Collection<Feature> clickedFeatures = winformsMap1.EditOverlay.EditShapesLayer.QueryTools.GetFeaturesNearestTo(clickedPointShape, GeographyUnit.Meter, 1,
                                                                      ReturningColumnsType.AllColumns);
                winformsMap1.EditOverlay.EditShapesLayer.Close();

                if (clickedFeatures.Count > 0)
                {
                    //Gets the dimension of the icon and checks if the clicked point is inside it.
                    ValueStyle valueStyle = (ValueStyle)winformsMap1.EditOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles[0];
                    //we loop thru the different ValueItem to get the appropriate icon according to the "Type".
                    GeoImage geoImage = null;
                    string text = clickedFeatures[0].ColumnValues["Type"].Trim();
                    foreach (ValueItem valueItem in valueStyle.ValueItems)
                    {
                        if (text == valueItem.Value)
                        {
                            geoImage = (GeoImage)valueStyle.ValueItems[0].DefaultPointStyle.Image;
                            break;
                        }
                    }
                    //We check to see if we clicked inside the icon itself.
                    ScreenPointF screenPointF = ExtentHelper.ToScreenCoordinate(winformsMap1.CurrentExtent, clickedFeatures[0], winformsMap1.Width, winformsMap1.Height);
                    RectangleF rectangleF = new RectangleF(screenPointF.X - (geoImage.Width / 2), screenPointF.Y - (geoImage.Height / 2),
                                                           geoImage.Width, geoImage.Height);
                    bool IsInside = rectangleF.Contains(new PointF(e.X, e.Y));

                    //If inside, removes the feature from the EditShapesLayer of the EditOverlay.
                    if (IsInside == true) 
                    {
                        winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Remove(clickedFeatures[0]);
                        winformsMap1.Refresh(winformsMap1.EditOverlay);
                    }
                }
            }
            //Adding a new icon.
            else if (e.Button == MouseButtons.Left)
            {
                Feature carFeature = new Feature(clickedPointShape);
                carFeature.ColumnValues["Type"] = "Unknown";
                //We use DateTime.Now.Ticks to be sure to use a unique key each time we add a new feature.
                winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(DateTime.Now.Ticks.ToString(), carFeature);
                winformsMap1.EditOverlay.EditShapesLayer.BuildIndex();
                //We call CalculateAllControlPoints to update the control points with the new feature to be able to drag the newly added feature.
                winformsMap1.EditOverlay.CalculateAllControlPoints();
                winformsMap1.Refresh(winformsMap1.EditOverlay);
            }
           
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
