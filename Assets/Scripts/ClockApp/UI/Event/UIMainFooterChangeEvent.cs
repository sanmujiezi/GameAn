using UIFrame.Event;

namespace ClockApp.UI.Event
{
    public enum MainFooterType
    {
        Clock,
        History
    }
    
    public struct UIMainFooterChangeEvent : IEventData
    {
        public MainFooterType Type;
    }
    
    public struct UICloseLoadingEvent : IEventData{}
}