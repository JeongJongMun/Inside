using UnityEngine;
using static Define;

public abstract class Trick : MonoBehaviour
{
    [SerializeField]
    internal bool isSolved = false;

    [SerializeField]
    internal TrickName trickName;

    public virtual void Start()
    {
        if (name.Contains("(Clone)"))
        {
            int cloneIdx = name.IndexOf("(Clone)");
            trickName = Item.GetEnumFromName<TrickName>(name.Substring(0, cloneIdx));
        }
        else trickName = Item.GetEnumFromName<TrickName>(this.name);

        if (DatabaseManager.Instance.GetData(trickName))
        {
            isSolved = true;
            SolvedAction();
        }
    }

    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetData(trickName);
        InGameManager.Instance.FadeInOut();
    }

    public bool IsSolved()
    {
        return this.isSolved;
    }

    public abstract void TrySolve(GameObject obj);

    public abstract void SolvedAction();
}
