using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{
    private bool firstPush = false;


    //�X�^�[�g�{�^���������ꂽ��Ă΂��
    public void PressStart()
    {
        Debug.Log("Press Start!");
        

        if (!firstPush)
        {
            Debug.Log("Go Next Scense!");

            //�����Ɏ��̃V�[���֍s�����߂�����
            SceneManager.LoadScene("WolfMap2");
            //
            firstPush = true;
        }
        
    }
}