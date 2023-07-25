using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    public GameObject[] wallPanel;
    private int currentWallPanel = 0;
    private void Start()
    {
        
    }

    public void OnClickItem(Item _item)
    {
        Inventory.Instance.AcquireItem(_item);
    }

    // 왼쪽 화살표 클릭 시
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

    // 오른쪽 화살표 클릭 시
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
