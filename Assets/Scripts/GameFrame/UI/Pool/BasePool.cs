using System.Collections;
using System.Collections.Generic;
using UIFrame.Manager;
using UnityEngine;

namespace UIFrame.Pool
{
    public interface IBasePool<T>
    {
        public Dictionary<string, T> _pool { get; set; }
        public bool IsEmpty();
        public  void Push(string name, T obj);
        public T Pop(string name);
    }
    
}