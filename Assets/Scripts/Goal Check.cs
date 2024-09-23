using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCheck : MonoBehaviour
{


    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.CompareTag("Goal"))
        {
            Debug.Log("GOAL!!");
            SceneManager.LoadScene("Goal");
        }
    }
}