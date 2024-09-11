// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Private
    private int _totalActionCount = 0;
    private int _actionIndex = 0;
    private List<Action> _actions = new();
    private bool _isInitialized = false;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private IEnumerator Start()
    {
        SetAction();
        _totalActionCount = _actions.Count;
        StartInitialize();
        yield return null;
    }
    
    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    // ----- Action Group
    private void SetAction()
    {
        // #1 Managers Init
        _actions.Add(() =>
        {
            Debug.Log($"[GameInitializer.Action...] Action {_actionIndex + 1} Start | Managers Init");
            Managers.Init();
            InitDone();
        });
        
        // #2 Trick Init
        _actions.Add(() =>
        {
            Debug.Log($"[GameInitializer.Action...] Action {_actionIndex + 1} Start | Trick Init");
            Managers.Trick.Init();
            InitDone();
        });
    }
    private void InitDone()
    {
        Debug.Log($"[GameInitializer.InitDone] Action Success Done {_actionIndex + 1}/{_actions.Count}");
    
        _actionIndex += 1;
    
        if (_actionIndex < _actions.Count)
        {
            _actions[_actionIndex].Invoke();
        }
        else
        {
            _isInitialized = true;
        }
    }
    
    private void StartInitialize()
    {
        Debug.Log($"[GameInitializer.StartInitialize] Start Init");
        if (!_isInitialized)
        {
            _actions[_actionIndex].Invoke();
            StartCoroutine(Co_Loading());
        }
        else
        {
            OnInit();
        }
    }

    private void OnInit()
    {
        SceneManager.LoadScene("1. InGame");
    }
    
    // --------------------------------------------------
    // Functions - Coroutine
    // --------------------------------------------------
    IEnumerator Co_Loading()
    {
        yield return new WaitUntil(() => _isInitialized);
        OnInit();
    }
}
