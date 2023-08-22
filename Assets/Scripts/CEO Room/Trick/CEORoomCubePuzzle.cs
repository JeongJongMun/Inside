using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CEORoomCubePuzzle : Trick
{
    [SerializeField]
    [Header("���� ��ġ")]
    private List<int> answerPos = new List<int>()
    {
        0, 1, 2, 3, 4
    };

    [SerializeField]
    [Header("���� ����")]
    private List<int> answerAngle = new List<int>()
    {
        0, 180, 0, 180, 0
    };

    [Header("CubeInputSlot ��ũ��Ʈ")]
    public CubeInputSlots cubeInput;

    [Header("Ʈ�� ���� �� ������ ��й�ȣ")]
    public GameObject password;

    [Header("ť���")]
    public GameObject[] cubes;
    public override void TrySolve(GameObject obj)
    {
        if (obj.name == this.name)
        {
            if (Enumerable.SequenceEqual(cubeInput.inputPos, answerPos) &&
                Enumerable.SequenceEqual(cubeInput.inputAngle, answerAngle))
            {
                Debug.LogFormat("{0} Solved", this.name);
                SetIsSolved(true);
                SolvedAction();
            }
            else
            {
                Debug.LogFormat("{0} Not Solved", this.name);
                // ť�� ����ġ �� Ȱ��ȭ
                foreach (GameObject cube in cubes)
                {
                    cube.GetComponent<Cube>().ResetPositon();
                    cube.GetComponent<Toggle>().interactable = true;
                    cube.transform.rotation = Quaternion.identity;

                }
            }
        }
    }
    public override void SolvedAction()
    {
        password.SetActive(true);
    }
}
