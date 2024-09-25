// ----- C#
using System.Collections.Generic;
using Unity.VisualScripting;

// ----- Unity
using UnityEngine;

public class UIManager
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private int _order = 10;
    private UI_Loading _loaindgUI = null;
    private Stack<UI_Popup> _popupPool = new Stack<UI_Popup>();

    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public GameObject GlobalUIRoot
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Global_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Global_Root" };
                GameObject.DontDestroyOnLoad(root);
            }
            return root;
        }
    }
    
    // --------------------------------------------------
    // Essential Functions - Normal
    // --------------------------------------------------
    public void Init()
    {
        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            root = new GameObject { name = "@UI_Root" };
        }
    }

    public void SetCanvas(GameObject go, bool sort = true, bool story = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        
        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    
    public T ShowLoadingUI<T>(string name = null) where T : UI_Loading
    {
        if (_loaindgUI != null && _loaindgUI.name == typeof(T).Name)
        {
            Managers.UI._loaindgUI.gameObject.SetActive(true);
            return (T)_loaindgUI;
        }

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go = Managers.Resource.Instantiate($"UI/Loading/{name}.prefab");
        T sceneUI = go.GetOrAddComponent<T>();
        
        _loaindgUI = sceneUI;
        
        go.transform.SetParent(GlobalUIRoot.transform);
        
        SetCanvas(go, false);
        
        go.GetComponent<Canvas>().sortingOrder = 9999;
        return sceneUI;
    }
    
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}.prefab");

        if (_popupPool.Count != 0)
        {
            var prevPopup = _popupPool.Peek();
            prevPopup.OnOffDim(false);
        }
        
        T popup = go.GetOrAddComponent<T>();
        _popupPool.Push(popup);
        
        Debug.Log($"[UIManager.ShowPopupUI] {popup.name}를 생성하였습니다.");
        
        go.transform.SetParent(Root.transform);
        return popup;
    }

    public void CloseLoadingUI()
    {
        UI_Loading loadingUI = Managers.UI._loaindgUI;
        if(loadingUI == null)
            return;
        Managers.Resource.Destroy(loadingUI.gameObject);
        loadingUI = null;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if(_popupPool.Count == 0)
            return;
        
        if(_popupPool.Peek() != popup)
        {
            Debug.Log($"[UIManager.ClosePopupUI] {popup.name}를 닫지 못했습니다.");
            return;
        }
        ClosePopupUI();
    }
    
    public void ClosePopupUI()
    {
        if(_popupPool.Count == 0)
            return;
        
        UI_Popup popup = _popupPool.Pop();
        if (_popupPool.Count != 0)
        {
            var currPopup = _popupPool.Peek();
            currPopup.Refresh();
            currPopup.OnOffDim(true);
        }
    
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }
    
    public void CloseAllPopupUI()
    {
        while(_popupPool.Count>0)
            ClosePopupUI();
    }
    
    public UI_Popup GetCurrentPopupUI()
    {
        return _popupPool.Count == 0 ? null : _popupPool.Peek();
    }
}