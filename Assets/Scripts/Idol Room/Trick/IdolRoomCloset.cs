using UnityEngine;
using UnityEngine.UI;

public class IdolRoomCloset : Trick
{
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == name)
        {
            SoundManager.instance.SFXPlay("closet");
            Debug.LogFormat("{0} Solved", name);
            SetIsSolved(true);
            SolvedAction();
        }
    }
    public override void SolvedAction()
    {
        Image image = GetComponent<Image>();
        Color color = image.color;
        color.a = 0;
        image.color = color;
        image.raycastTarget = false;
    }
}
