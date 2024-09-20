using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    [Header("FadeManager")] public FadeManager fade;

    public int countdownMinutes = 3;
    private float countdownSeconds;
    private Text timeText;

    private void Start()
    {
        timeText = GetComponent<Text>();
        countdownSeconds = countdownMinutes * 60;
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0,0,(int)countdownSeconds);
        timeText.text =span.ToString(@"mm\:ss");

        if(countdownSeconds <= 0)
        {
            fade.StartFadeIn();
            SceneManager.LoadScene("Timeover");
        }
    }
    

}
