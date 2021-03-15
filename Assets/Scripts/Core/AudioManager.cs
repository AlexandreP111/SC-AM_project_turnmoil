using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource levelFinish;
    public AudioSource endPortal;
    public AudioSource move;
    public AudioSource spikes;

    bool soundOn;

    private void Start()
    {
        soundOn = GameController.Instance.sound;
        if (!soundOn)
        {
            levelFinish.volume = 0;
            endPortal.volume = 0;
            move.volume = 0;
            spikes.volume = 0;
        }
    }
}
