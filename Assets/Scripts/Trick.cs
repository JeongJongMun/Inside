using UnityEngine;
using static Define;

// 트릭들의 추상 클래스
public abstract class Trick : MonoBehaviour
{
    [SerializeField]
    internal bool isSolved = false;

    [SerializeField]
    [Header("트릭 소속 방 이름")]
    internal RoomName roomName;

    [SerializeField]
    [Header("트릭 이름")]
    internal TrickName trickName;

    // 몇몇 트릭에선 Start 함수에서 해줘야 하는 작업이 있기에 virtual
    public virtual void Start()
    {
        // 현재 트릭이 속한 방 이름 가져오기
        roomName = Item.GetEnumFromName<RoomName>(GameObject.FindWithTag("RoomManager").name.Substring(11));
        // 현재 트릭의 열거형 이름 가져오기
        // Instantiate 된 오브젝트라면
        if (name.Contains("(Clone)"))
        {
            int cloneIdx = name.IndexOf("(Clone)");
            trickName = Item.GetEnumFromName<TrickName>(name.Substring(0, cloneIdx));
        }
        else trickName = Item.GetEnumFromName<TrickName>(this.name);

        // 씬 로드시에 트릭을 푼 적이 있다면 적용
        if (DatabaseManager.Instance.IsTrickSolved(roomName, trickName))
        {
            SolvedAction();
        }
    }

    // 트릭이 풀렸을때 모든 트릭이 공통으로 호출
    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetTrickStatus(roomName, trickName, _isSolved);
        GameManager.Instance.FadeInOut();
    }

    // 트릭이 풀렸는가 확인
    public bool IsSolved()
    {
        return this.isSolved;
    }

    // 플레이어가 트릭을 풀려고 시도할때 호출하는 함수
    public abstract void TrySolve(GameObject obj);

    // 트릭이 풀렸을때 변화
    public abstract void SolvedAction();
}
