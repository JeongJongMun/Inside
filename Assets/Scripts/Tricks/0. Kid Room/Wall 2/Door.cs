using UnityEngine;
using UnityEngine.UI;
/* Door.cs
 * 0. Kid Room - Wall 2
 */
public class Door : MonoBehaviour
{
    private void Awake() => gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    private void OnClick() => Managers.Sound.Play("doorLocked");
}