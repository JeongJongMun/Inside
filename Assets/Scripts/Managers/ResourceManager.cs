// ----- C#
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private Dictionary<string, Object> _global = new();
    private Dictionary<string, Object> _stage = new();

    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public T Load<T>(string key) where T : Object
    {
        if (_global.TryGetValue(key, out Object resource))
            return resource as T;
        
        if (_stage.TryGetValue(key, out Object resourceStage))
            return resourceStage as T;
        
        Debug.LogError("[ResourceManager.Load] 리소스를 로드하지 못했습니다.");
        
        return null;
    }


    public void LoadAsync<T>(string key, Define.ELoadType loadType = Define.ELoadType.Stage, Action<T> doneCallback = null) where T : Object
    { 
        if (_global.TryGetValue(key, out Object resourceGlobal))
        {
            doneCallback?.Invoke(resourceGlobal as T);
            return;
        }

        if (_stage.TryGetValue(key, out Object resourceStage))
        {
            doneCallback?.Invoke(resourceStage as T);
            return;
        }
        
        var asynceOperation =  Addressables.LoadAssetAsync<T>(key);
        if (loadType == Define.ELoadType.Global)
        {
            asynceOperation.Completed += (handler) =>
            {
                _global.Add(key, handler.Result);
                doneCallback?.Invoke(handler.Result);
            };
        }
        else if (loadType == Define.ELoadType.Stage)
        {
            asynceOperation.Completed += (handler) =>
            {
                _stage.Add(key, handler.Result);
                doneCallback?.Invoke(handler.Result);
            };
        }
    }

    /// <summary>
    /// 같은 라벨의 모든 객체를 로드하는 메서드
    /// </summary>
    /// <param name="label">어드레서블 라벨</param>
    /// <param name="callback">(key, loadcount, totalcount)</param>
    /// <typeparam name="T">Object</typeparam>
    public void LoadAllAsync<T>(string label, Define.ELoadType loadType = Define.ELoadType.Stage, Action<string, int , int> callback = null) where T : Object
    {
        var asynceOperation =  Addressables.LoadResourceLocationsAsync(label,typeof(T));
        asynceOperation.Completed += (handler) =>
        {
            int loadcount = 0;
            int totalCount = handler.Result.Count;

            foreach (var result in handler.Result)
            {
                LoadAsync<T>(result.PrimaryKey,loadType,(obj) =>
                {
                    loadcount++;
                    callback?.Invoke(result.PrimaryKey, loadcount, totalCount);
                });
            }
        };
    }
    
    public void ReleaseStage()
    {
        foreach (var pair in _stage)
        {
            Addressables.Release(pair.Value);
        }
        _stage.Clear();
    }
    
    public GameObject Instantiate(string path,Transform parent = null,bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"Assets/@Resources/Prefabs/{path}");
        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager.Instantiate] 프리팹을 생성하지 못하였습니다. : {path}");
            return null;
        }

        if (pooling)
            return Managers.Pool.Pop(prefab);
        
        GameObject go = Object.Instantiate(prefab,parent);
        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go ==null)
            return;
        if (Managers.Pool.Push(go))
            return;
        Object.Destroy(go);
    }
}