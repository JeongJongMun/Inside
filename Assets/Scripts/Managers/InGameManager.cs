using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
/* InGameManager.cs
 * 게임 내부에서 발생하는 이벤트를 관리하는 스크립트
 */
public class InGameManager : MonoBehaviour
{
#region Private Variables
    private const float BLINK_TIME = 0.15f;
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider audioSlider;
#endregion

#region Public Variables
    public GameObject[] mentalImage;
    public GameObject gameoverPanel;
    public Image blinkPanel;
#endregion

#region Private Methods
    private IEnumerator Blinking(Color _color)
    {
        yield return StartCoroutine(Blink(0, 1, _color));
        yield return StartCoroutine(Blink(1, 0, _color));
    }
    private IEnumerator Blink(float start, float end, Color _color)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        blinkPanel.color = _color;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / BLINK_TIME;

            Color color = blinkPanel.color;
            color.a = Mathf.Lerp(start, end, percent);
            blinkPanel.color = color;
            yield return null;
        }
    }
#endregion

#region Public Methods
    public void BlinkingEffect (Color _color) => StartCoroutine(Blinking(_color));
    public void SetMusicVolume(Slider slider)
    {
        float volume = slider.value;
        masterMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
    }
#endregion
    public void OnClickExitBtn(GameObject panel)
    {
        panel.SetActive(false);
        SoundManager.instance.SFXPlay("buttonSound");
        StartCoroutine(LoadMain());
    }
    private IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }
    public void MentalBreak()
    {
        DatabaseManager.Instance.MentalPointData--;
        
        for (int i = 0; i < 3; i++) {
            if (i < DatabaseManager.Instance.MentalPointData) {
                mentalImage[i].SetActive(true);
            }
            else mentalImage[i].SetActive(false);
        }
        // Game Over
        if (DatabaseManager.Instance.MentalPointData == 0) {
            gameoverPanel.SetActive(true);
        }
    }
    public void MentalRecovery()
    {
        DatabaseManager.Instance.MentalPointData = 3;
        for (int  i = 0; i < 3; i++){
            mentalImage[i].SetActive(true);
        }
    }
}