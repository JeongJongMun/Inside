using UnityEngine;
/* RoomInfo.cs
 * 방의 이름을 관리하는 스크립트
 */
public class RoomInfo : MonoBehaviour
{
#region Public Variables
    public string roomName;
#endregion

#region Private Methods
    private void Awake()
    {
        roomName = gameObject.name;
    }
#endregion
}
