using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagerKid : RoomManager
{
    public void AddTrick(Trick trick)
    {
        if (tricks.Contains(trick)) 
        {
            Debug.Log("�̹� �ش� Ʈ���� ����Ʈ�� �����ϰ� ����.");
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
            Debug.Log("�ش� Ʈ���� ����Ʈ�� �������� �ʾƼ� �������� ����.");
        }
    }
    public void OnClickExitHole()
    {
        SceneManager.LoadScene("IdolRoom");
    }
}
