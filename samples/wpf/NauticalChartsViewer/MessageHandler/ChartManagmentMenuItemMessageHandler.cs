using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace NauticalChartsViewer
{
    internal class ChartManagmentMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";
        private const string boundingBoxPreviewOverlayName = "BoundingBoxPreview";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            switch (message.MenuItem.Action.ToLowerInvariant())
            {
                case "loadcharts":
                    {
                        var window = new ChartsManagmentWindow();
                        window.Owner = owner;
                        window.ShowDialog();
                    }
                    break;

                case "unloadcharts":
                    {
                        if (map.Overlays.Contains(chartsOverlayName))
                        {
                            if (MessageBox.Show("Do you want to unload all the charts?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                if (map.Overlays.Contains(chartsOverlayName))
                                {
                                    var baseLayers = (map.Overlays[chartsOverlayName] as LayerOverlay).Layers;
                                    GeoCollection<Layer> layers = new GeoCollection<Layer>();

                                    foreach (var baseLayer in baseLayers)
                                    {
                                        if (baseLayer is Layer layer)
                                        {
                                            layers.Add(layer.Name, layer);
                                        }
                                    }
                                    foreach (Layer layer in layers)
                                    {
                                        layer.Close();
                                    }
                                    layers.Clear();

                                    ChartMessage chartMessage = new ChartMessage(ChartsManagmentViewModel.Instance.Charts);
                                    Messenger.Default.Send<ChartMessage>(chartMessage, "UnloadCharts");
                                    ChartsManagmentViewModel.Instance.Charts.Clear();
                                    ChartsManagmentViewModel.Instance.SelectedItem = null;
                                    ChartsManagmentViewModel.Instance.SelectedItems.Clear();
                                }

                                if (map.Overlays.Contains(boundingBoxPreviewOverlayName))
                                {
                                    ((InMemoryFeatureLayer)((LayerOverlay)map.Overlays[boundingBoxPreviewOverlayName]).Layers[0]).InternalFeatures.Clear();
                                }
                                await map.RefreshAsync();
                            }
                        }
                    }
                    break;
            }
        }

        public override string[] Actions
        {
            get { return new[] { "loadcharts", "unloadcharts" }; }
        }
    }
}