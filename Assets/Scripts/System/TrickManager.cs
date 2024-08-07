using System.Collections.Generic;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
#region Private Variables
    [SerializeField] private HashSet<NewTrick> tricks = new HashSet<NewTrick>();
#endregion

#region Public Variables
#endregion

#region Private Methods
#endregion

#region Public Methods
    public void AddTrick(NewTrick _trick)
    {
        tricks.Add(_trick);
        Debug.Log($"{_trick.name} is added to the trick list");
    }
    public void RemoveTrick(NewTrick _trick)
    {
        if (!tricks.Contains(_trick)) {
            return;
        }
        tricks.Remove(_trick);
        Debug.Log($"{_trick.name} is removed from the trick list");
    }
#endregion
}
