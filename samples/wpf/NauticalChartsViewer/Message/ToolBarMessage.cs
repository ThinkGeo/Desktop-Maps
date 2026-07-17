
namespace NauticalChartsViewer
{
    public class ToolBarMessage
    {
        public ToolBarMessage(string action) 
        {
            Action = action;
        }
        public string Action { get; private set; }
    }
}
