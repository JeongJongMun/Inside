using UnityEngine;

// Ʈ������ �߻� Ŭ����
public abstract class Trick : MonoBehaviour
{
    internal bool isSolved = false;
    internal string roomName;
    void Start()
    {
        // ���� Ʈ���� ���� �� �̸� ��������
        roomName = GameObject.FindWithTag("RoomManager").name.Substring(11);
        // �� �ε�ÿ� Ʈ���� Ǭ ���� �ִٸ� ����
        if (GameManager.Instance.IsTrickSolved(roomName, this.name))
            SolvedAction();
    }

    // Ʈ���� Ǯ������ ��� Ʈ���� �������� ȣ��
    public void Solved()
    {
        this.isSolved = true;
        GameManager.Instance.SetTrickSolved(roomName, this.name);
        StartCoroutine(GameManager.Instance.FadeInOut());
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
