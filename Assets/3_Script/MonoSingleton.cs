using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static bool shuttingdown = false;
    private static T instance = null;
    private static object locker = new object();
    public static T Instance
    {
        get
        {
            if(shuttingdown)
            {
                Debug.LogWarning("[MonoSingleton] Instance " + typeof(T) + "already destroyed. Returning null. ");
                return null;
            }
            lock (locker)
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                }
                return instance;
            }
        }
    }
    private void OnApplicationQuit() {
        shuttingdown = true;
    }
    private void OnDestroy() {
        shuttingdown = true;
    }
}
