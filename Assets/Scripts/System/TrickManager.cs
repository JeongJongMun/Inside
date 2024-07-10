using UnityEngine;

public class TrickManager : MonoBehaviour, ISubject
{
#region Private Variables
    private delegate void NotifyHandler(Define.TrickName trickName);
    private NotifyHandler notifyHandler;
#endregion

#region Public Variables
#endregion

#region Private Methods
#endregion

#region Public Methods
    public void AddObserver(Observer observer)
    {
        notifyHandler += observer.OnNotify;
        Debug.Log($"{observer.name} is added to the observer list");
    }
    public void RemoveObserver(Observer observer)
    {
        notifyHandler -= observer.OnNotify;
        Debug.Log($"{observer.name} is removed from the observer list");
    }
    public void Notify(Define.TrickName trickName)
    {
        notifyHandler(trickName);
    }
#endregion
}
