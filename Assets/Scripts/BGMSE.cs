using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSE : MonoBehaviour
{

    AudioSource audioSource;


    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //音(sound1)を鳴らす
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //音(sound3)を鳴らす
            GetComponents<AudioSource>()[1].Play();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //音(sound2)を鳴らす
            GetComponents<AudioSource>()[0].Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            //音(sound2)を鳴らす
            GetComponents<AudioSource>()[0].Play();
        }
    }
}