using System.Collections.Generic;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class MenuModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public List<MenuModel> Children { get; set; }
    }
}
