using System;
using System.Collections.Generic;

namespace UIFrame.Event
{
    public class EventCenter : IEventSystem
    {
        private readonly Dictionary<Type, Delegate> _eventHandlers = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> handler) where T : IEventData
        {
            var eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out var existingHandlers))
            {
                _eventHandlers[eventType] = Delegate.Combine(existingHandlers, handler);
            }
            else
            {
                _eventHandlers[eventType] = handler;
            }
        }


        // 取消订阅
        public void Unsubscribe<T>(Action<T> handler) where T : IEventData
        {
            var eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out var existingHandlers))
            {
                var newHandlers = Delegate.Remove(existingHandlers, handler);

                if (newHandlers == null)
                {
                    _eventHandlers.Remove(eventType);
                }
                else
                {
                    _eventHandlers[eventType] = newHandlers;
                }
            }
        }

        // 发布事件
        public void Publish<T>(T eventData) where T : IEventData
        {
            var eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out var handlers))
            {
                (handlers as Action<T>)?.Invoke(eventData);
            }
        }

        // 清除所有订阅
        public void Clear()
        {
            _eventHandlers.Clear();
        }

        // 当对象销毁时清除所有订阅
        protected void OnDestroy()
        {
            Clear();
        }
    }
}