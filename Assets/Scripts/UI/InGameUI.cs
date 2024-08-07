using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
#region Private Variables
#endregion

#region Public Variables
    public static InGameUI instance;
    public Button[] settingButtons;     // 설정 패널 On/Off 버튼
    public Button saveButton;           // 저장 버튼
    public Button reviewButton;         // 리뷰 버튼
    public GameObject settingPanel;     // 설정 패널
    public GameObject gameoverPanel;    // 게임오버 패널
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        settingPanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }
    private void Start()
    {
        foreach (Button button in settingButtons)
        {
            button.onClick.AddListener(ToggleSettingPanel);
        }
        saveButton.onClick.AddListener(SaveGame);
        reviewButton.onClick.AddListener(ReviewGame);
    }
    private void ToggleSettingPanel()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    private void SaveGame()
    {
        DatabaseManager.Instance.SaveData();
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    private void ReviewGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.openthedoorandscream.inside");
    }
#endregion

#region Public Methods
#endregion
}