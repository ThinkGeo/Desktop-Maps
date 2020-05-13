/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace LabelingFlightLines
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.CurrentExtent = new RectangleShape(-14083954.1012604, 30830158.6559763, 5640468.1736728, -4729738.90041646);
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            map.Overlays.Add(backgroundOverlay);

            //Create a layer to display oll of the flight points and lines
            LayerOverlay overlay = new LayerOverlay();
            InMemoryFeatureLayer flightsLayer = new InMemoryFeatureLayer();
            flightsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.LightGreen, 2));
            flightsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(), new GeoPen(GeoColors.Black, 1.5f), 7);
            flightsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            overlay.Layers.Add("FlightsLayer", flightsLayer);
            map.Overlays.Add("FlightsOverlay", overlay);

            //Create a layer for the popups
            map.Overlays.Add("PopupOverlay", new PopupOverlay());

            InitializeData();
        }

        private void InitializeData()
        {
            Collection<Flight> flights = new Collection<Flight>();
            InMemoryFeatureLayer flightsLayer = (InMemoryFeatureLayer)((LayerOverlay)map.Overlays["FlightsOverlay"]).Layers["FlightsLayer"];
            flightsLayer.FeatureSource.Projection = new Proj4Projection(4326, 3857);

            //Parse the data out of the csv file
            string[] flightsSource = File.ReadAllLines("./AppData/SampleData.csv", Encoding.ASCII);
            foreach (var item in flightsSource.Skip(1))
            {
                string[] cell = item.Split(',');
                Flight flight = new Flight
                {
                    Name = cell[0],
                    FlightNumber = cell[1],
                    Date = cell[2],
                    FlightTime = cell[3],
                    OriginAddress = cell[4],
                    DestinationAddress = cell[10]
                };

                string[] originLocation = cell[5].Split(' ');
                flight.OriginLocation = new PointShape(Convert.ToDouble(originLocation[1]), Convert.ToDouble(originLocation[0]));
                string[] destinationLocation = cell[11].Split(' ');
                flight.DestinationLocation = new PointShape(Convert.ToDouble(destinationLocation[1]), Convert.ToDouble(destinationLocation[0]));

                double offset = 0;
                foreach (var existingFlight in flights)
                {
                    //If the flight already exists we offset the crest of the arc by an ammount to seperate the lines so that both are visible
                    if (flight.OriginLocation.Equal2D(existingFlight.OriginLocation) && flight.DestinationLocation.Equal2D(existingFlight.DestinationLocation))
                    {
                        offset += flight.OriginLocation.GetDistanceTo(flight.DestinationLocation, GeographyUnit.DecimalDegree, DistanceUnit.Mile) / 3500;
                    }
                }

                //get arc to draw for flight
                MultilineShape flightLine = flight.GetArc(offset);

                flight.FlightLineShape = flightLine.Lines[0];
                flight.FlightLineCenter = flightLine.GetPointOnALine(StartingPoint.FirstPoint, 50f);
                flightsLayer.InternalFeatures.Add(new Feature(flight.OriginLocation));
                flightsLayer.InternalFeatures.Add(new Feature(flight.DestinationLocation));
                flightsLayer.InternalFeatures.Add(new Feature(flight.FlightLineShape));
                flights.Add(flight);
            }

            FlightsList.ItemsSource = flights;
        }

        private void DisplayFlightsPopup(object sender, RoutedEventArgs e)
        {
            //Create a popup for the origin, destination, and flight line the flight when the radio button is clicked
            RadioButton radioButton = (RadioButton)sender;
            Flight flight = (Flight)radioButton.DataContext;

            PopupOverlay popupOverlay = (PopupOverlay)map.Overlays["PopupOverlay"];
            popupOverlay.Popups.Clear();

            Proj4Projection proj4 = new Proj4Projection(4326, 3857);
            proj4.Open();
            var originLocation = (PointShape)proj4.ConvertToExternalProjection(flight.OriginLocation);
            Popup originPopup = new Popup(originLocation) { Content = new FlightsContent(flight, false) };
            popupOverlay.Popups.Add(originPopup);

            var destinationLocation = (PointShape)proj4.ConvertToExternalProjection(flight.DestinationLocation);
            Popup destinationPopup = new Popup(destinationLocation) { Content = new FlightsContent(flight, false) };
            popupOverlay.Popups.Add(destinationPopup);

            var flightLineCenter = (PointShape)proj4.ConvertToExternalProjection(flight.FlightLineCenter);
            Popup flightLinePopup = new Popup(flightLineCenter) { Content = new FlightsContent(flight, true) };
            popupOverlay.Popups.Add(flightLinePopup);

            map.Refresh();
        }
    }
}
