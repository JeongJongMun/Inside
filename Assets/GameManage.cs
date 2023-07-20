using PlayFab.EconomyModels;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    Dictionary<string, bool> trick_kid = new Dictionary<string, bool>()
    {

        /*
        ���̹� Ʈ��
        1) ���� 1
        1-1. �ð� ����
        1-2. ������ �Ӹ� �߸�
        1-3. ������ ���

        2) ���� 2
        2-1. ���� ���� �߸�

        3) ���� 3
        3-1. �������� �߸�

        4) ���� 4
        4-1. ���� ���� ��ġ
        4-2. ���� ���ӱ� ��ġ
        4-3. ���� �и�
         */

        {"clock", false}, 
        {"bear", false },
        {"drawer", false },
        {"familyPicture", false },
        {"worldMap", false },
        {"legoInLibrary", false },
        {"LibraryOpen", false }
    };

    // ���� ���� GameManage �ν��Ͻ��� �� instance�� ��� �༮�� ����
    // ������ ���� private
    private static GameManage instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ� �ı� X
        }
        else Destroy(gameObject);
    }

    // GameManage �ν��Ͻ��� �����ϴ� ������Ƽ
    public static GameManage Instance
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
    �κ��丮, ���ŷ� ����Ʈ
    ���� ��ô��(� Ʈ���� Ǯ������)
    

     
     */




}
