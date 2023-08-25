using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ResearcherRoomStand : Trick
{
    [SerializeField]
    [Header("시험관 꽂는 순서 (정답: 노랑 < 빨강 < 파랑 순)")]
    private List<ItemName> answer = new List<ItemName>()
    {
        ItemName.TestTubeYellow, ItemName.TestTubeRed, ItemName.TestTubeBlue
    };

    [SerializeField]
    [Header("시험관 꽂힌 순서 (입력)")]
    private List<ItemName> input = new List<ItemName>();

    [SerializeField]
    [Header("현재 꽂은 시험관 개수")]
    private int currentNumber = 0;

    [Header("시험관 오브젝트")]
    public GameObject[] testTubes;

    [Header("시험관 이미지 (노랑 < 빨강 < 파랑 순으로 배치)")]
    public Sprite[] testTubeSprites;

    [Header("시험관 트릭 성공 시 캐비넷 오픈 가능")]
    public ResearcherRoomRCloset rCloset;

    [Header("시험관 트릭 성공 시 이미지 변경")]
    public Sprite standFull;

    [Header("시험관 아이템")]
    public Item[] testTubeItems;

    ItemName[] itemNames = new ItemName[] { ItemName.TestTubeBlue, ItemName.TestTubeRed, ItemName.TestTubeYellow };
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            // 시험관 입력
            foreach (ItemName itemName in itemNames)
            {
                if (Inventory.Instance.IsClicked(itemName))
                {
                    input.Add(itemName);
                    Inventory.Instance.RemoveItem(itemName);
                    // 시험관 활성화
                    testTubes[currentNumber].SetActive(true);
                    // 효과음 출력
                    SoundManager.instance.SFXPlay("glassBottle");
                    // 시험관 이미지 변경
                    testTubes[currentNumber].GetComponent<Image>().sprite = testTubeSprites[answer.IndexOf(itemName)];
                    currentNumber++;

                    // 3개의 시험관을 모두 입력 시
                    if (currentNumber == answer.Count)
                    {
                        // 정답
                        if (Enumerable.SequenceEqual(input, answer))
                        {
                            Debug.LogFormat("{0} Solved", this.name);
                            Debug.Log("옷장 열린 사운드 재생");
                            SoundManager.instance.SFXPlay("lockerOpen");
                            SetIsSolved(true);
                            SolvedAction();
                        }
                        // 오답
                        else
                        {
                            Debug.LogFormat("{0} Not Solved", this.name);

                            // 초기화
                            currentNumber = 0;
                            input.Clear();
                            foreach (Item item in testTubeItems)
                            {
                                Inventory.Instance.AcquireItem(item);
                            }
                            foreach (GameObject testTube in testTubes)
                                testTube.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public override void SolvedAction()
    {
        GetComponent<Image>().sprite = standFull;
        GetComponent<Image>().raycastTarget = false;
        rCloset.isStandSolved = true;
    }
}
