using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerResearcher : RoomManager
{
    public void OnClickDoor()
    {
        SceneManager.LoadScene("LivingRoom");
    }
    public void OnClickFlower(GameObject panel)
    {
        if (Inventory.Instance.IsClicked(Define.ItemName.Magnifier))
        {
            ZoomIn(panel);
        }
    }
}
