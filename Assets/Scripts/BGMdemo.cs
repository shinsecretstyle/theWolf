using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMdemo : MonoBehaviour
{

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;

    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ��
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //��(sound1)��炷
            audioSource.PlayOneShot(sound1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //��(sound2)��炷
            audioSource.PlayOneShot(sound2);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //��(sound3)��炷
            audioSource.PlayOneShot(sound3);
        }
    }
}