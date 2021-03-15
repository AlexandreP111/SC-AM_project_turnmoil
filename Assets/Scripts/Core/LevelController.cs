using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    GameController gc;

    List<ToggleWall>[] tWalls;
    List<Pressable> pressables;
    BoardController[] boards;
    bool[] finishedStatus;
    BoardController activeBoard;
    AudioManager audioManager;
    public int moveCounter { get; private set; }
    public bool animationMode { get; set; }

    public Transform camFocus;
    public WinScreen winScreen;
    public int levelNumber = 1;


    bool gameOver;
    float winTimer;

    
    void Awake()
    {
        Instance = this;
        transform.position = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        gc = GameController.Instance;

        winTimer = 0;

        audioManager = GetComponent<AudioManager>();

        moveCounter = 0;
        animationMode = false;

        // Get all boards
        GameObject[] boardList = GameObject.FindGameObjectsWithTag("Board");
        boards = new BoardController[boardList.Length];
        finishedStatus = new bool[boardList.Length];

        for (int i = 0; i < boardList.Length; ++i)
        {
            BoardController tmp = boardList[i].GetComponent<BoardController>();
            boards[tmp.boardNumber - 1] = tmp;
            tmp.ballIndicator.Initialize(boardList.Length, tmp.boardNumber - 1);
        }

        activateBoard(1);


        // Walls
        tWalls = new List<ToggleWall>[4];
        for (int i = 0; i < 4; ++i)
        {
            tWalls[i] = new List<ToggleWall>();
        }

        GameObject[] walls = GameObject.FindGameObjectsWithTag("ToggleWall");
        foreach (GameObject o in walls)
        {
            ToggleWall tw = o.GetComponent<ToggleWall>();
            tWalls[tw.type - 1].Add(tw);
        }


        // Buttons
        pressables = new List<Pressable>();
        GameObject[] ps = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject p in ps)
        {
            Pressable prs = p.GetComponent<OneOffButton>();
            if (!prs)
                prs = p.GetComponent<PressurePlate>();
            pressables.Add(prs);
        }
    }

    public void toggleWalls(int type)
    {
        foreach (ToggleWall tw in tWalls[type - 1])
        {
            tw.toggle();
        }
    }

    public void activateBoard(int number)
    {
        activeBoard = boards[number - 1];
        camFocus.position = activeBoard.camFocus.position;
    }

    public void restart()
    {
        moveCounter = 0;

        // Reset boards
        foreach (BoardController bc in boards)
        {
            bc.ball.spawn();
            bc.ballIndicator.ResetFinished();
        }

        // Reset finished status
        for (int i = 0; i < finishedStatus.Length; ++i)
        {
            finishedStatus[i] = false;
        }

        // Reset buttons
        foreach (Pressable p in pressables)
            p.reset();

        // Reset walls
        foreach (List<ToggleWall> twl in tWalls)
            foreach(ToggleWall tw in twl)
                tw.reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            return;
        }

        // Gameplay actions will be subject to movement and animation mode
        if (!animationMode && !activeBoard.ball.moving)
        {
            // Handle input
            bool valid_move = false;

            float h_axis = Input.GetAxisRaw("Horizontal");
            if (h_axis > 0)
            {
                valid_move = activeBoard.ball.issueMove(new Vector3(-1.0f, 0.0f, 0.0f));
            }
            else if (h_axis < 0)
            {
                valid_move = activeBoard.ball.issueMove(new Vector3(1.0f, 0.0f, 0.0f));
            }

            float v_axis = Input.GetAxisRaw("Vertical");
            if (v_axis > 0)
            {
                valid_move = activeBoard.ball.issueMove(new Vector3(0.0f, 0.0f, -1.0f));
            }
            else if (v_axis < 0)
            {
                valid_move = activeBoard.ball.issueMove(new Vector3(0.0f, 0.0f, 1.0f));
            }

            if (valid_move)
            {
                audioManager.move.Play();
                moveCounter++;
            }
                


            // Moving between boards
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (boards[0])
                    activateBoard(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (boards.Length > 1)
                    activateBoard(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (boards.Length > 2)
                    activateBoard(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (boards.Length > 3)
                    activateBoard(4);
            }


            // Reset
            if (Input.GetKeyDown(KeyCode.R))
            {
                restart();
            }
        }
        
        // Menu actions
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        // Win check
    }

    public void BallEnd(int ballNum)
    {
        finishedStatus[ballNum] = true;
        foreach (BoardController bc in boards)
        {
            bc.ballIndicator.SetFinished(ballNum);
        }

        bool over = true;
        foreach (bool f in finishedStatus)
        {
            if (!f)
                over = false;
        }

        gameOver = over;
        if (gameOver)
        {
            gc.setLastLevelCompleted(levelNumber);
            winScreen.DisplayScreen(levelNumber, moveCounter);
            audioManager.levelFinish.Play();
        }
    }
}
