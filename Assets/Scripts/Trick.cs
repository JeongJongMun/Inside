using UnityEngine;
using static Define;

// Ʈ������ �߻� Ŭ����
public abstract class Trick : MonoBehaviour
{
    [SerializeField]
    internal bool isSolved = false;

    [SerializeField]
    [Header("Ʈ�� �Ҽ� �� �̸�")]
    internal RoomName roomName;

    [SerializeField]
    [Header("Ʈ�� �̸�")]
    internal TrickName trickName;

    // ��� Ʈ������ Start �Լ����� ����� �ϴ� �۾��� �ֱ⿡ virtual
    public virtual void Start()
    {
        // ���� Ʈ���� ���� �� �̸� ��������
        roomName = Item.GetEnumFromName<RoomName>(GameObject.FindWithTag("RoomManager").name.Substring(11));
        // ���� Ʈ���� ������ �̸� ��������
        // Instantiate �� ������Ʈ���
        if (name.Contains("(Clone)"))
        {
            int cloneIdx = name.IndexOf("(Clone)");
            trickName = Item.GetEnumFromName<TrickName>(name.Substring(0, cloneIdx));
        }
        else trickName = Item.GetEnumFromName<TrickName>(this.name);

        // �� �ε�ÿ� Ʈ���� Ǭ ���� �ִٸ� ����
        if (DatabaseManager.Instance.IsTrickSolved(roomName, trickName))
        {
            SolvedAction();
        }
    }

    // Ʈ���� Ǯ������ ��� Ʈ���� �������� ȣ��
    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetTrickStatus(roomName, trickName, _isSolved);
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
