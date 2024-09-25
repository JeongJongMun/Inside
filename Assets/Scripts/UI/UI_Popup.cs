// ----- Unity
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_Popup : UI_Base
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [Header("----- Common Group -----")] 
    [SerializeField] protected Animation _animation = null;
    [SerializeField] protected Image _IMG_Dim = null;
    [SerializeField] protected NormalButton _BTN_Close = null;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    protected string SHOW_KEY = "UI_Popup_Show";
    protected string HIDE_KEY = "UI_Popup_Hide";
    
    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    // ----- Public
    public virtual void Init()
    {
        Managers.UI.SetCanvas(gameObject,true);
        
        if (_BTN_Close != null)
            _BTN_Close.Init(ClosePopupUI);
        
        PlayAnimation(true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
    
    public virtual void Refresh()
    {
    
    }

    public virtual void PlayAnimation(bool isShow)
    {
        if (_animation == null)
            return;
        
        _animation.Stop();

        var clip = _animation.GetClip(isShow ? SHOW_KEY : HIDE_KEY);
        _animation.clip = clip;
        _animation.Play();
    }

    public void OnOffDim(bool isOn)
    {
        if (_IMG_Dim == null)
            return;
        
        _IMG_Dim.gameObject.SetActive(isOn);
    }
}
