using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeInputSlots : MonoBehaviour
{
    [SerializeField]
    [Header("�Էµ� ť�� ��")]
    private int count = 0;

    [Header("�Էµ� ť��� ��ġ")]
    public List<int> inputPos = new List<int>()
    {
        -1, -1, -1, -1, -1
    };

    [Header("�Էµ� ť��� ����")]
    public List<int> inputAngle = new List<int>()
    {
        -1, -1, -1, -1, -1
    };

    [Header("�̵��� ť���")]
    public GameObject[] cubes;

    [Header("TrySolve ȣ���� ���� CubePuzzle")]
    public GameObject cubePuzzle;

    public void OnClickSlot(GameObject slot)
    {
        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<Toggle>().isOn)
            {
                // ť�� ��ġ �̵�
                cube.transform.position = slot.transform.position;
                // ť�� ��� ����
                cube.GetComponent<Toggle>().isOn = false;
                cube.GetComponent<Toggle>().interactable = false;
                // ť�갡 �Էµ� ��ġ�� ���� ����
                inputPos[int.Parse(slot.name)] = int.Parse(cube.name);
                inputAngle[int.Parse(slot.name)] = (int)cube.transform.eulerAngles.z;
                // �Էµ� ť�� ��++
                count++;
                // ť�긦 ��� �Է�������
                if (count == cubes.Length)
                {
                    count = 0;
                    cubePuzzle.GetComponent<CEORoomCubePuzzle>().TrySolve(cubePuzzle);
                }
                break;
            }
        }
    }
}
