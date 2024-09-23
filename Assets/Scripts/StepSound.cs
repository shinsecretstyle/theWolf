using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    AudioSource AS;

    public AudioClip phase1left;
    public AudioClip phase1right;
    public AudioClip phase1jumpleft;
    public AudioClip phase1jumpright;
    public AudioClip phase2left;
    public AudioClip phase2right;
    public AudioClip phase2jumpleft;
    public AudioClip phase2jumpright;
    public AudioClip phase3left;
    public AudioClip phase3right;
    public AudioClip laydownmove;
    public AudioClip laydown;
    public AudioClip crouch;
    public AudioClip phase1Ladder;
    public AudioClip phase2Ladder;
    public AudioClip phase3Ladder;

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
    public void phase1JumpLeft()
    {
        AS.PlayOneShot(phase1jumpleft);
    }
    public void phase1JumpRight()
    {
        AS.PlayOneShot(phase1jumpright);
    }

    public void phase2Left()
    {
        AS.PlayOneShot(phase2left);
    }
    public void phase2Right()
    {
        AS.PlayOneShot(phase2right);
    }
    public void phase2jumpLeft()
    {
        AS.PlayOneShot(phase2jumpleft);
    }
    public void phase2jumpRight()
    {
        AS.PlayOneShot(phase2jumpright);
    }
    public void phase3Left()
    {
        AS.PlayOneShot(phase3left);
    }
    public void phase3Right()
    {
        AS.PlayOneShot(phase3right);
    }

    public void Laydownmove()
    {
        AS.PlayOneShot(laydownmove);
    }

    public void Laydown()
    {
        AS.PlayOneShot(laydown);
    }

    public void Crouch()
    {
        AS.PlayOneShot(crouch);
    }

    public void Phase1Ladder()
    {
        AS.PlayOneShot(phase1Ladder);
    }

    public void Phase2Ladder()
    {
        AS.PlayOneShot(phase2Ladder);
    }

    public void Phase3Ladder()
    {
        AS.PlayOneShot(phase3Ladder);
    }
}
