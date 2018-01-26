using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singletonクラス
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)Object.FindObjectOfType<T>();
            }

            return instance;
        }

    }

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
    }
}
