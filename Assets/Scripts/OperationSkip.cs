using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationSkip : MonoBehaviour
{
    private string nextSceneName = "WolfMap2(アニメーションあり)";

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}