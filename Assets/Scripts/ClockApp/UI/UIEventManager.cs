using UIFrame.Event;

namespace ClockApp.UI
{
    public class UIEventManager : EventCenter
    {
        private static UIEventManager _instance;

        public static UIEventManager Instance
        {
            get
            {
                return _instance ??= new UIEventManager();
            }
        }
        
        
        
    }
}