// ----- C#
using System;

// ------ Unity
using UnityEngine;
using UnityEngine.UI;

public class NormalButton : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [Header("1. Button Group")] 
    [SerializeField] private Button _BTN_Click = null;
    [SerializeField] private RectTransform _rectTransform = null;
        
    [Space(1.5f)] [Header("2. Option Group")] 
    [SerializeField] private bool _isPlaySound = true;
    [SerializeField] private bool _isPlayAnimation = true;
        
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Private
    private Action _onClickAction = null;
    private Animator _animator = null;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void Awake()
    {
        if (_animator != null)
        {
            _animator = this.GetComponent<Animator>();
            _animator.enabled = _isPlayAnimation;
        }
    }

    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    // ----- Public
    public void Init(Action onClickAction)
    {
        if (_onClickAction != null)
            return;

        _onClickAction = onClickAction;
        _BTN_Click.onClick.AddListener(SetEvent);
    }

    public void SetInteractable(bool isOn)
    {
        if (_BTN_Click.interactable == isOn)
            return;

        _BTN_Click.interactable = isOn;
    }

    // ----- Private
    private void SetEvent()
    {
        // Sound Play 추가해야함.
            
        _onClickAction?.Invoke();
    }
}