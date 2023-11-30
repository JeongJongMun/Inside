using UnityEngine;
using static Define;

// Ʈ������ �߻� Ŭ����
public abstract class Trick : MonoBehaviour
{
    [SerializeField]
    internal bool isSolved = false;

    [SerializeField]
    [Header("Ʈ�� �̸�")]
    internal TrickName trickName;

    // ��� Ʈ������ Start �Լ����� ����� �ϴ� �۾��� �ֱ⿡ virtual
    public virtual void Start()
    {
        // ���� Ʈ���� ������ �̸� ��������
        // Instantiate �� ������Ʈ���
        if (name.Contains("(Clone)"))
        {
            int cloneIdx = name.IndexOf("(Clone)");
            trickName = Item.GetEnumFromName<TrickName>(name.Substring(0, cloneIdx));
        }
        else trickName = Item.GetEnumFromName<TrickName>(this.name);

        // �� �ε�ÿ� Ʈ���� Ǭ ���� �ִٸ� ����
        if (DatabaseManager.Instance.GetData(trickName))
        {
            isSolved = true;
            SolvedAction();
        }
    }

    // Ʈ���� Ǯ������ ��� Ʈ���� �������� ȣ��
    public void SetIsSolved(bool _isSolved)
    {
        this.isSolved = _isSolved;
        DatabaseManager.Instance.SetData(trickName);
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

    // ���� �Ŵ���
    SoundManager soundManager;

    private void Awake()
    {
        //soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

}