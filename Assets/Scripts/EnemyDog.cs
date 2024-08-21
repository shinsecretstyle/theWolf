using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : MonoBehaviour
{
    public PlayerWithAnim Player;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.isBark)
        {
            animator.SetBool("Bark",true);
            //Debug.Log("Bark");
        }
        else
        {
            animator.SetBool("Bark",false);
        }
        
    }
}
