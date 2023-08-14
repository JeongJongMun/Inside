using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayManager : MonoBehaviour
{
    public void OnClickDoor()
    {
        SceneManager.LoadScene("IdolRoom");
    }
    public void OnClickStair()
    {
        SceneManager.LoadScene("LivingRoom");
    }
}
