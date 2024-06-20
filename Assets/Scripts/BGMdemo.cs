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
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //音(sound2)を鳴らす
            audioSource.PlayOneShot(sound2);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound3);
        }
    }
}