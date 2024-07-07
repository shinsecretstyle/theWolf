using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{
    private bool firstPush = false;


    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start!");
        

        if (!firstPush)
        {
            Debug.Log("Go Next Scense!");

            //ここに次のシーンへ行く命令を書く
            SceneManager.LoadScene("WolfMap2");
            //
            firstPush = true;
        }
        
    }
}