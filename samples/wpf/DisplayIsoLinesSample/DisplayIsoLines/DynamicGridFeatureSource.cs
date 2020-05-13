namespace ThinkGeo.MapSuite.Layers
{
    public class DynamicGridFeatureSource : InMemoryGridFeatureSource
    {
        public DynamicGridFeatureSource()
            : base()
        { }

        protected override bool IsOpenCore
        {
            get
            {
                return true;
            }
        }
    }
}