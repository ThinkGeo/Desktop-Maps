using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

public partial class Sample : Form
{
    private RectangleShape fullExtent;
    private LayerOverlay layerOverlay;

    public Sample()
    {
        InitializeComponent();
    }

    private void DisplayASimpleMap_Load(object sender, EventArgs e)
    {
        layerOverlay = new LayerOverlay();
        winformsMap1.Overlays.Add(layerOverlay);

        winformsMap1.MapUnit = GeographyUnit.Meter;

        string[] dwgFiles = Directory.GetFiles(@"..\..\Data\");
        foreach (string dwgFile in dwgFiles)
        {
            sampleFileListBox.Items.Add(Path.GetFileName(dwgFile));
        }
        // Display the first file in the list
        sampleFileListBox.SelectedIndex = 0;
    }

    private void sampleFileListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        CadFeatureLayer layer = new CadFeatureLayer(@"..\..\Data\" + sampleFileListBox.SelectedItem.ToString());

        // Render the data using embedded style in the CAD file
        if (radioButtonEmbeddedStyle.Checked)
        {
            layer.StylingType = CadStylingType.EmbeddedStyling;
        }
        // Render the data using a customized style
        else
        {
            layer.StylingType = CadStylingType.StandardStyling;
            layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Black, 2, true);
            layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Blue, 8);
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Yellow);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        layerOverlay.Layers.Clear();
        layerOverlay.Layers.Add(layer);

        Cursor = Cursors.WaitCursor;
        layer.Open();
        fullExtent = layer.GetBoundingBox();
        winformsMap1.CurrentExtent = fullExtent;
        winformsMap1.Refresh();
        Cursor = Cursors.Default;
    }

    private void radioButtonStandardStyle_CheckedChanged(object sender, EventArgs e)
    {
        CadFeatureLayer layer = ((CadFeatureLayer)layerOverlay.Layers[0]);
        if (radioButtonStandardStyle.Checked)
        {
            layer.StylingType = CadStylingType.StandardStyling;
            layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Black, 1F, false);
            layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.White, 3.2F, GeoColor.StandardColors.Black, 1F);
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
        else
        {
            layer.StylingType = CadStylingType.EmbeddedStyling;
        }
        winformsMap1.Refresh();
    }

    private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
        switch (e.Button.Tag.ToString())
        {
            case "Zoom In":
                winformsMap1.CurrentExtent.ScaleDown(50);
                break;
            case "Zoom Out":
                winformsMap1.CurrentExtent.ScaleUp(50);
                break;
            case "Full Extent":
                winformsMap1.CurrentExtent = fullExtent;
                break;
            case "Pan Left":
                winformsMap1.CurrentExtent = ExtentHelper.Pan(winformsMap1.CurrentExtent, PanDirection.Left, 20);
                break;
            case "Pan Right":
                winformsMap1.CurrentExtent = ExtentHelper.Pan(winformsMap1.CurrentExtent, PanDirection.Right, 20);
                break;
            case "Pan Up":
                winformsMap1.CurrentExtent = ExtentHelper.Pan(winformsMap1.CurrentExtent, PanDirection.Up, 20);
                break;
            case "Pan Down":
                winformsMap1.CurrentExtent = ExtentHelper.Pan(winformsMap1.CurrentExtent, PanDirection.Down, 20);
                break;
            default:
                break;
        }
        winformsMap1.Refresh();
    }
}
