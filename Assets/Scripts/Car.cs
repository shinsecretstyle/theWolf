using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public bool isTouch = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float move = Input.GetAxis("Horizontal");

        if(move > 0 && isTouch)
        {
            animator.SetFloat("Speed", move);
            Debug.Log("Car");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouch = true;
        }
        else
        {

            isTouch = false;
        }
    }
}
