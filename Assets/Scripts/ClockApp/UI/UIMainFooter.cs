using ClockApp.UI.Event;
using UIFrame.Core;
using UIFrame.Event;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp.UI
{
    public class UIMainFooter : UIBaseWindow
    {
        [Header("绑定")] public Button ClockButton;
        public Button HistoryButton;

        private UIDevDefine.DevToggleButton _clockButton;
        private UIDevDefine.DevToggleButton _historyButton;
        
        private UIDevDefine.DevToggleButton _preSelected;
        private UIDevDefine.DevToggleButton _curSelected;
        
        public UIDevDefine.DevToggleButton CurSelected
        {
            set
            {
                if (_preSelected != null)
                {
                    _preSelected.SetSelectState(false);
                }

                _preSelected = _curSelected;
                _curSelected = value;
                _curSelected.SetSelectState(true);
                
                SendChangeEvent(_curSelected);
            }
            get { return _curSelected; }
        }

        protected override void Start()
        {
            base.Start();
            _clockButton = new UIDevDefine.DevToggleButton(ClockButton.gameObject);
            _historyButton = new UIDevDefine.DevToggleButton(HistoryButton.gameObject);
            ClockButton.onClick.AddListener(OnClockButtonClick);
            HistoryButton.onClick.AddListener(OnHistoryButtonClick);
        }


        private void OnClockButtonClick()
        {
            CurSelected = _clockButton;
            Debug.Log("ClockButtonClick");
        }

        private void OnHistoryButtonClick()
        {
            CurSelected = _historyButton;
            Debug.Log("HistoryButtonClick");
        }

        private void SendChangeEvent(UIDevDefine.DevToggleButton curenToggleButton)
        {
            if (curenToggleButton == _clockButton)
            {
                UIEventManager.Instance.Publish(new UIMainFooterChangeEvent{Type = MainFooterType.Clock});
            }
            else if (curenToggleButton == _historyButton)
            {
                UIEventManager.Instance.Publish(new UIMainFooterChangeEvent{Type = MainFooterType.History});
            }
        }
    }
}