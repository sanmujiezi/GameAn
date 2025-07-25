﻿using System;
using System.Collections;
using System.Collections.Generic;
using ClockApp.UI;
using ClockApp.UI.Event;
using UnityEngine;
using UniFramework.Event;
using YooAsset;

public class Boot : MonoBehaviour
{
    /// <summary>
    /// 资源系统运行模式
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

    public string PackageName;

    void Awake()
    {
        Debug.Log($"资源系统运行模式：{PlayMode}");
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);
    }
    IEnumerator Start()
    {
        // 游戏管理器
        GameManager.Instance.Behaviour = this;

        // 初始化事件系统
        UniEvent.Initalize();

        // 初始化资源系统
        YooAssets.Initialize();

        // 加载更新页面
        var go = Resources.Load<GameObject>("PatchWindow");
        var pathWindow = GameObject.Instantiate(go);

        // 开始补丁更新流程
        var operation = new PatchOperation(PackageName, PlayMode);
        YooAssets.StartOperation(operation);
        yield return operation;

        // 设置默认的资源包
        var gamePackage = YooAssets.GetPackage(PackageName);
        YooAssets.SetDefaultPackage(gamePackage);

        // 加载主页面主页面场景
        UIEventManager.Instance.Publish(new UIMainFooterChangeEvent(){Type = MainFooterType.Clock});
        UIEventManager.Instance.Publish(new UIStartAppEvent());
        UIEventManager.Instance.Publish(new LoadingDataEvent());
        pathWindow.gameObject.SetActive(false);
    }
}