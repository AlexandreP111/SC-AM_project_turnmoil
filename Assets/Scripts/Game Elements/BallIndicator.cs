using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Video;

public class BallIndicator : MonoBehaviour
{
    public MeshRenderer[] balls;
    public Material matDefault;
    public Material matSelected;
    public Material matFinished;

    int ballCount;
    int selected;
    bool[] ballStates;

    private void Awake()
    {
        ballStates = new bool[balls.Length];
    }

    public void Initialize(int _ballCount, int _selected)
    {
        ballCount = _ballCount;

        int counter = 0;
        for (; counter < ballCount; ++counter)
        {
            balls[counter].gameObject.SetActive(true);
            balls[counter].material = matDefault;
            ballStates[counter] = false;
        }

        for (; counter < balls.Length; ++counter)
        {
            balls[counter].gameObject.SetActive(false);
        }

        selected = _selected;
        balls[selected].material = matSelected;
    }

    public void SetFinished(int num)
    {
        ballStates[num] = true;
        if (num != selected)
            balls[num].material = matFinished;
    }

    public void ResetFinished()
    {
        for (int i = 0; i < ballCount; ++i)
        {
            ballStates[i] = false;
            if (i != selected)
                balls[i].material = matDefault;
        }
    }
}
