using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeRotate : MonoBehaviour
{
    [Header("회전 할 큐브들")]
    public GameObject[] cubes;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickRotate);
    }
    public void OnClickRotate()
    {
        foreach (GameObject cube in cubes)
        {
            if (cube.GetComponent<Toggle>().isOn)
            {
                cube.transform.Rotate(Vector3.forward * 90);
                break;
            }
        }
    }
}
