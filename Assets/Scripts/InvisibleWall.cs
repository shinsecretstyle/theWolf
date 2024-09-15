using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public PlayerWithAnim Player;
    private BoxCollider2D boxCollider;
    //番犬のフェーズ1,2では通れない道のスクリプト

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(Player.InvisibleWall)
        {
            boxCollider.isTrigger=true;
            //通れるようになる
            //Debug.Log("InvisibleWall");
        }
        else
        {
            boxCollider.isTrigger=false;
            //通れなくなる

        }
    }
}
