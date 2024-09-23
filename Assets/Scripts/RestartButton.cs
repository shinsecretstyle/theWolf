using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RestartButton : MonoBehaviour
{
    public GameObject button;
    public GameObject movie2;
    public GameObject movie;
    private float time = 20.0f;

    void Start()
    {
        button.SetActive(false);
        movie2.SetActive(false);
        movie.SetActive(true);
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
            movie2.SetActive(true);
            movie.SetActive(false);
        }
        Debug.Log(time);
    }
}