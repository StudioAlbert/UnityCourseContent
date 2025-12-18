using System;
using UnityEngine;

namespace Singletons
{
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance;

        public static bool HasInstance => instance != null;
        public static T TryGetInstance => HasInstance ? instance : null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        var go = new GameObject("new singleton generated");
                        instance = go.AddComponent<T>();
                    }
                }

                return instance;
                
            }
        }

        protected void Awake()
        {
            Initialize();
        }


        private void Initialize()
        {
            if(!Application.isPlaying) return;

            instance = this as T;
        } 

    }
}
