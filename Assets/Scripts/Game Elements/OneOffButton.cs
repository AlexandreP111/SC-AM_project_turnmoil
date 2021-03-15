using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using UnityEngine;

public class OneOffButton : Pressable
{
    public GameObject upObj;
    public GameObject downObj;
    public int type = 1;

    bool down;

    // Singleton instance

    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    public override void reset()
    {
        upObj.SetActive(true);
        downObj.SetActive(false);
        down = false;
    }

    // Update is called once per frame
    public override void press()
    {
        if (down)
            return;
        upObj.SetActive(false);
        downObj.SetActive(true);
        down = true;
        LevelController.Instance.toggleWalls(type);
    }
}
