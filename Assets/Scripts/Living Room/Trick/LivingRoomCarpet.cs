using UnityEngine;

public class LivingRoomCarpet : Trick
{
    [Header("ī�� ������")]
    public GameObject carpetOpened;

    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            Debug.LogFormat("{0} Solved", this.name);
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
