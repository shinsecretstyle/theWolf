using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSE : MonoBehaviour
{

    AudioSource audioSource;


    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //��(sound1)��炷
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //��(sound3)��炷
            GetComponents<AudioSource>()[1].Play();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //��(sound2)��炷
            GetComponents<AudioSource>()[2].Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            //��(sound2)��炷
            GetComponents<AudioSource>()[2].Play();
        }
    }
}