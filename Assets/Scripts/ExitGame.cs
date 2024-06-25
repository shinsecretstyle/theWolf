using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            #if UNITY_EDITOR // エディターに
                UnityEditor.EditorApplication.isPlaying = false;
            #else //ビルドしたデータに
                Application.Quit();
            #endif
        }
    }
}
