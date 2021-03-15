using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnimator : MonoBehaviour
{
    public float angSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0, 1, 0) * angSpeed * Time.deltaTime;
        transform.Rotate(rotation);
    }
}
