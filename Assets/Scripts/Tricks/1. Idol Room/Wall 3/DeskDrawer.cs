using UnityEngine;
using UnityEngine.UI;

public class DeskDrawer : MonoBehaviour
{
    public Image desk;
    public Sprite deskOpen;
    public GameObject paperHint;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        GameManager.instance.soundManager.Play("drawerOpened");
        desk.sprite = deskOpen;
        paperHint.SetActive(true);
    }
}
