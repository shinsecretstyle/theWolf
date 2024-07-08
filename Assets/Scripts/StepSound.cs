using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    AudioSource AS;

    public AudioClip phase1left;
    public AudioClip phase1right;
    public AudioClip phase2left;
    public AudioClip phase2right;
    public AudioClip phase3left;
    public AudioClip phase3right;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void phase1Left()
    {
        AS.PlayOneShot(phase1left);
    }

    public void phase1Right()
    {
        AS.PlayOneShot(phase1right);
    }

    public void phase2Left() 
    {
        AS.PlayOneShot(phase2left);
    }
    public void phase2Right()
    {
        AS.PlayOneShot(phase2right);
    }
    public void phase3Left()
    {
        AS.PlayOneShot(phase3left);
    }
    public void phase3Right()
    {
        AS.PlayOneShot(phase3right);
    }
}
