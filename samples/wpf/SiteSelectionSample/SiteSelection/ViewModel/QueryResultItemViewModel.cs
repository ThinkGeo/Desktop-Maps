using System;
using System.Linq;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class QueryResultItemViewModel : ViewModelBase
    {
        private static readonly double zoomLevel19Scale = 2252.9288864135742;

        private string name;
        private bool canZoomToFeature;
        private Feature feature;
        private MapModel mapModel;
        private CommandBase zoomToFeatureCommand;

        public QueryResultItemViewModel(string displayName)
            : base()
        {
            Name = displayName;
            CanZoomToFeature = false;
        }

        public QueryResultItemViewModel(Feature feature, MapModel mapModel)
            : base()
        {
            this.CanZoomToFeature = true;
            this.feature = feature;
            this.mapModel = mapModel;
            this.name = feature.ColumnValues.FirstOrDefault(item => item.Key.Equals("NAME", StringComparison.InvariantCultureIgnoreCase)).Value;
        }

        public Feature Feature
        {
            get { return feature; }
            set { feature = value; }
        }

        public bool CanZoomToFeature
        {
            get { return canZoomToFeature; }
            set
            {
                canZoomToFeature = value;
                RaisePropertyChanged(() => CanZoomToFeature);
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public CommandBase ZoomToFeatureCommand
        {
            get
            {
                return zoomToFeatureCommand ?? (zoomToFeatureCommand = new CommandBase(()
                    => mapModel.MapControl.ZoomTo(feature.GetShape().GetCenterPoint(), zoomLevel19Scale)));
            }
        }
    }
}