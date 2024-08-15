using UnityEngine;
using UnityEngine.UI;
/* Door.cs
 * 0. Kid Room - Wall 2
 */
public class Door : MonoBehaviour
{
#region Private Methods
    private void Awake() => gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    private void OnClick() => GameManager.instance.soundManager.Play("doorLocked");
#endregion
}
