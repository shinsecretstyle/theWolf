using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaciourScript : MonoBehaviour
{

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;

    void Start()
    {
        //Component‚ğæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ¶
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //‰¹(sound1)‚ğ–Â‚ç‚·
            audioSource.PlayOneShot(sound1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //‰¹(sound2)‚ğ–Â‚ç‚·
            audioSource.PlayOneShot(sound2);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //‰¹(sound1)‚ğ–Â‚ç‚·
            audioSource.PlayOneShot(sound3);
        }
    }
}