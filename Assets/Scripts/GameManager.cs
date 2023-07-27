using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public GameObject[] wallPanel;
    private int currentWallPanel = 0;

    public GameObject settingPanel;


    // ������ Ŭ�� ��
    public void OnClickItem(GameObject _item)
    {
        // �κ��丮�� �߰�
        Inventory.Instance.AcquireItem(_item.GetComponent<Item>());
        // ȭ�鿡 �ִ� ������ ����
        Destroy(_item);
    }

    // ���� ȭ��ǥ Ŭ�� ��
    public void OnClickLeftArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        currentWallPanel = (currentWallPanel + 3) % 4;
        wallPanel[currentWallPanel].SetActive(true);

    }

    // ������ ȭ��ǥ Ŭ�� ��
    public void OnClickRightArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        currentWallPanel = (currentWallPanel + 1) % 4;
        wallPanel[currentWallPanel].SetActive(true);
    }
    public void OnClickSettingBtn()
    {
        // ����â�� Ȱ��ȭ ���¶�� ��Ȱ��ȭ
        if (settingPanel.activeSelf) settingPanel.SetActive(false);
        // ����â�� ��Ȱ��ȭ ���¶�� Ȱ��ȭ
        else settingPanel.SetActive(true);
    }

    public void OnClickTestBtn()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void OnClickTestBackBtn()
    {
        SceneManager.LoadScene("KidRoom");
    }

}
