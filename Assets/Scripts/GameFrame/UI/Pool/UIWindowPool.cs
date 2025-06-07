using System.Collections.Generic;
using UIFrame.Core;
using UIFrame.Manager;
using UnityEngine;

namespace UIFrame.Pool
{
    public class UIWindowPool : BaseSinglerModel<UIWindowPool>,IBasePool<GameObject>
    {
        private Transform _uiPool;
        public UIWindowPool() : base()
        {
            _uiPool = GameObject.Find("UIPool").GetComponent<Transform>();
        }

        public Dictionary<string, GameObject> _pool { get; set; }

        public bool IsEmpty()
        {
            return _pool.Count <= 0;
        }

        public void Push(string name, GameObject obj)
        {
            if (_pool.ContainsKey(name))
            {
                GameObject.Destroy(obj); 
                Debug.Log($"{name} already in pool");
                return;
            }
            _pool.Add(name, obj);
            obj.transform.SetParent(_uiPool);
        }

        public GameObject Pop(string name)
        {
            if (IsEmpty())
            {
                return null;
            }

            GameObject window = null;
            if (_pool.TryGetValue(name, out window))
            {
                _pool.Remove(name);
                return window;
            }
            
            GameObject windowPrefab = LoadPrefab($"Prefabs/UI/{name}");
            window = GameObject.Instantiate(windowPrefab);
            return window;
        }

        private GameObject LoadPrefab(string path)
        {
            Debug.Log($"{path}");
            return Resources.Load<GameObject>(path);
        }
    }
}