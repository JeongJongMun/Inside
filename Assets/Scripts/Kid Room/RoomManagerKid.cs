using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKid : RoomManager
{
    public void AddTrick(Trick trick)
    {
        if (tricks.Contains(trick)) 
        {
            Debug.Log("이미 해당 트릭이 리스트에 존재하고 있음.");
        }
        else
        {
            tricks.Add(trick);
        }
    }
    public void RemoveTrick(Trick trick)
    {
        if (tricks.Contains(trick))
        {
            tricks.Remove(trick);
        }
        else
        {
            Debug.Log("해당 트릭이 리스트에 존재하지 않아서 제거하지 못함.");
        }
    }
    public void OnClickExitHole()
    {
        SceneManager.LoadScene("IdolRoom");
    }
}
