using System;
using UIFrame.Event;
using Unity.VisualScripting;

namespace Core
{
    public class PlayerEvent : EventCenter, IMonoControl
    {
        private static PlayerEvent _instance;

        public static PlayerEvent Instance
        {
            get { return _instance ??= new PlayerEvent(); }
        }

        public void MonoInit()
        {
            
        }

        public void MonoUpdate()
        {
        }

        public void MonoDestory()
        {
            OnDestroy();
        }
    }
}