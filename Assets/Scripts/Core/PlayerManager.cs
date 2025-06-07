using System;
using UnityEngine;


namespace Core
{
    public class PlayerManager : MonoBehaviour
    {
        public float moveSpeed;
        
        private Animator _animator;

        private PlayerAnimation _playerAnimation;
        private PlayerInput _playerInput;
        private PlayerControl _playerControl;
        private PlayerEvent _playerEvent;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _animator = GetComponentInChildren<Animator>();
            
            CheckCompontnet();

            _playerEvent = new PlayerEvent();
            _playerEvent.MonoInit();
            _playerAnimation = new PlayerAnimation(_animator);
            _playerAnimation.MonoInit();
            _playerInput = new PlayerInput();
            _playerInput.MonoInit();
            _playerControl = new PlayerControl(this);     
            _playerControl.MonoInit();

        }
        
        private void CheckCompontnet()
        {
            if (!_animator)
            {
                Debug.LogError("PlayerAnimator not found");
            }
        }
        
        void Update()
        {
            _playerAnimation.MonoUpdate();
            _playerInput.MonoUpdate();
            _playerControl.MonoUpdate();
            _playerEvent.MonoUpdate();
        }

        private void OnDestroy()
        {
            _playerAnimation.MonoDestory();
            _playerInput.MonoDestory();
            _playerControl.MonoDestory();
            _playerEvent.MonoDestory();
        }
        
    }
}