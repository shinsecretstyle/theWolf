using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSound : MonoBehaviour
{
    AudioSource AS;

    public AudioClip speak;
    public AudioClip sleep;


    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void Speak()
    {
        AS.PlayOneShot(speak);
    }

    public void Sleep()
    {
        AS.PlayOneShot(sleep);
    }
}