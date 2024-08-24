using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
#region Private Variables
    private TrickManager trickManager;
    private const float HINT_DELAY = 3f;
#endregion

#region Public Variables
    public static InGameUI instance;
    public Button saveButton;           // 저장 버튼
    public Button reviewButton;         // 리뷰 버튼
    public Button[] settingButtons;     // 설정 패널 On/Off 버튼
    public GameObject settingPanel;     // 설정 패널
    public Button hintButton;           // 힌트 버튼
    public GameObject hintPanel;        // 힌트 패널
    public TMP_Text hintText;           // 힌트 텍스트
    public GameObject gameoverPanel;    // 게임오버 패널
#endregion

#region Private Methods
    private void Awake()
    {
        instance = this;
        settingPanel.SetActive(false);
        gameoverPanel.SetActive(false);
        trickManager = FindObjectOfType<TrickManager>();
    }
    private void Start()
    {
        foreach (Button button in settingButtons) {
            button.onClick.AddListener(ToggleSettingPanel);
        }
        saveButton.onClick.AddListener(SaveGame);
        reviewButton.onClick.AddListener(ReviewGame);
        hintButton.onClick.AddListener(() => ShowHint(trickManager.trickGraph.GetHint()));
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
    private IEnumerator HideHintAfterDelay(string _hint, float delay)
    {
        hintPanel.SetActive(true);
        hintText.text = _hint;

        yield return new WaitForSeconds(delay);

        hintPanel.SetActive(false);
        hintText.text = "";
    }
#endregion

#region Public Methods
    public void ShowHint(string _hint)
    {
        StartCoroutine(HideHintAfterDelay(_hint, HINT_DELAY));
    }
#endregion
}