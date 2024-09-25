using UnityEngine;
using UnityEngine.Serialization;
using static Define;

public abstract class Trick : MonoBehaviour
{
    [SerializeField]
    internal bool isSolved = false;

    [FormerlySerializedAs("trickName")] [SerializeField]
    internal ETrickType eTrickType;

    public virtual void Start()
    {
        if (name.Contains("(Clone)"))
        {
            int cloneIdx = name.IndexOf("(Clone)");
            eTrickType = Item.GetEnumFromName<ETrickType>(name.Substring(0, cloneIdx));
        }
        else eTrickType = Item.GetEnumFromName<ETrickType>(this.name);

        if (DatabaseManager.Instance.GetData(eTrickType))
        {
            isSolved = true;
            SolvedAction();
        }
    }

    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetData(eTrickType);
        // InGameManager.instance.BlinkingEffect();
    }

    public bool IsSolved()
    {
        return this.isSolved;
    }

    public abstract void TrySolve(GameObject obj);

    public abstract void SolvedAction();
}
