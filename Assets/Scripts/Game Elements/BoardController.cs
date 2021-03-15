using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public BallController ball;
    public Transform camFocus;
    public BallIndicator ballIndicator;
    public int boardNumber = 1;

    private void Start()
    {
        ball.ballNumber = boardNumber;
    }
}
