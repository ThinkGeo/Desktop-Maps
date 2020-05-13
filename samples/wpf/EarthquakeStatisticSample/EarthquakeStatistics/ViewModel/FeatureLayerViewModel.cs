using ThinkGeo.MapSuite.Layers;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class FeatureLayerViewModel : ViewModelBase
    {
        private FeatureLayer featureLayer;

        public FeatureLayerViewModel()
            : this(null)
        { }

        public FeatureLayerViewModel(FeatureLayer featureLayer)
            : this(featureLayer, false)
        {
        }

        public FeatureLayerViewModel(FeatureLayer featureLayer, bool isVisible)
        {
            FeatureLayer = featureLayer;
            IsVisible = isVisible;
        }

        public bool IsVisible
        {
            get { return FeatureLayer.IsVisible; }
            set
            {
                FeatureLayer.IsVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public FeatureLayer FeatureLayer
        {
            get { return featureLayer; }
            set
            {
                featureLayer = value;
                RaisePropertyChanged(() => FeatureLayer);
            }
        }

        public string Name
        {
            get { return featureLayer != null ? featureLayer.Name : string.Empty; }
        }
    }
}