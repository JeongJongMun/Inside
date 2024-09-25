using UnityEngine;
using UnityEngine.Serialization;

/* RoomInfo.cs
 * 각 방의 이름을 가져오는 스크립트
 */
public class RoomInfo : MonoBehaviour
{
#region Public Variables
    public string roomPath;
    [FormerlySerializedAs("roomName")] public Define.ERoomType eRoomType;
#endregion

#region Private Methods
    private void Awake()
    {
        roomPath = gameObject.name;
        string _roomName = roomPath.Split(' ')[1];
        eRoomType = System.Enum.Parse<Define.ERoomType>(_roomName);
    }
#endregion
}
