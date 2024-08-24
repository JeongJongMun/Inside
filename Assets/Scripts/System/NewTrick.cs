using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Trick.cs
 * Abstract class that tricks should implement
 */
public abstract class NewTrick : MonoBehaviour
{
#region Private Variables
    private bool isComplete = false;
    private TrickManager trickManager;
    private Action OnCompleteAction;
    private InGameManager inGameManager;
#endregion

#region Public Variables
    public bool IsComplete { get { return isComplete; }
        protected set { 
            isComplete = value;
            if (isComplete) {
                OnCompleteAction?.Invoke();
                // DatabaseManager.Instance.SetData(trickName);
            }
        } 
    }
    public Define.TrickName trickName;
    public int id;
    public List<int> successor;
#endregion

#region Protected Variables
#endregion

#region Protected Methods
    protected virtual void Start()
    {
        if (Enum.TryParse<Define.TrickName>(this.name, out var trickName)) {
            this.trickName = trickName;
        }
        successor = new List<int>();
        inGameManager = FindObjectOfType<InGameManager>();
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
        inGameManager.BlinkingEffect(Color.black);
        if (TryGetComponent(out Image image)) {
            image.raycastTarget = false;
        }
    }
#endregion
}