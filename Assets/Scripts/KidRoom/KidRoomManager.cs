using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRoomManager : RoomManager
{

    public void AddTrick(Trick trick)
    {
        if (tricks.Contains(trick)) 
        {
            Debug.Log("�̹� �ش� Ʈ���� ����Ʈ�� �����ϰ� ����.");
        }
        else
        {
            tricks.Add(trick);
        }
    }
    public void RemoveTrick(Trick trick)
    {
        if (tricks.Contains(trick))
        {
            tricks.Remove(trick);
        }
        else
        {
            Debug.Log("�ش� Ʈ���� ����Ʈ�� �������� �ʾƼ� �������� ����.");
        }
    }
    // Ʈ�� Ŭ�� �� Ʈ���鿡�� �˸�



    /*
    ���̹� Ʈ��
    1) ���� 1
    1-1. �ð� ���� : �ð� ��ħ ���� ��
    1-2. ������ �Ӹ� �߸� : Ŀ��Į�� ��ȣ�ۿ�
    1-3. ������ ��� : ����� ����

    2) ���� 2
    2-1. ���� ���� �߸� : Ŀ��Į�� ��ȣ�ۿ�

    3) ���� 3
    3-1. �������� �߸� : Ŀ��Į�� ��ȣ�ۿ�

    4) ���� 4
    4-1. ���� ���� ��ġ : ���� ��ȣ�ۿ�
    4-2. ���� ���ӱ� ��ġ : ���ӱ� ��ȣ�ۿ�
    4-3. ���� �и� : ���ӱ� ���� ����
     */
}
