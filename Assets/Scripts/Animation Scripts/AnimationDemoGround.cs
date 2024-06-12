using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDemoGround : MonoBehaviour
{
    AnimationDemo AnimationDemo;
    private void Start()
    {
        AnimationDemo = GetComponentInParent<AnimationDemo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AnimationDemo.checkGround(true);
            Debug.Log("ground enter");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AnimationDemo.checkGround(false);
            Debug.Log("ground exit");

        }
    }
    
}
