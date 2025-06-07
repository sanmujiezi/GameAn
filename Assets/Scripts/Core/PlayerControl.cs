using UnityEngine;

namespace Core
{
    public class PlayerControl : IMonoControl
    {
        private PlayerManager _player;

        private bool canMove;
        private bool isRuning;
        private bool isMove;

        public PlayerControl(PlayerManager player)
        {
            _player = player;
        }

        public void MonoInit()
        {
            PlayerEvent.Instance.Subscribe<PlayerMoveEvent>(OnMove);
        }

        public void MonoUpdate()
        {
        }

        public void MonoDestory()
        {
        }

        public void OnMove(PlayerMoveEvent moveEvent)
        {
            if (moveEvent == null)
            {
                return;
            }

            if (!canMove)
            {
                return;
            }

            float speed = moveEvent.moveDir.magnitude;
            
        }
    }
}