// ----- C#
using System;
using System.Collections;

// ----- Unity
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static MonoBehaviour monoInstance;
        
    public static void Init(Action callback = null)
    {
        if (monoInstance != null)
        {
            callback?.Invoke();
            return;
        }
            
        monoInstance = new GameObject($"[{nameof(CoroutineHelper)}]")
            .AddComponent<CoroutineHelper>();
        DontDestroyOnLoad(monoInstance.gameObject);
        callback?.Invoke();
    }

    public new static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }

    public new static void StopCoroutine(Coroutine coroutine)
    {
        monoInstance.StopCoroutine(coroutine);
    }
}