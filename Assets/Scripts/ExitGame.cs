using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            #if UNITY_EDITOR // �G�f�B�^�[��
                UnityEditor.EditorApplication.isPlaying = false;
            #else //�r���h�����f�[�^��
                Application.Quit();
            #endif
        }
    }
}
