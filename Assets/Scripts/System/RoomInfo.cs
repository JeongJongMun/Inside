using UnityEngine;
/* RoomInfo.cs
 * 각 방의 이름을 가져오는 스크립트
 */
public class RoomInfo : MonoBehaviour
{
#region Public Variables
    public string roomPath;
    public Define.RoomName roomName;
#endregion

#region Private Methods
    private void Awake()
    {
        roomPath = gameObject.name;
        string _roomName = roomPath.Split(' ')[1];
        roomName = System.Enum.Parse<Define.RoomName>(_roomName);
    }
#endregion
}
