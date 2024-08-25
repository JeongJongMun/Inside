using System.Collections.Generic;
using System.Linq;

// 방향 그래프
[System.Serializable]
public class Graph
{
#region Private Variables
    private List<int>[] adjList;
    private int[] inDegree;
    private string[] hints;
#endregion

#region Public Methods
    public Graph(int nodeCount)
    {
        adjList = new List<int>[nodeCount];
        inDegree = new int[nodeCount];
        hints = new string[nodeCount];
        for (int i = 0; i < nodeCount; i++) {
            adjList[i] = new List<int>();
        }
    }
    public void AddEdge(int src, List<int> destList, string hint)
    {
        foreach (int dest in destList) {
            if (dest == -1) continue;
            adjList[src].Add(dest);
            inDegree[dest]++;
        }
        hints[src] = hint;
    }
    public void RemoveEdge(int src)
    {
        List<int> destList = adjList[src].ToList();

        foreach (int dest in destList) {
            adjList[src].Remove(dest);
            inDegree[src]--;
            inDegree[dest]--;
        }
    }
    // 진입차수가 0인 정점을 찾아서 반환
    public int FindZeroInDegreeNode()
    {
        for (int i = 0; i < inDegree.Length; i++) {
            if (inDegree[i] == 0) {
                return i;
            }
        }
        return -1;
    }
    public string GetHint()
    {
        int node = FindZeroInDegreeNode();
        if (node == -1)
            return string.Empty;
        else 
            return hints[node];
    }
    public int[] GetInDegree() => inDegree;
    public List<int> GetSuccessor(int node) => adjList[node];
#endregion


}
