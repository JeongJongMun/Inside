using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TrickHintInfo
{
    public string name;
    public int id;
    public List<int> successor;
    public string hint;
}

[System.Serializable]
public class GraphInfo
{
    public string __comment__;
    public TrickHintInfo[] tricks;
}
public class TrickManager : MonoBehaviour
{
#region Private Variables
    private const string HintGraphPath = "HintGraph";
#endregion

#region Public Variables
    public Graph trickGraph;
#endregion

#region Private Methods
    private void Awake()
    {
        string json = Resources.Load<TextAsset>(HintGraphPath).text;
        GraphInfo graphInfo = JsonUtility.FromJson<GraphInfo>(json);

        trickGraph = new Graph((int)Define.TrickName.TrickCount);
        foreach (var trick in graphInfo.tricks) {
            trickGraph.AddEdge(trick.id, trick.successor, trick.hint);
        }
    }
#endregion

#region Public Methods
    public void AddTrick(NewTrick _trick)
    {
        _trick.id = (int)_trick.trickName;
        _trick.successor = trickGraph.GetSuccessor(_trick.id).ToList();
    }
    public void RemoveTrick(NewTrick _trick)
    {
        foreach (int succ in _trick.successor) {
            Debug.Log("Remove Edge: " + _trick.id + " -> " + succ);
            trickGraph.RemoveEdge(_trick.id, succ);
        }

        // Debugging
        int[] inDegree = trickGraph.GetInDegree();
        for (int i = 0; i < 12; i++) {
            Debug.Log("InDegree[" + i + "]: " + inDegree[i]);
        }
    }
#endregion
}
