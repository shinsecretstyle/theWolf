using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool active;
    Animator animator;
    public DoorScripts door;
    public bool isOn = false;
    public bool isOff = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!active&&other.CompareTag("Player"))
        //スイッチに触れているとき
        {
            door.isOpen = true;
            animator.SetBool("isOn",true);
            animator.SetBool("isOff",false);
            enabled = true;
            Debug.Log("Switch enter");

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    //スイッチに触れていないとき
    {
        if(!active&&other.CompareTag("Player"))
        {
            door.isOpen = false;
            animator.SetBool("isOn",false);
            animator.SetBool("isOff",true);
            enabled = false;
            Debug.Log("Switch exit");
        }
    }
}
