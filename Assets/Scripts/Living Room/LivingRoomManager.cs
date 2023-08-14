using UnityEngine.SceneManagement;
using UnityEngine;

public class LivingRoomManager : RoomManager
{
    public void OnClickHallwayStair()
    {
        SceneManager.LoadScene("Hallway");
    }
    public void OnClickResearcherDoor()
    {
        if (DatabaseManager.Instance.IsTrickSolved(Define.RoomName.Living, Define.TrickName.CardReader))
            SceneManager.LoadScene("ResearcherRoom");
    }
    public void OnClickCEODoor()
    {
        Debug.Log("CEO������ �̵�");
        //SceneManager.LoadScene("CEORoom");
    }
    public void OnClickOutDoor()
    {
        // ��� �Ҹ� ���
    }
}
