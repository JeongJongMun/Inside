// ----- Unity

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UI_Loading : UI_Base
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [Header("1. Text Group")]
    [SerializeField] private TextMeshProUGUI _TMP_Percent = null;
    [SerializeField] private TextMeshProUGUI _TMP_Tip = null;
    
    [Space(1.5f)] [Header("2. Slider Group")]
    [SerializeField] private Slider _Slider = null;
    
    [Space(1.5f)] [Header("3. Button Group")]
    [SerializeField] private Button _BTN_Start = null;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private bool _isReady = false;
    
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public bool IsReady => _isReady;
    
    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void Awake() { Init(); }

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    public virtual void Init()
    {
        Managers.UI.SetCanvas(gameObject,false);
        _Slider.value = 0f;
    }
    
    public virtual void OpenLoading()
    {
        _isReady = true;
    }
    
    public virtual void SetProgress(float value)
    {
        _Slider.value = value;
    }
    
    public virtual void SetTip(string tip)
    {
        _TMP_Tip.text = tip;
    }
    
    public virtual void SetPercent(float percent)
    {
        _TMP_Percent.gameObject.SetActive(true);
        _TMP_Percent.text = $"{percent}%";
    }
    
    public virtual void SetActiveStartButton(bool isActive)
    {
        _BTN_Start.interactable = isActive;
        _BTN_Start.gameObject.SetActive(isActive);
    }
    
    public virtual void SetStartButtonEvent(Action action)
    {
        _BTN_Start.onClick.RemoveAllListeners();
        _BTN_Start.onClick.AddListener(() => action());
    }
}