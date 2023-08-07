using UnityEngine;

// 트릭들의 추상 클래스
public abstract class Trick : MonoBehaviour
{
    internal bool isSolved = false;
    internal string roomName;
    private void Start()
    {
        // 현재 트릭이 속한 방 이름 가져오기
        roomName = GameObject.FindWithTag("RoomManager").name.Substring(11);
        // 씬 로드시에 트릭을 푼 적이 있다면 적용
        if (DatabaseManager.Instance.IsTrickSolved(roomName, this.name))
        {
            SolvedAction();
        }
    }

    // 트릭이 풀렸을때 모든 트릭이 공통으로 호출
    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetTrickStatus(roomName, this.name, _isSolved);
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
