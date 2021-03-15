using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Respawn()
    {

    }

    void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        position += new Vector3(0.5f, 0.4f, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.4f);
    }
}
