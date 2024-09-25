// ----- C#
using System.Collections;

// ----- Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private string _previousScene = string.Empty;
    private string _currenctScene = string.Empty;
    
    private float _time = 0f;
    
    public bool _isRunLoad = false;
    
    private UI_Loading _loading = null;
    
    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    public void Init()
    {
        _currenctScene = SceneManager.GetActiveScene().name;
    }
    
    public void LoadScene(string SceneName, UI_Loading uiLoading = null, System.Action callback = null, System.Action sceneStartAction = null)
    {
        if(_isRunLoad)
            return;
        
        _previousScene = SceneManager.GetActiveScene().name;
        _currenctScene = SceneName;
        
        if (uiLoading == null)
        {
            _loading = Managers.UI.ShowLoadingUI<UI_Loading>();
            CoroutineHelper.StartCoroutine(LoadAsyncSceneCoroutine(SceneName, true, callback, sceneStartAction));
        }
        else
        {
            _loading = uiLoading;
            CoroutineHelper.StartCoroutine(LoadAsyncSceneCoroutine(SceneName, false, callback, sceneStartAction));
        }

    }
    
    // --------------------------------------------------
    // Functions - Coroutine
    // --------------------------------------------------
    private IEnumerator LoadAsyncSceneCoroutine(string sceneName, bool isOpenLoading, System.Action callback, System.Action sceneStartAction = null)
    {
        _isRunLoad = true;
        
        if(isOpenLoading)
            _loading.OpenLoading();

        yield return new WaitUntil(() => _loading.gameObject && _loading.IsReady);

        Managers.UI.CloseAllPopupUI();
        Managers.Resource.ReleaseStage();

        yield return new WaitForSeconds(0.2f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        
        _time = 0;
        while (_time < 0.5f) 
        {
            _time += Time.deltaTime;
            if (_time > 0.5f)
                operation.allowSceneActivation = true;
            yield return null;
        }

        yield return new WaitUntil(() => operation.isDone);
        if (operation.isDone)
        {
            callback?.Invoke();
        }
        
        yield return new WaitUntil(() => _loading == null);
        
        _isRunLoad = false; 
        
        sceneStartAction?.Invoke(); 
    }
}