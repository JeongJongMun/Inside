using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LatchHole : MonoBehaviour
{
    [Header("�ɼ� ���� �̹���")]
    public Sprite[] latchInputs;

    [SerializeField]
    [Header("���� ���� �ɼ� ��ȣ (0 ~ 3)")]
    private int inputLatchNumber = -1;

    private Image image;

    private Sprite startSprite;

    [SerializeField]
    [Header("���� ���� ������ ������ �̸�")]
    private ItemName currentItemName = ItemName.None;

    [Header("TrySolve ȣ��� Hatch")]
    public GameObject hatch;

    private void Start()
    {
        image = GetComponent<Image>();
        startSprite = image.sprite;
        GetComponent<Button>().onClick.AddListener(OnClickLatchHole);

        // �ɼ谡 �����־����� Ȯ��
        ItemName _name = DatabaseManager.Instance.GetHatchData(int.Parse(this.name));
        if (_name != ItemName.None)
        {
            // �ɼ� ��ȣ ���ϱ�
            string str = _name.ToString();
            int latchIdx = str[str.Length - 1] - '0';
            // �ɼ� ��ȣ�� �´� �̹����� ����
            image.sprite = latchInputs[latchIdx];
            // ���� �ɼ� �ε��� ����
            inputLatchNumber = latchIdx;
            // ������ �̸� ����
            currentItemName = _name;
        }
    }

    // ���� üũ�� ���� ���� �ɼ� ��ȣ ��ȯ �Լ�
    public int GetInputLatchNumber()
    {
        return inputLatchNumber;
    }
    public void OnClickLatchHole()
    {
        if (inputLatchNumber == -1)
            InputLatch();
        else 
            OutputLatch();
    }

    private void InputLatch()
    {
        // ���� Ŭ���� ������ �̸� ��������
        currentItemName = Inventory.instance.GetClickedItemName();
        // ���� Ŭ���� �������� �ɼ谡 �ƴϸ� return
        if (!(currentItemName == ItemName.Latch0 || currentItemName == ItemName.Latch1 || currentItemName == ItemName.Latch2 || currentItemName == ItemName.Latch3))
        {
            currentItemName = ItemName.None;
            return;
        }
        // �ɼ� ��ȣ ���ϱ�
        string str = currentItemName.ToString();
        int latchIdx = str[str.Length - 1] - '0';
        // �ɼ� ��ȣ�� �´� �̹����� ����
        image.sprite = latchInputs[latchIdx];
        // ȿ���� ���
        SoundManager.instance.SFXPlay("consoleInsert");
        // �κ��丮���� ���� �ɼ� ����
        Inventory.instance.RemoveItem(currentItemName);
        // �ɼ谡 ���� ������ ��ȣ ����
        DatabaseManager.Instance.SetHatchData(currentItemName, int.Parse(this.name));
        // �ɼ� ���� ���� ����
        inputLatchNumber = latchIdx;
        // TrySolve
        hatch.GetComponent<Trick>().TrySolve(hatch);
    }

    private void OutputLatch()
    {
        // ���� �̹����� ����
        image.sprite = startSprite;
        // �ɼ� ������ ȸ��
        Inventory.instance.AcquireItem(currentItemName);
        // �ɼ谡 ���� ������ ��ȣ ����
        DatabaseManager.Instance.SetHatchData(currentItemName, -1);
        // �ɼ� ���� ���� ����
        inputLatchNumber = -1;
    }
}
