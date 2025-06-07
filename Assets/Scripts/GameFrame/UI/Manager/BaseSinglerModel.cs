using UnityEngine;

namespace UIFrame.Manager
{
    public interface IBaseSinglerModel<T> where T : new()
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
    public class BaseSinglerModel<T> where T : new()
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
        public virtual void Print()
        {
            Debug.Log($"{this.GetType().Name} Print");
        }
    }
}