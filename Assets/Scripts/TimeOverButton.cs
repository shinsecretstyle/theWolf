using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeOverButton : MonoBehaviour
{
    public GameObject button;
    private float time = 6.0f;

    void Start()
    {
        button.SetActive(false);
    }

    void Update()
    {
        if (0 < time)
        {
            time -= Time.deltaTime;
        }
        else
        {
            button.SetActive(true);
        }
        Debug.Log(time);
    }
}