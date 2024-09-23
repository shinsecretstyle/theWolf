﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EDSkip : MonoBehaviour
{
    private string nextSceneName = "Title2";

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}