using UnityEngine;

namespace Core
{
    public class PlayerInput : IMonoControl
    {
        private PlayerInputSystem _inputSystem;
        
        public PlayerInput()
        {
            MonoInit();
        }
        
        public void MonoInit()
        {
            _inputSystem = new PlayerInputSystem();
            _inputSystem.Player.Enable();
        }

        public void MonoUpdate()
        {
            PlayerMoveEvent moveEvent = null;
            moveEvent.moveDir = GetMoveInput();
            PlayerEvent.Instance.Publish(moveEvent);
        }

        public void MonoDestory()
        {
            _inputSystem.Player.Disable();
        }

        public Vector2 GetMoveInput()
        {
            return _inputSystem.Player.Move.ReadValue<Vector2>().normalized;
        }
    }
}