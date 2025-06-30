using System.Collections.Generic;
using UIFrame.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using YooAsset;

namespace UIFrame.Manager
{
    
    public class UIManager : BaseSinglerModel<UIManager>
    {
        private Transform _dynamicCanvas;
        private Transform _staticCanvas;
        private Dictionary<string, UIBaseWindow> _openedWindows;
        private Dictionary<string, GameObject> _buffer;

        public UIManager()
        {
            _staticCanvas = GameObject.Find("StaticCanvas").GetComponent<Transform>();
            _dynamicCanvas = GameObject.Find("DynamicCanvas").GetComponent<Transform>();
            _openedWindows = new Dictionary<string, UIBaseWindow>();
        }

        public GameObject ShowWindow<T>(UnityAction OnComplelted = null) where T : UIBaseWindow
        {
            string windowName = typeof(T).Name;
            if (_openedWindows.ContainsKey(windowName))
            {
                Debug.LogError($"{windowName} is already opened!");
                return null;
            }

            GameObject windowPrefab = LoadPrefab($"{windowName}");
            var windowScript = windowPrefab.GetComponent<T>();
            var parent = windowScript.windowType == WindowType.Static ? _staticCanvas : _dynamicCanvas;
            GameObject window = GameObject.Instantiate(windowPrefab, parent);
            windowScript = window.GetComponent<T>();
            
            ShowWindow(windowName,windowScript,OnComplelted);
            return window;
        }

        private void ShowWindow(string windowName,UIBaseWindow windowScript,UnityAction OnComplelted = null)
        {
            windowScript.ShowWindow(OnComplelted);
            _openedWindows.Add(windowName, windowScript);
        }

        public void HideWindow<T>(UnityAction OnComplelted = null)
        {
            string windowName = typeof(T).Name;
            
            CloseWindow(windowName,OnComplelted);
        }
        
        private void CloseWindow(string windowName,UnityAction OnComplelted = null)
        {
            if (!_openedWindows.ContainsKey(windowName))
            {
                Debug.LogError($"{windowName} is not open!");
                return;
            }
            var script =_openedWindows[windowName]; 
            _openedWindows.Remove(windowName);
            script.HideWindow(OnComplelted);
        }

        public void ClearAllOpenedWindows()
        {
            _openedWindows.Clear();
        }

        private GameObject LoadPrefab(string path)
        {
            Debug.Log($"{path}");
            return YooAssets.LoadAssetSync<GameObject>(path).AssetObject.GameObject();
        }
    }
}