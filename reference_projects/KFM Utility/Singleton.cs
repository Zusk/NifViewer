using System;
using System.Collections.Generic;
using System.Text;

namespace KFM_Utility 
{
    public abstract class Singleton<T> where T : new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
        static Singleton()
        {
            instance = new T();
        }
        protected Singleton()
        {
            Instantiate();
        }
        protected abstract void Instantiate();
        public void Clear()
        {
            Instantiate();
        }
    }
}