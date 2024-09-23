using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSound : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource AS;
    public AudioClip sleep;
    public AudioClip speak;
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep()
    {
        AS.PlayOneShot(sleep);
    }
    public void Speak()
    {
        AS.PlayOneShot(speak);
    }
}
