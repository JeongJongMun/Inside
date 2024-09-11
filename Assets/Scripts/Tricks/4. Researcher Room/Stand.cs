using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Stand : NewTrick
{
    private List<ItemName> answer = new()
    {
        ItemName.TestTubeYellow, ItemName.TestTubeRed, ItemName.TestTubeBlue
    };

    private List<ItemName> input = new List<ItemName>();
    private int currentNumber = 0;
    public GameObject[] testTubes;
    public Sprite[] testTubeSprites;
    public Sprite standFull;
    public NewItem[] testTubeItems;

    ItemName[] itemNames = new[] { ItemName.TestTubeBlue, ItemName.TestTubeRed, ItemName.TestTubeYellow };
    
    protected override void Start()
    {
        base.Start();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        foreach (ItemName itemName in itemNames)
        {
            if (itemName == _currentClickedItem.itemName)
            {
                input.Add(itemName);
                NewInventory.instance.RemoveItem(_currentClickedItem);
                testTubes[currentNumber].SetActive(true);
                Managers.Sound.Play("glassBottle");
                testTubes[currentNumber].GetComponent<Image>().sprite = testTubeSprites[answer.IndexOf(itemName)];
                currentNumber++;

                if (currentNumber == answer.Count)
                {
                    if (Enumerable.SequenceEqual(input, answer))
                    {
                        Managers.Sound.Play("lockerOpen");
                        return true;
                    }
                    else
                    {
                        currentNumber = 0;
                        input.Clear();
                        foreach (NewItem item in testTubeItems)
                            NewInventory.instance.AddItem(item);
                        
                        foreach (GameObject testTube in testTubes)
                            testTube.SetActive(false);
                    }
                }
            }
        }
        return false;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        GetComponent<Image>().sprite = standFull;
        GetComponent<Image>().raycastTarget = false;
    }
}
