using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BacktoTitle : MonoBehaviour
{
    public void OnClick()
    {
        // ボタンが押されたとき
        Time.timeScale =1;
        SceneManager.LoadScene("Title2");
    }
}