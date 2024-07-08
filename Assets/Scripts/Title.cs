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
            Debug.Log("Go Next Scene!");

            // 2秒後にシーン遷移
            StartCoroutine(LoadSceneAfterWait(1.0f));

            firstPush = true;
        }
    }

    IEnumerator LoadSceneAfterWait(float waitTime)
   {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Prologue");
   }
}