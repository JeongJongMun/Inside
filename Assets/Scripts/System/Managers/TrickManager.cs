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
    private Dictionary<Define.TrickName, NewTrick> trickDict;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    public void Init()
    {
        string json = Resources.Load<TextAsset>(HintGraphPath).text;
        HintGraph hintGraph = JsonUtility.FromJson<HintGraph>(json);

        int trickCount = (int)Define.TrickName.TrickCount;
        trickGraph = new Graph(trickCount);
        trickDict = new Dictionary<Define.TrickName, NewTrick>();
        foreach (var trick in hintGraph.trickInfo) {
            trickGraph.AddEdge(trick.id, trick.successor, trick.hint);
        }
    }
    
    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    public void AddTrick(NewTrick _trick)
    {
        if (trickDict.ContainsKey(_trick.trickName)) {
            Debug.LogWarning($"{_trick.gameObject.name}, {_trick.trickName}이 이미 존재하여 추가할 수 없습니다.");
            return;
        }
        trickDict.Add(_trick.trickName, _trick);
        _trick.OnCompleteAction += () => trickGraph.RemoveEdge(_trick.id);
    }
    public void RemoveTrick(NewTrick _trick)
    {
        if (!trickDict.ContainsKey(_trick.trickName)) {
            Debug.LogWarning($"{_trick.gameObject.name}, {_trick.trickName}이 존재하지 않아 삭제할 수 없습니다.");
            return;
        }

        trickDict.Remove(_trick.trickName);
        _trick.OnCompleteAction -= () => trickGraph.RemoveEdge(_trick.id);
    }
    public string GetHint() => trickGraph.GetHint();
    public bool IsComplete(Define.TrickName _trickName) 
    {
        if (!trickDict.ContainsKey(_trickName)) {
            Debug.LogWarning($"{_trickName}이 존재하지 않아 성공 여부를 확인할 수 없습니다.");
            return false;
        }

        return trickDict[_trickName].IsComplete;
    }
}
