using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CEORoomCubePuzzle : Trick
{
    [SerializeField]
    [Header("정답 위치")]
    private List<int> answerPos = new List<int>()
    {
        0, 1, 2, 3, 4
    };

    [SerializeField]
    [Header("정답 각도")]
    private List<int> answerAngle = new List<int>()
    {
        0, 180, 0, 180, 0
    };

    [Header("CubeInputSlot 스크립트")]
    public CubeInputSlots cubeInput;

    [Header("트릭 성공 시 보여줄 비밀번호")]
    public GameObject password;

    [Header("큐브들")]
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
                // 큐브 원위치 및 활성화
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
