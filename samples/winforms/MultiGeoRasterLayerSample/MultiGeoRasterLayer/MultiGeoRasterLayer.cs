using GeoAPI.Geometries;
using NetTopologySuite.Index.Strtree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    // IMPORTANT: You have to reference NetTopologySuite.dll & GeoApi.dll in your project.  They come with Map Suite.  

    // This class speeds up the loading of a large number of Raster layers by loading and drawing on demand only the files  
    // in the current extent.  Normally if you had 100 Raster files you would need to load 100 layers; however, this has performance  
    // issues--so we created this high level layer.  It loads a reference file that contains the bounding box, path and file information for  
    // all of the Raster files.  We load this information into an in-memory spatial index. When the map requests to draw the layer, we find the  
    // Rasters that are in the current extent, create a layer on-the-fly, call their Draw method and then close them.  In this way, we load  
    // on demand only the files that are in the current extent.  

    // I have also included a small routine to build a reference file from a directory of Raster files.  

    // Reference File Format: [UpperLeftPointX],[LowerRightPoint.X],[UpperLeftPoint.Y],[LowerRightPoint.Y],[Path|& File Name to Raster]  

    // Sample:  
    // -180.02197265625,-157.52197265625,-67.47802734375,-89.97802734375,..\..\App_Data\RasterImage\1.jpg  
    // -112.52197265625,-90.02197265625,-67.47802734375,-89.97802734375,..\..\App_Data\RasterImage\10.jpg  
    // 67.47802734375,89.97802734375,67.52197265625,45.02197265625,..\..\App_Data\RasterImage\100.jpg  
    // 89.97802734375,112.47802734375,67.52197265625,45.02197265625,..\..\App_Data\RasterImage\101.jpg  

    public class MultiGeoRasterLayer : Layer
    {
        private string rasterRefrencePathFileName;
        private STRtree<string> spatialIndex;
        private double upperScale;
        private double lowerScale;
        private RectangleShape boundingBox;

        private const int upperLeftXPosition = 0;
        private const int upperLeftYPosition = 2;
        private const int lowerRightXPosition = 1;
        private const int lowerRightYPosition = 3;
        private const int pathFileNamePosition = 4;

        public MultiGeoRasterLayer()
            : this(string.Empty, double.MaxValue, double.MinValue)
        { }

        public MultiGeoRasterLayer(string rasterRefrencePathFileName)
            : this(rasterRefrencePathFileName, double.MaxValue, double.MinValue)
        { }

        public MultiGeoRasterLayer(string rasterRefrencePathFileName, double upperScale, double lowerScale)
        {
            this.rasterRefrencePathFileName = rasterRefrencePathFileName;
            this.upperScale = upperScale;
            this.lowerScale = lowerScale;
            boundingBox = new RectangleShape();
        }

        public string RasterRefrencePathFileName
        {
            get { return rasterRefrencePathFileName; }
            set { rasterRefrencePathFileName = value; }
        }

        public double UpperScale
        {
            get { return upperScale; }
            set { upperScale = value; }
        }

        public double LowerScale
        {
            get { return lowerScale; }
            set { lowerScale = value; }
        }

        // Here on the OpenCore we load our reference file and build the spatial index, which will be used in the DrawCore later.  
        // You need to make sure the reference file is in the right format as described in the comments above.  
        protected override void OpenCore()
        {
            if (File.Exists(rasterRefrencePathFileName))
            {
                string[] rasterFiles = File.ReadAllLines(rasterRefrencePathFileName);
                spatialIndex = new STRtree<string>(rasterFiles.Length);

                Collection<BaseShape> boundingBoxes = new Collection<BaseShape>();

                foreach (string rasterLine in rasterFiles)
                {
                    string[] parts = rasterLine.Split(new string[] { "," }, StringSplitOptions.None);
                    RectangleShape rasterBoundingBox = new RectangleShape(new PointShape(double.Parse(parts[upperLeftXPosition]), double.Parse(parts[upperLeftYPosition])), new PointShape(double.Parse(parts[lowerRightXPosition]), double.Parse(parts[lowerRightYPosition])));
                    boundingBoxes.Add(rasterBoundingBox);

                    Envelope envelope = new Envelope(double.Parse(parts[upperLeftXPosition]), double.Parse(parts[lowerRightXPosition]), double.Parse(parts[upperLeftYPosition]), double.Parse(parts[lowerRightYPosition]));
                    spatialIndex.Insert(envelope, parts[pathFileNamePosition]);
                }
                spatialIndex.Build();
                boundingBox = ExtentHelper.GetBoundingBoxOfItems(boundingBoxes);
            }
            else
            {
                throw new FileNotFoundException("The Raster reference file could not be found.", rasterRefrencePathFileName);
            }
        }

        // Here we set the spatial index to null to clean up the memory and get ready for serialization  
        protected override void CloseCore()
        {
            spatialIndex = null;
        }

        // When we get to the Draw, things are easy.  First we check to make sure we are within our scales.  
        // Next we look up the Raster files in the spatial index,  
        // then open their layer, call their Draw and close them.  
        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            double currentScale = ExtentHelper.GetScale(canvas.CurrentWorldExtent, canvas.Width, canvas.MapUnit);

            if (currentScale >= lowerScale && currentScale <= upperScale)
            {
                RectangleShape currentExtent = canvas.CurrentWorldExtent;
                Envelope currentExtentEnvelope = new Envelope(currentExtent.UpperLeftPoint.X, currentExtent.LowerRightPoint.X, currentExtent.UpperLeftPoint.Y, currentExtent.LowerRightPoint.Y);
                IList<string> rasters = spatialIndex.Query(currentExtentEnvelope);

                foreach (string file in rasters)
                {
                    NativeImageRasterLayer rasterRasterLayer = new NativeImageRasterLayer(file);
                    rasterRasterLayer.Open();
                    rasterRasterLayer.Draw(canvas, labelsInAllLayers);
                    rasterRasterLayer.Close();
                }
            }
        }

        // Here we let everyone know we support having a bounding box  
        public override bool HasBoundingBox
        {
            get
            {
                return true;
            }
        }

        //  We use the cached bounding box we set in the OpenCore  
        protected override RectangleShape GetBoundingBoxCore()
        {
            return boundingBox;
        }

        // This is just a handy function to build a reference file from a directory.  
        // You can tailor this code to fit your needs.  
        public static void BuildReferenceFile(string newReferencepathFileName, string pathOfRasterFiles)
        {
            if (Directory.Exists(pathOfRasterFiles))
            {
                string[] files = Directory.GetFiles(pathOfRasterFiles, "*.jpg");
                StreamWriter streamWriter = null;

                try
                {
                    streamWriter = File.CreateText(newReferencepathFileName);

                    foreach (string file in files)
                    {
                        NativeImageRasterLayer rasterRasterLayer = new NativeImageRasterLayer(file);
                        rasterRasterLayer.Open();
                        RectangleShape boundingBox = rasterRasterLayer.GetBoundingBox();
                        rasterRasterLayer.Close();
                        streamWriter.WriteLine(string.Format("{0},{1},{2},{3},{4}", boundingBox.UpperLeftPoint.X, boundingBox.LowerRightPoint.X, boundingBox.UpperLeftPoint.Y, boundingBox.LowerRightPoint.Y, file));
                    }
                    streamWriter.Close();
                }
                finally
                {
                    if (streamWriter != null) { streamWriter.Dispose(); }
                }
            }
            else
            {
                throw new DirectoryNotFoundException("The path containing the Raster files could not be found.");
            }
        }
    }
}