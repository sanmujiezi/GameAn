using UIFrame.Event;
using UnityEngine;

namespace Core
{
    public interface IPlayerEventData : IEventData
    {
    }

    public abstract class PlayerMoveEvent : IEventData
    {
        public Vector2 moveDir;
    }
}