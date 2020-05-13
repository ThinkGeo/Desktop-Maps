using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;

namespace TrackZoomInWithoutShiftKey
{
    class ModeInteractiveOverlay: ExtentInteractiveOverlay
    {
        public enum Mode { TrackZoomIn,Pan };
        private Mode mode;
        public ModeInteractiveOverlay() 
        {
            //Sets default mode to TrackZoomIn.
            base.PanMode = MapPanMode.Disabled;
            base.LeftClickDragMode = MapLeftClickDragMode.ZoomInWithKey;
            base.LeftClickDragKey = Keys.None;
            mode = Mode.TrackZoomIn;
        }

        public Mode MapMode
        {
            get { return mode; }
            set { 
                    mode = value;
                    if (mode == Mode.TrackZoomIn)
                    {
                        base.PanMode = MapPanMode.Disabled;
                        base.LeftClickDragMode = MapLeftClickDragMode.ZoomInWithKey;
                        base.LeftClickDragKey = Keys.None;
                    }
                    else if (mode == Mode.Pan)
                    {
                        base.PanMode = MapPanMode.StandardPanning;
                        base.LeftClickDragMode = MapLeftClickDragMode.Disabled;
                        base.LeftClickDragKey = Keys.None;
                    }
                }
        }

        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
        {
            InteractiveResult result = null;
            result = base.MouseDownCore(interactionArguments);
            base.PanAndTrackZoomState.IsLeftClickDragKeyPressed = true;
            return result;
        }
    }
   
}
