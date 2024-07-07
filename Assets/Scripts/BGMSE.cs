using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSE : MonoBehaviour
{

    AudioSource audioSource;


    void Start()
    {
        //Component‚ğæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ¶
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //‰¹(sound1)‚ğ–Â‚ç‚·
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //‰¹(sound3)‚ğ–Â‚ç‚·
            GetComponents<AudioSource>()[1].Play();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //‰¹(sound2)‚ğ–Â‚ç‚·
            GetComponents<AudioSource>()[2].Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            //‰¹(sound2)‚ğ–Â‚ç‚·
            GetComponents<AudioSource>()[2].Play();
        }
    }
}