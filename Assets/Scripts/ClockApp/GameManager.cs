using System.Collections;
using System.Collections.Generic;
using ClockApp.PlayerData;
using ClockApp.UI;
using ClockApp.UI.Event;
using UIFrame.Manager;
using UnityEngine;
using UniFramework.Event;
using YooAsset;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }
    
    private PlayerInfo _playerInfo;

    /// <summary>
    /// 协程启动器
    /// </summary>
    public MonoBehaviour Behaviour;


    private GameManager()
    {
        // 注册监听事件
        UIEventManager.Instance.Subscribe<UIMainFooterChangeEvent>(OnHandleEventMessage);
        UIEventManager.Instance.Subscribe<UIStartAppEvent>(OnStartApp);
        UIEventManager.Instance.Subscribe<LoadingDataEvent>(OnLoadingData);
        UIEventManager.Instance.Subscribe<AddTypeItemEvent>(OnAddTypeItem);
    }

    private void OnAddTypeItem(AddTypeItemEvent obj)
    {
        if (_playerInfo.timeInfo.ContainsKey(obj.TypeName))
        {
            UIEventManager.Instance.Publish(new AddTypeItemFailEvent(){msg = "类型已存在"});
            return;
        }
        _playerInfo.timeInfo.Add(obj.TypeName, 0);
        UIEventManager.Instance.Publish(new AddTypeItemSuccessEvent());
    }

    private void OnLoadingData(LoadingDataEvent obj)
    {
        //自动加载数据
        _playerInfo = PlayerDataManager.Instance.PlayerInfo;
    }

    /// <summary>
    /// 开启一个协程
    /// </summary>
    public void StartCoroutine(IEnumerator enumerator)
    {
        Behaviour.StartCoroutine(enumerator);
    }

    
    
    /// <summary>
    /// 接收事件
    /// </summary>
    private void OnHandleEventMessage(UIMainFooterChangeEvent message)
    {
        if (message.Type == MainFooterType.Clock)
        {
            UIManager.Instance.ShowWindow<UITimerMain>();
        }
        else if (message.Type == MainFooterType.History)
        {
            UIManager.Instance.HideWindow<UITimerMain>();
        }
    }

    private void OnStartApp(UIStartAppEvent message)
    {
        UIManager.Instance.ShowWindow<UIMainFooter>();
    }
}