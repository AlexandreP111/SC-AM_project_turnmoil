using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Pressable
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

    public override void press()
    {
        if (down)
            return;
        upObj.SetActive(false);
        downObj.SetActive(true);
        down = true;
        LevelController.Instance.toggleWalls(type);
    }

    public override void depress()
    {
        if (!down)
            return;
        upObj.SetActive(true);
        downObj.SetActive(false);
        down = false;
        LevelController.Instance.toggleWalls(type);
    }
}
