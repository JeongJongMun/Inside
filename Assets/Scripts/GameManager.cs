using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // ���� ���� GameManager �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ� �ı� X
        }
        else Destroy(gameObject);
    }

    // GameManager �ν��Ͻ��� �����ϴ� ������Ƽ
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
    �ʿ��� ���� ����
    ���ŷ� ����Ʈ
    ���� ��ô��(� Ʈ���� Ǯ������)
    

     
     */


    public GameObject settingPanel;

    public Image fadeImage; // Ʈ���� Ǯ�� ����Ʈ �̹���
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;

    public void OnClickSettingBtn()
    {
        // ����â�� Ȱ��ȭ ���¶�� ��Ȱ��ȭ
        if (settingPanel.activeSelf) settingPanel.SetActive(false);
        // ����â�� ��Ȱ��ȭ ���¶�� Ȱ��ȭ
        else settingPanel.SetActive(true);
    }

    // ������ Ŭ�� ��
    public void OnClickItem(GameObject _item)
    {
        // �κ��丮�� �߰�
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // ȭ�鿡 �ִ� ������ ����
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
