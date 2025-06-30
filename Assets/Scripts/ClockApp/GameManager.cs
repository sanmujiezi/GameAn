using System.Collections;
using System.Collections.Generic;
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
    

    /// <summary>
    /// 协程启动器
    /// </summary>
    public MonoBehaviour Behaviour;


    private GameManager()
    {
        // 注册监听事件
        UIEventManager.Instance.Subscribe<UIMainFooterChangeEvent>(OnHandleEventMessage);
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
            UIManager.Instance.ShowWindow<UIMainFooter>();
            UIManager.Instance.ShowWindow<UITimerMain>();
        }
        else if (message.Type == MainFooterType.History)
        {
            
        }
    }
}