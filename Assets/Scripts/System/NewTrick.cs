using System;
using UnityEngine;
using UnityEngine.UI;
/* Trick.cs
 * Abstract class that tricks should implement
 */
public abstract class NewTrick : MonoBehaviour
{
#region Private Variables
    private bool isComplete = false;
    public Action OnCompleteAction;
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
    public Define.ETrickType ETrickType { get; private set; }
    public int id { get; private set; }
#endregion

#region Protected Variables
#endregion

#region Protected Methods
    protected virtual void Start()
    {
        if (Enum.TryParse<Define.ETrickType>(this.name, out var trickName)) {
            this.ETrickType = trickName;
            this.id = (int)this.ETrickType;
        }
        inGameManager = FindObjectOfType<InGameManager>();
        Managers.Trick.AddTrick(this);

        if (TryGetComponent(out Button button)) {
            button.onClick.AddListener(() => IsComplete = CheckComplete(NewInventory.instance.GetClickedItem()));
        }
        OnCompleteAction += OnComplete;
    }
    protected abstract bool CheckComplete(NewItem _currentClickedItem);
    protected virtual void OnComplete()
    {
        OnCompleteAction -= OnComplete;

        inGameManager.BlinkingEffect(Color.black);
        if (TryGetComponent(out Image image)) {
            image.raycastTarget = false;
        }
    }
#endregion
}