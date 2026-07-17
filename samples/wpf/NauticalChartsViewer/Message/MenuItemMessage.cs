
namespace NauticalChartsViewer
{
    internal class MenuItemMessage
    {
        public MenuItemMessage(BaseMenuItem menuItem) 
        {
            MenuItem = menuItem;
        }

        public BaseMenuItem MenuItem { get; private set; }
    }
}
