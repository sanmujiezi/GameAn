using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UIFrame.Event
{
    public interface IEventData
    {
    }

    public interface IEventSystem
    {
        void Subscribe<T>(Action<T> handler) where T : IEventData;
        void Unsubscribe<T>(Action<T> handler) where T : IEventData;
        void Publish<T>(T evetData) where T : IEventData;
        void Clear();
    }
    
}