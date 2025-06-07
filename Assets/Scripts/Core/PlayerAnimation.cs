using UnityEngine;

namespace Core
{
    public class PlayerAnimation : IMonoControl
    {
        private Animator _animator;

        public PlayerAnimation(Animator _animator)
        {
            SetAnimator(_animator);
        }

        public void MonoInit()
        {
            Debug.Log($"{_animator.name}");
        }

        public void MonoUpdate()
        {
        }

        public void MonoDestory()
        {
            
        }

        public void SetAnimator(Animator _animator)
        {
            this._animator = _animator;
        }
    }
}