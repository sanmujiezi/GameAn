using System;
using System.Collections.Generic;
using ClockApp.PlayerData;
using ClockApp.UI.Event;
using TMPro;
using UIFrame.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp.UI
{
    public class UITimerMain : UIBaseWindow
    {
        [Header("Binding")] public GameObject TypeGrouop;
        public GameObject HistoryGroup;
        public Button AddTypeButton;
        public GameObject TemplateTypeItem;
        public GameObject TemplateHistoryItem;
        public GameObject AddTypeBox;

        private PlayerInfo _playerInfo;
        private List<GameObject> _typeList;
        private List<GameObject> _historyList;
        private AddTypeBoxWindow _addTypeBoxWindow;

        private class ItemInfo
        {
            public string typeName;
            public float time;
        }

        private class AddTypeBoxWindow
        {
            private GameObject _rootGameObject;
            private Button _closeButton;
            private TMP_InputField _inputField;
            private Button _sureButton;

            public AddTypeBoxWindow(GameObject rootGameObject)
            {
                _rootGameObject = rootGameObject;
                _closeButton = _rootGameObject.transform.Find("Root/BaseView/TopGroup/m_CloseButton").GetComponent<Button>();
                _inputField = _rootGameObject.transform.Find("Root/BaseView/m_InputField").GetComponent<TMP_InputField>();
                _sureButton = _rootGameObject.transform.Find("Root/BaseView/m_SureButton").GetComponent<Button>();
                _closeButton.onClick.AddListener(OnCloseButtonClick);
                _sureButton.onClick.AddListener(OnSureButtonClick);
            }

            public void OnDestory()
            {
                _closeButton.onClick.RemoveListener(OnCloseButtonClick);
                _sureButton.onClick.RemoveListener(OnSureButtonClick);
            }

            public void OpenWindow()
            {
                _rootGameObject.SetActive(true);
            }

            public void CloseWindow()
            {
                _rootGameObject.SetActive(false);
            }

            private void OnSureButtonClick()
            {
                string msg = GetInputText();
                UIEventManager.Instance.Publish(new AddTypeItemEvent(){TypeName = msg});
                CloseWindow();
                Debug.Log($"{msg}");
            }

            private void OnCloseButtonClick()
            {
                CloseWindow();
            }

            public string GetInputText()
            {
                return _inputField.text;
            }
        }


        protected override void Awake()
        {
            base.Awake();
            _typeList = new List<GameObject>();
            _historyList = new List<GameObject>();
            _addTypeBoxWindow = new AddTypeBoxWindow(AddTypeBox.gameObject);
        }

        protected override void InitializedWindow()
        {
            base.InitializedWindow();
            TemplateTypeItem.SetActive(false);
            TemplateHistoryItem.SetActive(false);
            
            AddTypeButton.onClick.AddListener(()=>{_addTypeBoxWindow.OpenWindow();});
            UIEventManager.Instance.Subscribe<AddTypeItemSuccessEvent>(OnAddTypeItemSuccess);
            
            _playerInfo = PlayerDataManager.Instance.PlayerInfo;

            ClearTypeItem();
            ClearHistoryItem();
            
            UpdateContent();
        }

        private void OnAddTypeItemSuccess(AddTypeItemSuccessEvent obj)
        {
            UpdateContent();
        }

        protected void OnDestory()
        {
            _addTypeBoxWindow.OnDestory();
            AddTypeButton.onClick.RemoveAllListeners();
            UIEventManager.Instance.Unsubscribe<AddTypeItemSuccessEvent>(OnAddTypeItemSuccess);
        }

        private void UpdateContent()
        {
            Debug.Log("刷新了内容");
            foreach (var VARIABLE in _playerInfo.timeInfo)
            {
                ItemInfo info = new ItemInfo() { typeName = VARIABLE.Key, time = VARIABLE.Value };
                AddTypeItem(info);
                //AddHistoryItem(info);
            }
        }

        private void AddTypeItem(ItemInfo info)
        {
            if (TemplateTypeItem == null)
            {
                Debug.LogError("TemplateTypeItem is null");
                return;
            }

            var item = Instantiate(TemplateTypeItem, TypeGrouop.transform);
            SetTypeItemData(item, info);
            item.SetActive(true);
        }

        private void AddHistoryItem(ItemInfo info)
        {
            if (TemplateHistoryItem == null)
            {
                Debug.LogError("TemplateHistoryItem is null");
                return;
            }

            var item = Instantiate(TemplateHistoryItem, HistoryGroup.transform);
            SetTypeItemData(item, info);
            item.SetActive(true);
        }

        private void SetTypeItemData(GameObject item, ItemInfo info)
        {
            var text_C = item.transform.Find("Root/m_TypeText").GetComponent<TextMeshProUGUI>();
            text_C.text = info.typeName;
        }

        private void SetHistoryData(GameObject item, ItemInfo info)
        {
            var typeText_C = item.transform.Find("Root/m_TypeText").GetComponent<TextMeshProUGUI>();
            var timeText_C = item.transform.Find("Root/m_TimeText").GetComponent<TextMeshProUGUI>();
            typeText_C.text = info.typeName;

            TimeSpan timeSpan = TimeSpan.FromSeconds(info.time);
            timeText_C.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void ClearTypeItem()
        {
            if (TemplateTypeItem == null)
            {
                Debug.LogError("TemplateTypeItem is null");
                return;
            }

            for (int i = TypeGrouop.transform.childCount - 1; i >= 1; i--)
            {
                Destroy(TypeGrouop.transform.GetChild(i).gameObject);
            }
        }

        private void ClearHistoryItem()
        {
            if (TemplateHistoryItem == null)
            {
                Debug.LogError("TemplateHistoryItem is null");
                return;
            }

            for (int i = HistoryGroup.transform.childCount - 1; i >= 1; i--)
            {
                Destroy(HistoryGroup.transform.GetChild(i).gameObject);
            }
        }
    }
}