using System;
using UnityEngine;
using UnityEngine.UI;
using static Define;
/* Trick.cs
 * Abstract class that tricks should implement
 */
public abstract class NewTrick : MonoBehaviour
{
#region Private Variables
    private bool isComplete = false;
    private TrickManager trickManager;
    private Action OnCompleteAction;
#endregion

#region Public Variables
    public bool IsComplete { get { return isComplete; }
        protected set { 
            isComplete = value;
            if (isComplete) {
                OnCompleteAction?.Invoke();
                // DatabaseManager.Instance.SetData(trickName);
                // InGameManager.Instance.FadeInOut();
            }
        } 
    }
#endregion

#region Protected Variables
    protected TrickName trickName;
#endregion

#region Private Methods
    private TrickName GetTrickNameFromName(string _name)
    {
        foreach(TrickName value in Enum.GetValues(typeof(TrickName))) {
            if (value.ToString() == _name) {
                return value;
            }
        }
        return default;
    }
#endregion

#region Public Methods
#endregion

#region Protected Methods
    protected virtual void Start()
    {
        trickName = GetTrickNameFromName(this.name);
        trickManager = FindObjectOfType<TrickManager>();
        trickManager.AddTrick(this);

        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(() => IsComplete = CheckComplete(NewInventory.instance.GetClickedItem()));
        }
        OnCompleteAction += OnComplete;
    }
    protected abstract bool CheckComplete(NewItem _currentClickedItem);
    protected virtual void OnComplete()
    {
        OnCompleteAction -= OnComplete;
        trickManager.RemoveTrick(this);
        if (TryGetComponent(out Image image)) {
            image.raycastTarget = false;
        }
    }
#endregion
}