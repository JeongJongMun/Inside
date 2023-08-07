using UnityEngine;

// Ʈ������ �߻� Ŭ����
public abstract class Trick : MonoBehaviour
{
    internal bool isSolved = false;
    internal string roomName;
    private void Start()
    {
        // ���� Ʈ���� ���� �� �̸� ��������
        roomName = GameObject.FindWithTag("RoomManager").name.Substring(11);
        // �� �ε�ÿ� Ʈ���� Ǭ ���� �ִٸ� ����
        if (DatabaseManager.Instance.IsTrickSolved(roomName, this.name))
        {
            SolvedAction();
        }
    }

    // Ʈ���� Ǯ������ ��� Ʈ���� �������� ȣ��
    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetTrickStatus(roomName, this.name, _isSolved);
        GameManager.Instance.FadeInOut();
    }

    // Ʈ���� Ǯ�ȴ°� Ȯ��
    public bool IsSolved()
    {
        return this.isSolved;
    }

    // �÷��̾ Ʈ���� Ǯ���� �õ��Ҷ� ȣ���ϴ� �Լ�
    public abstract void TrySolve(GameObject obj);

    // Ʈ���� Ǯ������ ��ȭ
    public abstract void SolvedAction();
}
