using UnityEngine;
/* Item.cs
 * 아이템을 관리하는 스크립트
 * 아이템은 필드 or 인벤토리에 있음
 * 필드에 있는 아이템 클릭 -> 인벤토리에 추가
 * 인벤토리에 있는 아이템 클릭하여 사용
 * 인벤토리에 있는 아이템은 이미지와 이름만 있으면 됨
 */
public class NewItem : MonoBehaviour
{
#region Private Variables
#endregion

#region Public Variables
    [HideInInspector] public Define.ItemName itemName = Define.ItemName.None;
    public Sprite icon = null;
#endregion

#region Private Methods
#endregion

#region Public Methods
    public void InitializeItem()
    {
        string path = this.GetComponentInParent<RoomInfo>().roomPath;
        icon = Resources.Load<Sprite>($"Sprites/Items/{path}/{this.name}");
        itemName = System.Enum.Parse<Define.ItemName>(this.name);
    }
#endregion
}