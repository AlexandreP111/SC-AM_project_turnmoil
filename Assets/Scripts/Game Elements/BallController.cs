using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject spawnPoint;
    public float speed = 5.0f;

    // Movement variables
    float movementLeft;
    float totalMoved;
    Vector3 previousPosition;
    Vector3 direction;
    public bool moving { get; private set; }
    public int ballNumber { get; set; }
    bool finished;
    bool dead;

    SphereCollider sphereCollider;

    Queue<Collider> collisions;
    AudioManager audioManager;
    LevelController levelController;



    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        spawn();
        levelController = LevelController.Instance;
        audioManager = LevelController.Instance.GetComponent<AudioManager>();
    }

    public bool issueMove(Vector3 dir)
    {
        if (moving || finished || dead)
            return false;
        direction = dir;
        totalMoved = 0;
        if (canMove())
        {
            moving = true;
            return true;
        }
        return false;
    }

    public void spawn()
    {
        moving = false;
        finished = false;
        dead = false;
        transform.position = spawnPoint.transform.position;
        previousPosition = transform.position;
        collisions = new Queue<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
            return;

        float move_left = Time.deltaTime * speed;

        while (move_left > 0 && moving)
        {
            totalMoved += move_left;
            if (totalMoved >= 1.0f)
            {
                move_left = totalMoved - 1.0f;
                totalMoved = 0;
                transform.position = previousPosition + direction;
                previousPosition = transform.position;

                handleStopTrigger();

                // Check next position and keep going if we can
                if (!canMove())
                {
                    moving = false;
                }
            }
            else
            {
                // Movement
                transform.position += move_left * direction;
                move_left = 0.0f;
            }
        }
    }

    bool canMove()
    {
        if (finished || dead)
            return false;

        Vector3 spherePos = transform.position;
        spherePos += sphereCollider.center;
        spherePos += direction;

        bool tmp = Physics.CheckSphere(spherePos, 0.35f, 1, QueryTriggerInteraction.Ignore);
        return !tmp;
    }

    private void handleStopTrigger()
    {
        if (collisions.Count <= 0)
            return;

        SpeedPad sp = collisions.Peek().GetComponent<SpeedPad>();
        if (sp)
        {
            direction = sp.direction;
            return;
        }

        EndPortal ep = collisions.Peek().GetComponent<EndPortal>();
        if (ep)
        {
            finished = true;
            moving = false;
            audioManager.endPortal.Play();
            levelController.BallEnd(ballNumber - 1);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killer")
        {
            moving = false;
            dead = true;
            audioManager.spikes.Play();
            // Play dying animation
            return;
        }

        Pressable pp = other.GetComponent<Pressable>();
        if (pp)
        {
            pp.press();
        }

        collisions.Enqueue(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (collisions.Count <= 0)
            return;

        Pressable pp = other.GetComponent<Pressable>();
        if (pp)
        {
            pp.depress();
        }

        collisions.Dequeue();
    }
}
