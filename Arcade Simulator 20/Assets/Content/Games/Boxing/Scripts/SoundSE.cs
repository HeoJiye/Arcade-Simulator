using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSE : MonoBehaviour
{
    public static SoundSE instance;

    public AudioClip btn;
    public AudioClip hit;
    public AudioClip gameOver;
    AudioSource sound;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }

        sound = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip name)
    {
        sound.PlayOneShot(name);
    }
}
