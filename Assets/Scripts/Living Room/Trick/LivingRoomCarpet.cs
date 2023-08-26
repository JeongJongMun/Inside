using UnityEngine;

public class LivingRoomCarpet : Trick
{
    [Header("Ä«Æê ¿­¸°°Å")]
    public GameObject carpetOpened;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            Debug.LogFormat("{0} Solved", this.name);
            SoundManager.instance.SFXPlay("bedFabric");
            SetIsSolved(true);
            SolvedAction();
        }
    }
    public override void SolvedAction()
    {
        carpetOpened.SetActive(true);
        gameObject.SetActive(false);
    }
}
