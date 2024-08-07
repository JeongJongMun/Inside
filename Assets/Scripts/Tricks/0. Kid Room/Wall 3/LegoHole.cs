using System;
using System.Collections.Generic;
using UnityEngine;

public class LegoHole : NewTrick
{
#region Private Variables
    private HashSet<NewTrick> legoHoles;
#endregion

#region Public Variables
    public GameObject book;
    public GameObject bookDrop;
    public Action<NewTrick> OnLegoComplete;
#endregion

#region Private Methods
#endregion

#region Public Methods
    public void RemoveLegoHole(NewTrick _legoHole)
    {
        if (legoHoles.Contains(_legoHole)) {
            legoHoles.Remove(_legoHole);
        }
        IsComplete = CheckComplete(null);
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        legoHoles = new HashSet<NewTrick>(GetComponentsInChildren<NewTrick>());
        OnLegoComplete += RemoveLegoHole;
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (legoHoles.Count > 1 || IsComplete) return false;
        GameManager.Instance.soundManager.Play("bookdrop");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        book.SetActive(false);
        bookDrop.SetActive(true);
    }
#endregion
}