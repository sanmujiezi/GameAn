

using UIFrame.Event;
using UnityEngine;
using UnityEngine.Events;

public static class UIDevDefine
{
    
    public interface IToggleButton
    {
        void SetSelectState(bool isSelected);
    }

    /// <summary>
    /// 快速实现业务逻辑上的toggle按钮
    /// 1.可选中状态命名 m_Selected
    /// 2.不可选中状态命名 m_UnSelected
    /// 3.根节点下必须有Root节点
    /// buttonName
    ///     Root
    ///         m_Selected
    ///         m_UnSelected
    /// </summary>
    public class DevToggleButton : IToggleButton
    {
        public bool IsSelected;
        
        
        private UnityAction _onSelected;
        
        private GameObject _button;
        private GameObject _selectedRoot;
        private GameObject _unSelectedRoot;
        
        public DevToggleButton(GameObject button)
        {
            Init(button);
        }
        
        public DevToggleButton(GameObject button,UnityAction onSelected)
        {
            Init(button);
            _onSelected = onSelected;
        }

        private void Init(GameObject button)
        {
            _button = button;
            _selectedRoot = _button.transform.Find("Root/m_Selected").gameObject;
            _unSelectedRoot = _button.transform.Find("Root/m_UnSelected").gameObject;
        }

        public void SetSelectState(bool isSelected)
        {
            IsSelected = isSelected;
            if (IsSelected)
            {
                _selectedRoot.SetActive(true);
                _unSelectedRoot.SetActive(false);
                if (_onSelected != null) _onSelected.Invoke();
            }
            else
            {
                _selectedRoot.SetActive(false);
                _unSelectedRoot.SetActive(true);
            }
        }
    }
}