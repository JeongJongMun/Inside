// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

[System.Serializable]
public class TrickInfo
{
    public string name;
    public int id;
    public List<int> successor;
    public string hint;
}

[System.Serializable]
public class HintGraph
{
    public string __comment__;
    public List<TrickInfo> trickInfo;
}
public class TrickManager
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Private
    private const string HintGraphPath = "HintGraph";
    private Graph trickGraph;
    private Dictionary<Define.ETrickType, NewTrick> trickDict;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    public void Init()
    {
        string json = Resources.Load<TextAsset>(HintGraphPath).text;
        HintGraph hintGraph = JsonUtility.FromJson<HintGraph>(json);

        int trickCount = (int)Define.ETrickType.TrickCount;
        trickGraph = new Graph(trickCount);
        trickDict = new Dictionary<Define.ETrickType, NewTrick>();
        foreach (var trick in hintGraph.trickInfo) {
            trickGraph.AddEdge(trick.id, trick.successor, trick.hint);
        }
    }
    
    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    public void AddTrick(NewTrick _trick)
    {
        if (trickDict.ContainsKey(_trick.ETrickType)) {
            Debug.LogWarning($"{_trick.gameObject.name}, {_trick.ETrickType}이 이미 존재하여 추가할 수 없습니다.");
            return;
        }
        trickDict.Add(_trick.ETrickType, _trick);
        _trick.OnCompleteAction += () => trickGraph.RemoveEdge(_trick.id);
    }
    public void RemoveTrick(NewTrick _trick)
    {
        if (!trickDict.ContainsKey(_trick.ETrickType)) {
            Debug.LogWarning($"{_trick.gameObject.name}, {_trick.ETrickType}이 존재하지 않아 삭제할 수 없습니다.");
            return;
        }

        trickDict.Remove(_trick.ETrickType);
        _trick.OnCompleteAction -= () => trickGraph.RemoveEdge(_trick.id);
    }
    public string GetHint() => trickGraph.GetHint();
    public bool IsComplete(Define.ETrickType eTrickType) 
    {
        if (!trickDict.ContainsKey(eTrickType)) {
            Debug.LogWarning($"{eTrickType}이 존재하지 않아 성공 여부를 확인할 수 없습니다.");
            return false;
        }

        return trickDict[eTrickType].IsComplete;
    }
}
