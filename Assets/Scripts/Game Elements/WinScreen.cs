using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject masterObject;
    public TextMesh levelIndicator;
    public TextMesh moveIndicator;

    private void Start()
    {
        masterObject.SetActive(false);
    }

    public void DisplayScreen(int levelNumber, int moveCount)
    {
        levelIndicator.text = "Level " + levelNumber + " Complete!";
        moveIndicator.text = "Moves: " + moveCount;
        masterObject.SetActive(true);
    }
}
