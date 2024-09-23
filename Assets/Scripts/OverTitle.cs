using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//文字化け直す必要

public class OverTitle : MonoBehaviour
{
    [Header("�t�F�[�h")] public FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;

    //�X�^�[�g�{�^���������ꂽ��Ă΂��
    public void PressStart()
    {
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
            SceneManager.LoadScene("Title2");
            goNextScene = true;
        }
    }
}