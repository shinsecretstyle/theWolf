using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public bool isTouch = false;
    public PlayerWithAnim Player;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&Player.isPush)
        {
            isTouch = true;
        }
        else
        {
            //isTouch = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouch = false;
    }
}
