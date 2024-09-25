// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private GameObject _prefab;
    private IObjectPool<GameObject> _pool;
    private Transform _root;
    
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    Transform Root
    {
        get
        {
            if (_root == null)
            {
                GameObject go = new GameObject() { name = $"{_prefab.name}_Root" };
                _root = go.transform;
            }

            return _root;
        }

    }
    
    // --------------------------------------------------
    // Constructor
    // --------------------------------------------------
    public Pool(GameObject prefab)
    {
        _prefab = prefab;
        _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }
    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(_prefab);
        go.transform.parent = Root;
        go.name = _prefab.name;
        return go;
    }
    
    public void OnGet(GameObject go)
    {
        go.SetActive(true);
    }
    
    public void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }
    
    public void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }

    public GameObject Pop()
    {
        return _pool.Get();
    }

    public void Push(GameObject go)
    {
        _pool.Release(go);
    }
}


public class PoolManager
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    // ----- Private
    private void CreatePool(GameObject prefab)
    {
        Pool pool = new Pool(prefab);
        _pools.Add(prefab.name, pool);
    }
    
    // ----- Public
    public GameObject Pop(GameObject prefab)
    {
        if (_pools.ContainsKey(prefab.name) == false)
            CreatePool(prefab);
        return _pools[prefab.name].Pop();
    }

    public bool Push(GameObject go)
    {
        if (_pools.ContainsKey(go.name) == false)
            return false;
        
        _pools[go.name].Push(go);
        return true;
    }
}