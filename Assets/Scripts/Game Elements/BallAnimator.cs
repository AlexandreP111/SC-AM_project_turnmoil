using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimator : MonoBehaviour
{
    public float squashMagnitude = 0.3f;
    public float squashSpeed = 1f;

    float squash;
    float ang;

    // Start is called before the first frame update
    void Start()
    {
        ang = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ang += squashSpeed * Time.deltaTime;
        if (ang >= Math.PI * 2)
            ang -= (float)Math.PI * 2;

        transform.localScale = new Vector3(1, 1 - ((float)Math.Sin(ang) + 1) / 2 * squashMagnitude, 1);
    }
}
