using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEscape : MonoBehaviour
{

    private bool firstPush = false;

    //ゲーム終了
    public void EndGame()
    {
        if (!firstPush)
        {
            Debug.Log("END!");
            firstPush = true;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
        }

    }
}
