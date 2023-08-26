using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LatchHole : MonoBehaviour
{
    [Header("걸쇠 꽂힌 이미지")]
    public Sprite[] latchInputs;

    [SerializeField]
    [Header("현재 꽂힌 걸쇠 번호 (0 ~ 3)")]
    private int inputLatchNumber = -1;

    private Image image;

    private Sprite startSprite;

    [SerializeField]
    [Header("현재 꽂힌 아이템 열거형 이름")]
    private ItemName currentItemName = ItemName.None;

    [Header("TrySolve 호출용 Hatch")]
    public GameObject hatch;

    private void Start()
    {
        image = GetComponent<Image>();
        startSprite = image.sprite;
        GetComponent<Button>().onClick.AddListener(OnClickLatchHole);

        // 걸쇠가 꽂혀있었는지 확인
        ItemName _name = DatabaseManager.Instance.GetHatchData(int.Parse(this.name));
        if (_name != ItemName.None)
        {
            // 걸쇠 번호 구하기
            string str = _name.ToString();
            int latchIdx = str[str.Length - 1] - '0';
            // 걸쇠 번호에 맞는 이미지로 변경
            image.sprite = latchInputs[latchIdx];
            // 꽂힌 걸쇠 인덱스 수정
            inputLatchNumber = latchIdx;
            // 열거형 이름 설정
            currentItemName = _name;
        }
    }

    // 정답 체크용 현재 꽂힌 걸쇠 번호 반환 함수
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
        // 현재 클릭된 아이템 이름 가져오기
        currentItemName = Inventory.Instance.GetClickedItemName();
        // 현재 클릭된 아이템이 걸쇠가 아니면 return
        if (!(currentItemName == ItemName.Latch0 || currentItemName == ItemName.Latch1 || currentItemName == ItemName.Latch2 || currentItemName == ItemName.Latch3))
        {
            currentItemName = ItemName.None;
            return;
        }
        // 걸쇠 번호 구하기
        string str = currentItemName.ToString();
        int latchIdx = str[str.Length - 1] - '0';
        // 걸쇠 번호에 맞는 이미지로 변경
        image.sprite = latchInputs[latchIdx];
        // 효과음 출력
        SoundManager.instance.SFXPlay("consoleInsert");
        // 인벤토리에서 꽂은 걸쇠 삭제
        Inventory.Instance.RemoveItem(currentItemName);
        // 걸쇠가 꽂힌 구멍의 번호 저장
        DatabaseManager.Instance.SetHatchData(currentItemName, int.Parse(this.name));
        // 걸쇠 꽂힘 유무 수정
        inputLatchNumber = latchIdx;
        // TrySolve
        hatch.GetComponent<Trick>().TrySolve(hatch);
    }

    private void OutputLatch()
    {
        // 원래 이미지로 변경
        image.sprite = startSprite;
        // 걸쇠 아이템 회수
        Inventory.Instance.AcquireItem(currentItemName);
        // 걸쇠가 꽂힌 구멍의 번호 저장
        DatabaseManager.Instance.SetHatchData(currentItemName, -1);
        // 걸쇠 꽂힘 유무 수정
        inputLatchNumber = -1;
    }
}
