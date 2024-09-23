using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSound : MonoBehaviour
{
    AudioSource AS;

    public AudioClip voice;
    public AudioClip move;


    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void Voice()
    {
        AS.PlayOneShot(voice);
    }

    public void Move()
    {
        AS.PlayOneShot(move);
    }
}
