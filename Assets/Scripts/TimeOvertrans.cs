using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeOvertrans : MonoBehaviour
{
    private string nextSceneName = "Title2";
    private float waitTime = 20.0f;

    void Start()
    {
        Invoke("LoadScene", waitTime);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}