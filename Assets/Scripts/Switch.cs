using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool active;

    public DoorScripts door;

    private void OnTriggerEnter2D(Collider2D other)
    {

      
        if (!active && other.CompareTag("Player"))
        //スイッチに触れているとき
        {
            door.isOpen = true;
            enabled = false;
            Debug.Log("Switch enter");
           
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    //スイッチに触れていないとき
    {
        if(!active && other.CompareTag("Player"))
        {
            door.isOpen = false;
            enabled = true;
            Debug.Log("Switch exit");
        }
    }
}
