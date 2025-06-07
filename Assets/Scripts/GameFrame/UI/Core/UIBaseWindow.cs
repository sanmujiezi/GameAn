using System;
using System.Collections;
using UIFrame.Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


namespace UIFrame.Core
{
    public enum WindowOpenType
    {
        Alpha,
        Animation
    }

    public enum WindowType
    {
        Static,
        Dynamic
    }

    public class UIBaseWindow : MonoBehaviour
    {
        public WindowOpenType openType = WindowOpenType.Alpha;
        public WindowType windowType = WindowType.Dynamic;
        protected float _alphaSpeed = 7f;
        private CanvasGroup _canvasGroup;
        private RectTransform _rootNode;
        private Animator _animator;
        private UnityAction _showCallback;
        private UnityAction _hideCallback;
        private bool _isOpen;
        private bool _toOpen;
        
        

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _rootNode = transform.Find("Root").GetComponent<RectTransform>();
            _canvasGroup = _rootNode.GetComponent<CanvasGroup>();
            if (!_canvasGroup)
            {
                _canvasGroup = _rootNode.AddComponent<CanvasGroup>();
            }
        }

        protected virtual void Start()
        {
            InitializedWindow();
        }

        protected virtual void Update()
        {
            if (!_isOpen && _canvasGroup.alpha < 1 && _toOpen)
            {
                _canvasGroup.alpha += Time.deltaTime * _alphaSpeed;
                //Debug.Log("1");
            }
            else if (_isOpen && _canvasGroup.alpha > 0 && !_toOpen)
            {
                _canvasGroup.alpha -= Time.deltaTime * _alphaSpeed;
                //Debug.Log("2");
            }

            if (!_isOpen && _canvasGroup.alpha >= 1)
            {
                _isOpen = true;
                _canvasGroup.alpha = 1;
                _showCallback?.Invoke();
                //Debug.Log("1_1");
            }else if (_isOpen && _canvasGroup.alpha <= 0)
            {
                _isOpen = false;
                _canvasGroup.alpha = 0;
                _hideCallback?.Invoke();
                //Debug.Log("2_1");
            }
        }

        public void ShowWindow(UnityAction action = null)
        {
            if (_isOpen)
            {
                return;
            }

            _showCallback = ()=>
            {
                action?.Invoke();
            };
            
            switch (openType)
            {
                case WindowOpenType.Alpha:
                    AlphaOpen();
                    break;
                case WindowOpenType.Animation:
                    AnimationOpen();
                    break;
            }
           
        }

        public void HideWindow(UnityAction action = null)
        {
            if (!_isOpen)
            {
                return;
            }

            _hideCallback = ()=>
            {
                Destroy(gameObject);
                action?.Invoke();
            };
            
            switch (openType)
            {
                case WindowOpenType.Alpha:
                    AlphaClose();
                    break;
                case WindowOpenType.Animation:
                    AnimationClose();
                    break;
            }
            
        }

        protected virtual void InitializedWindow()
        {
            _canvasGroup.alpha = 0;
            _isOpen = false;
        }

        private void AlphaOpen()
        {
            _toOpen = true;
        }

        private void AlphaClose()
        {
            _toOpen = false;
        }

        protected virtual void AnimationOpen()
        {
            
        }
        
        protected virtual void AnimationClose()
        {
            
        }
    }
}