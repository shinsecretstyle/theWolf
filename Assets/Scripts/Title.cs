﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;

    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Time.timeScale = 1;
        Debug.Log("Press Start!");


        if (!firstPush)
        {
            Debug.Log("Go Next Scense!");
            fade.StartFadeOut();
            firstPush = true;
        }

    }


    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Prologue");
            goNextScene = true;
        }
    }
}