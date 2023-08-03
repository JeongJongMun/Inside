using System;
using UnityEngine;

// 트릭들의 추상 클래스
public abstract class Trick : MonoBehaviour
{
    private bool isSolved = false;

    public void Solve()
    {
        this.isSolved = true;
        StartCoroutine(GameManager.Instance.FadeInOut());

    }
    public bool IsSolved()
    {
        return this.isSolved;
    }

    public abstract void SolveOrNotSolve(GameObject obj);
}
