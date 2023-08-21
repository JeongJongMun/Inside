using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeInputSlots : MonoBehaviour
{
    [SerializeField]
    [Header("입력된 큐브 수")]
    private int count = 0;

    [Header("입력된 큐브들 위치")]
    public List<int> inputPos = new List<int>()
    {
        -1, -1, -1, -1, -1
    };

    [Header("입력된 큐브들 각도")]
    public List<int> inputAngle = new List<int>()
    {
        -1, -1, -1, -1, -1
    };

    [Header("이동할 큐브들")]
    public GameObject[] cubes;

    [Header("TrySolve 호출을 위한 CubePuzzle")]
    public GameObject cubePuzzle;

    public void OnClickSlot(GameObject slot)
    {
        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<Toggle>().isOn)
            {
                // 큐브 위치 이동
                cube.transform.position = slot.transform.position;
                // 큐브 토글 끄기
                cube.GetComponent<Toggle>().isOn = false;
                cube.GetComponent<Toggle>().interactable = false;
                // 큐브가 입력된 위치와 각도 저장
                inputPos[int.Parse(slot.name)] = int.Parse(cube.name);
                inputAngle[int.Parse(slot.name)] = (int)cube.transform.eulerAngles.z;
                // 입력된 큐브 수++
                count++;
                // 큐브를 모두 입력했을때
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
