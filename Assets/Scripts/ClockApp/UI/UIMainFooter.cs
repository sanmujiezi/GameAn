using ClockApp.UI.Event;
using UIFrame.Core;
using UIFrame.Event;
using UIFrame.Manager;
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
                _preSelected = _curSelected;
                
                if (_preSelected != null)
                {
                    _preSelected.SetSelectState(false);
                }

                _curSelected = value;
                _curSelected.SetSelectState(true);
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
            
            UIEventManager.Instance.Subscribe<UIMainFooterChangeEvent>(SendChangeEvent);
        }


        private void OnClockButtonClick()
        {
            UIEventManager.Instance.Publish(new UIMainFooterChangeEvent(){Type = MainFooterType.Clock});
            Debug.Log($"ClockButtonClick {_curSelected == _clockButton}");
        }

        private void OnHistoryButtonClick()
        {
            UIEventManager.Instance.Publish(new UIMainFooterChangeEvent(){Type = MainFooterType.History});
            Debug.Log($"ClockButtonClick {_curSelected == _clockButton}");
        } 

        private void SendChangeEvent(UIMainFooterChangeEvent message)
        {
            if (message.Type == MainFooterType.Clock)
            {
                CurSelected = _clockButton;
            }
            else if (message.Type == MainFooterType.History)
            {
                CurSelected = _historyButton;
            }
        }
    }
}