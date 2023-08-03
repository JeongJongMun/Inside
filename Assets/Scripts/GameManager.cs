using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // 게임 내에 GameManager 인스턴스는 이 instance에 담긴 녀석만 존재
    // 보안을 위해 private
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에 파괴 X
        }
        else Destroy(gameObject);
    }

    // GameManager 인스턴스에 접근하는 프로퍼티
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    /*
    필요한 전역 변수
    정신력 포인트
    게임 진척도(어떤 트릭을 풀었는지)
    

     
     */


    public GameObject settingPanel;

    public Image fadeImage; // 트릭을 풀때 이펙트 이미지
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;

    public void OnClickSettingBtn()
    {
        // 설정창이 활성화 상태라면 비활성화
        if (settingPanel.activeSelf) settingPanel.SetActive(false);
        // 설정창이 비활성화 상태라면 활성화
        else settingPanel.SetActive(true);
    }

    // 아이템 클릭 시
    public void OnClickItem(GameObject _item)
    {
        // 인벤토리에 추가
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // 화면에 있는 아이템 삭제
        Destroy(_item);
    }
    public void OnClickTestBtn()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void OnClickTestBackBtn()
    {
        SceneManager.LoadScene("KidRoom");
    }
    public IEnumerator FadeInOut()
    {
        yield return StartCoroutine(Fade(0, 1));

        yield return StartCoroutine(Fade(1, 0));

    }
    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;
            yield return null;
        }
    }
}
