using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using System.Text;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int lastLevelCompleted { get; set; }
    public bool sound { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            loadProgress();
            sound = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void saveProgress()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        System.IO.FileStream file;
        byte[] towrite = Encoding.ASCII.GetBytes(lastLevelCompleted + "");

        if (System.IO.File.Exists(destination)) file = System.IO.File.OpenWrite(destination);
        else file = System.IO.File.Create(destination);

        file.Write(towrite, 0, towrite.Length);
        file.Close();

        print("wrote " + lastLevelCompleted);
    }

    private void loadProgress()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        System.IO.FileStream file;

        if (System.IO.File.Exists(destination)) file = System.IO.File.OpenRead(destination);
        else
        {
            lastLevelCompleted = 0;
            return;
        }

        byte[] buffer = new byte[10];
        file.Read(buffer, 0, 10);
        file.Close();

        lastLevelCompleted = Int32.Parse(Encoding.ASCII.GetString(buffer));
    }

    public void setLastLevelCompleted(int level)
    {
        if (level > lastLevelCompleted)
        {
            lastLevelCompleted = level;
            saveProgress();
        }
    }
}
