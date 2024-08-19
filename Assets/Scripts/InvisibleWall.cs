using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public PlayerWithAnim Player;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(Player.InvisibleWall)
        {
            boxCollider.isTrigger=true;
            Debug.Log("InvisibleWall");
        }
        else
        {
            boxCollider.isTrigger=false;
        }
    }
}
