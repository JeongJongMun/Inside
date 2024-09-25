// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [Header("1. Loading Group")]
    [SerializeField] private UI_Loading _UI_Loading = null;
    
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
        _UI_Loading.SetActiveStartButton(false);
        _UI_Loading.SetPercent(0f);
        _UI_Loading.SetProgress(0f);
        
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
        
        // #2 CoroutineHelper Init
        _actions.Add(() =>
        {
            Debug.Log($"[GameInitializer.Action...] Action {_actionIndex + 1} Start | CoroutineHelper Init");
            CoroutineHelper.Init();
            InitDone();
        });
        
        // #3 Global Addressable Load
        _actions.Add(() =>
        {
            Debug.Log($"[GameInitializer.Action...] Action {_actionIndex + 1} Start | Global Addressable Load");
            Managers.Resource.LoadAllAsync<GameObject>("global", Define.ELoadType.Global, (asset, current, total) =>
            {
                if (current == total)
                    InitDone();
            });
        });
        
        // #4 Sprite Addressable Load
        // _actions.Add(() =>
        // {
        //     Debug.Log($"[GameInitializer.Action...] Action {_actionIndex + 1} Start | Sprite Addressable Load");
        //     Managers.Resource.LoadAllAsync<Sprite>("sprite", Define.ELoadType.Global, (asset, current, total) =>
        //     {
        //         if (current == total)
        //             InitDone();
        //     });
        // });
        
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
            SetLoadingText();
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
        _UI_Loading.SetActiveStartButton(true);
        _UI_Loading.SetPercent(100f);
        _UI_Loading.SetProgress(1f);
        _UI_Loading.SetStartButtonEvent(OnClickStart);
    }

    private void OnClickStart()
    {
        Managers.LoadingScene.LoadScene(Define.ESceneType.OutGameScene.ToString());
    }
    
    private void SetLoadingText()
    {
        var progressValue = (int)(_actionIndex / (float)_totalActionCount);
        
        _UI_Loading.SetPercent(progressValue);
        _UI_Loading.SetProgress(progressValue);
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
