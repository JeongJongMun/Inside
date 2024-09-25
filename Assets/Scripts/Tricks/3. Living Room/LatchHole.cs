using UnityEngine;
using UnityEngine.Serialization;
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

    [FormerlySerializedAs("currentItemName")]
    [SerializeField]
    [Header("���� ���� ������ ������ �̸�")]
    private EItemType currentEItemType = EItemType.None;

    [Header("TrySolve ȣ��� Hatch")]
    public GameObject hatch;

    private void Start()
    {
        image = GetComponent<Image>();
        startSprite = image.sprite;
        GetComponent<Button>().onClick.AddListener(OnClickLatchHole);

        // �ɼ谡 �����־����� Ȯ��
        EItemType type = DatabaseManager.Instance.GetHatchData(int.Parse(this.name));
        if (type != EItemType.None)
        {
            // �ɼ� ��ȣ ���ϱ�
            string str = type.ToString();
            int latchIdx = str[str.Length - 1] - '0';
            // �ɼ� ��ȣ�� �´� �̹����� ����
            image.sprite = latchInputs[latchIdx];
            // ���� �ɼ� �ε��� ����
            inputLatchNumber = latchIdx;
            // ������ �̸� ����
            currentEItemType = type;
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
        currentEItemType = Inventory.instance.GetClickedItemName();
        // ���� Ŭ���� �������� �ɼ谡 �ƴϸ� return
        if (!(currentEItemType == EItemType.Latch0 || currentEItemType == EItemType.Latch1 || currentEItemType == EItemType.Latch2 || currentEItemType == EItemType.Latch3))
        {
            currentEItemType = EItemType.None;
            return;
        }
        // �ɼ� ��ȣ ���ϱ�
        string str = currentEItemType.ToString();
        int latchIdx = str[str.Length - 1] - '0';
        // �ɼ� ��ȣ�� �´� �̹����� ����
        image.sprite = latchInputs[latchIdx];
        // ȿ���� ���
        SoundManager.instance.SFXPlay("consoleInsert");
        // �κ��丮���� ���� �ɼ� ����
        Inventory.instance.RemoveItem(currentEItemType);
        // �ɼ谡 ���� ������ ��ȣ ����
        DatabaseManager.Instance.SetHatchData(currentEItemType, int.Parse(this.name));
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
        Inventory.instance.AcquireItem(currentEItemType);
        // �ɼ谡 ���� ������ ��ȣ ����
        DatabaseManager.Instance.SetHatchData(currentEItemType, -1);
        // �ɼ� ���� ���� ����
        inputLatchNumber = -1;
    }
}
