using System.Windows;
using System.Windows.Controls;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class CategoryItemTemplateSelector : DataTemplateSelector
    {
        private DataTemplate customDataTemplate;
        private DataTemplate defaultDataTemplate;

        public CategoryItemTemplateSelector()
            : base()
        { }

        public DataTemplate CustomDataTemplate
        {
            get { return customDataTemplate; }
            set { customDataTemplate = value; }
        }

        public DataTemplate DefaultDataTemplate
        {
            get { return defaultDataTemplate; }
            set { defaultDataTemplate = value; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return item is CustomDataCategoryViewModel ? CustomDataTemplate : DefaultDataTemplate;
        }
    }
}