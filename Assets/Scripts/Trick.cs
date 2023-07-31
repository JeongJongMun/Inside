using System;
using UnityEngine;

// Ʈ������ �߻� Ŭ����
public abstract class Trick : MonoBehaviour
{
    private bool isSolved = false;

    public void Solve()
    {
        this.isSolved = true;
    }
    public bool IsSolved()
    {
        return this.isSolved;
    }

    public abstract void SolveOrNotSolve(GameObject obj);
}
