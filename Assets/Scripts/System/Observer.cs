using System;
using UnityEngine;
using UnityEngine.UI;
/* Observer.cs
 * Abstract class that observers(tricks) should implement
 * Start(): Virtual method, so that classes that inherit Observer can override Start() if needed
 * OnNotify(Define.TrickName trickName): Abstract method, so that classes that inherit Observer must implement OnNotify()
 * IsComplete: Property that can be accessed by inheriting classes
 */
public abstract class Observer : MonoBehaviour
{
#region Private Variables
    private bool isComplete = false;
    private TrickManager trickManager;
#endregion

#region Public Variables
    public bool IsComplete { get { return isComplete; }
        protected set { 
            isComplete = value;
            if (isComplete) {
                trickManager.RemoveObserver(this);
                // DatabaseManager.Instance.SetData(trickName);
                // InGameManager.Instance.FadeInOut();
            }
        } 
    }
#endregion

#region Protected Variables
    protected Define.TrickName trickName;
#endregion

#region Private Methods
    private Define.TrickName GetTrickNameFromName(string _name)
    {
        foreach(Define.TrickName value in Enum.GetValues(typeof(Define.TrickName))) {
            if (value.ToString() == _name) {
                return value;
            }
        }
        return default;
    }
#endregion

#region Public Methods
    public abstract void OnNotify(Define.TrickName trickName);
#endregion

#region Protected Methods
    protected virtual void Start()
    {
        trickName = GetTrickNameFromName(this.name);
        trickManager = FindObjectOfType<TrickManager>();
        trickManager.AddObserver(this);
        gameObject.GetComponent<Button>().onClick.AddListener(() => trickManager.Notify(trickName));
        if (IsComplete) {
            trickManager.Notify(trickName);
        }
    }
#endregion
}