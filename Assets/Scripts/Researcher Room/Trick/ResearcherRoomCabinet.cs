using UnityEngine;
using UnityEngine.UI;

public class ResearcherRoomCabinet : Trick
{
    [Header("열린 캐비넷 이미지")]
    public Sprite cabinetOpened;

    [Header("시험관들")]
    public GameObject[] testTubes;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Inventory.Instance.IsClicked(Define.ItemName.GoldKey))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SoundManager.instance.SFXPlay("closet");
                Inventory.Instance.RemoveItem(Define.ItemName.GoldKey);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                SoundManager.instance.SFXPlay("drawerLocked");
            }

        }
    }
    public override void SolvedAction()
    {
        GetComponent<Image>().sprite = cabinetOpened;
        GetComponent<Image>().raycastTarget = false;
        foreach (GameObject testTube in testTubes)
        {
            testTube.SetActive(true);
        }
    }
}
