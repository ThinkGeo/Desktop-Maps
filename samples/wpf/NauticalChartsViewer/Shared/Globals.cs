using System;
using System.IO;
using ThinkGeo.Core;

namespace NauticalChartsViewer
{
    internal static class Globals
    {
        // Editable copy of the S-52 presentation library. It is exported once from the
        // SDK's built-in default library (NauticalChartsFeatureLayer.ExportDefaultStyleFile);
        // the "Edit Symbol File" editor opens this file and saves edits back to it in place,
        // and every chart is loaded against it, so symbol/color edits show up on the map.
        public static readonly string StyleFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NauticalCharts.style.xml");

        // Writes the SDK's built-in default S-52 style to StyleFilePath if it is not there yet.
        public static void EnsureStyleFile()
        {
            if (!File.Exists(StyleFilePath))
            {
                NauticalChartsFeatureLayer.ExportDefaultStyleFile(StyleFilePath);
            }
        }

        public static double SafetyDepth = 28.0d;

        public static double SafetyContour = 10.0d;

        public static double ShallowDepth = 3.0d;

        public static double DeepDepth = 10.0d;

        public static NauticalChartsDisplayCategory DisplayMode;

        public static NauticalChartsSymbolTextDisplayMode SymbolTextDisplayMode;

        public static NauticalChartsDefaultColorSchema CurrentColorSchema;

        public static NauticalChartsSymbolDisplayMode CurrentSymbolDisplayMode;

        public static NauticalChartsBoundaryDisplayMode CurrentBoundaryDisplayMode;

        public static NauticalChartsDepthUnit CurrentDepthUnit;

        public static bool IsDepthContourTextVisible = true;

        public static bool IsFullLightLineVisible = true;

        public static bool IsMetaObjectsVisible = false;

        public static bool IsLightDescriptionVisible = true;

        public static bool IsSoundingTextVisible = true;

        public static bool IsShallowWaterPatternVisible = false;

        public static bool IsIsolatedDangerVisible = false;

        public static bool IsMinimumScaleEnabled = true;

        public static NauticalChartsDepthShades CurrentDepthShades = NauticalChartsDepthShades.TwoColor;

        public static NauticalChartsStylingType CurrentStylingType = NauticalChartsStylingType.EmbeddedStyling;


        static Globals()
        {
            DisplayMode = NauticalChartsDisplayCategory.All;
            SymbolTextDisplayMode = NauticalChartsSymbolTextDisplayMode.None;
        }
    }
}