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
    
    public struct UIStartAppEvent : IEventData{}
    public struct LoadingDataEvent : IEventData{}

    public struct AddTypeItemEvent : IEventData
    {
        public string TypeName;
    }

    public struct AddTypeItemSuccessEvent : IEventData
    {

    }

    public struct AddTypeItemFailEvent : IEventData
    {
        public string msg;
    }
    
}