using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEscape : MonoBehaviour
{

    private bool firstPush = false;

    //�Q�[���I��
    public void EndGame()
    {
        if (!firstPush)
        {
            Debug.Log("END!");
            firstPush = true;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }

    }
}
