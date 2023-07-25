using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    private void Start()
    {
        
    }

    public void OnClickItem(Item _item)
    {
        Inventory.Instance.AcquireItem(_item);
    }

    // ���� ȭ��ǥ Ŭ�� ��
    public void OnClickLeftArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        if (currentWallPanel == 0)
        {
            currentWallPanel = 3;
        }
        else
        {        
            currentWallPanel--;
        }
        wallPanel[currentWallPanel].SetActive(true);

    }

    // ������ ȭ��ǥ Ŭ�� ��
    public void OnClickRightArrow()
    {
        wallPanel[currentWallPanel].SetActive(false);
        if (currentWallPanel == 3)
        {
            currentWallPanel = 0;
        }
        else
        {
            currentWallPanel++;
        }
        wallPanel[currentWallPanel].SetActive(true);
    }



}
