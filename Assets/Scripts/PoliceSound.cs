using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSound : MonoBehaviour
{
    AudioSource AS;

    public AudioClip stick;
    public AudioClip whistle;


    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void Stick()
    {
        AS.PlayOneShot(stick);
    }

    public void Whistle()
    {
        AS.PlayOneShot(whistle);
    }
}