using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    player player;
    private void Start()
    {
        player = GetComponentInParent<player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.checkGround(true);
            Debug.Log("ground enter");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.checkGround(false);
            Debug.Log("ground exit");

        }
    }
    
}
