using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ResearcherRoomStand : Trick
{
    [SerializeField]
    [Header("����� �ȴ� ���� (����: ��� < ���� < �Ķ� ��)")]
    private List<ItemName> answer = new List<ItemName>()
    {
        ItemName.TestTubeYellow, ItemName.TestTubeRed, ItemName.TestTubeBlue
    };

    [SerializeField]
    [Header("����� ���� ���� (�Է�)")]
    private List<ItemName> input = new List<ItemName>();

    [SerializeField]
    [Header("���� ���� ����� ����")]
    private int currentNumber = 0;

    [Header("����� ������Ʈ")]
    public GameObject[] testTubes;

    [Header("����� �̹��� (��� < ���� < �Ķ� ������ ��ġ)")]
    public Sprite[] testTubeSprites;

    [Header("����� Ʈ�� ���� �� ĳ��� ���� ����")]
    public ClosetResearcher rCloset;

    [Header("����� Ʈ�� ���� �� �̹��� ����")]
    public Sprite standFull;

    [Header("����� ������")]
    public Item[] testTubeItems;

    ItemName[] itemNames = new ItemName[] { ItemName.TestTubeBlue, ItemName.TestTubeRed, ItemName.TestTubeYellow };
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            // ����� �Է�
            foreach (ItemName itemName in itemNames)
            {
                if (Inventory.instance.IsClicked(itemName))
                {
                    input.Add(itemName);
                    Inventory.instance.RemoveItem(itemName);
                    // ����� Ȱ��ȭ
                    testTubes[currentNumber].SetActive(true);
                    // ȿ���� ���
                    SoundManager.instance.SFXPlay("glssBottle");
                    // ����� �̹��� ����
                    testTubes[currentNumber].GetComponent<Image>().sprite = testTubeSprites[answer.IndexOf(itemName)];
                    currentNumber++;

                    // 3���� ������� ��� �Է� ��
                    if (currentNumber == answer.Count)
                    {
                        // ����
                        if (Enumerable.SequenceEqual(input, answer))
                        {
                            Debug.LogFormat("{0} Solved", this.name);
                            Debug.Log("���� ���� ���� ���");
                            SoundManager.instance.SFXPlay("lockerOpen");
                            SetIsSolved(true);
                            SolvedAction();
                        }
                        // ����
                        else
                        {
                            Debug.LogFormat("{0} Not Solved", this.name);

                            // �ʱ�ȭ
                            currentNumber = 0;
                            input.Clear();
                            foreach (Item item in testTubeItems)
                            {
                                Inventory.instance.AcquireItem(item);
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
