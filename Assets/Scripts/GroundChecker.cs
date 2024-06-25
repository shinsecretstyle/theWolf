using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public int phaseId;
    player player;
    
    private void Start()
    {
        player = GetComponentInParent<player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && player.phaseProcess == phaseId)
        {
            player.checkGround(true);
            Debug.Log("ground enter");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && player.phaseProcess == phaseId)
        {
            player.checkGround(false);
            Debug.Log("ground exit");

        }
    }
    
}
