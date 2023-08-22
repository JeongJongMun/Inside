using UnityEngine;

public class Cube : MonoBehaviour
{
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void ResetPositon()
    {
        transform.position = startPos;
    }
}
