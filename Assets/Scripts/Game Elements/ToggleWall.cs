using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWall : MonoBehaviour
{
    public GameObject wallSolid;
    public GameObject wallFaded;
    public bool solidOnStart = true;
    public int type = 1;
    private bool solid;

    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    public void reset()
    {
        setSolid(solidOnStart);
    }

    public void toggle()
    {
        setSolid(!solid);
    }

    void setSolid(bool solid)
    {
        this.solid = solid;
        wallSolid.SetActive(false);
        wallFaded.SetActive(false);
        (solid ? wallSolid : wallFaded).SetActive(true);
    }

    public void OnValidate()
    {
        setSolid(solidOnStart);
    }
}
