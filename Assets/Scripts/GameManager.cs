using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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


    [Header("���ŷ� ����Ʈ")]
    public int mentalPoint = 3;

    [Header("���ŷ� ����Ʈ �̹��� �迭")]
    public GameObject[] mentalImage;

    [Header("����â �г�")]
    public GameObject settingPanel;

    [Header("Ʈ�� ���� ����Ʈ")]
    public Image fadeImage;

    [SerializeField]
    [Header("����Ʈ �ӵ�")]
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
        if (SceneManager.GetActiveScene().name == "KidRoom")
            SceneManager.LoadScene("IdolRoom");
        else if (SceneManager.GetActiveScene().name == "IdolRoom")
            SceneManager.LoadScene("KidRoom");
    }

    // Ʈ�� ���� �� ����Ʈ

    public void FadeInOut()
    {
        StartCoroutine(DoFadeInOut());
    }
    private IEnumerator DoFadeInOut()
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

    // ��Ż ����Ʈ -1
    public void MentalBreak()
    {
        mentalPoint--;
        for (int i = 0; i < 3; i++)
        {
            if (i < mentalPoint)
                mentalImage[i].SetActive(true);
            else 
                mentalImage[i].SetActive(false);
        }

    }
}