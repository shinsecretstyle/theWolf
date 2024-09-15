using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : MonoBehaviour
{
    public PlayerWithAnim Player;
    Animator animator;
    AudioSource AS;
    //public AudioClip sleep;
    //public AudioClip speak;
    void Start()
    {
        animator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.isBark)
        {
            animator.SetBool("Bark",true);
            //吠える
            //Debug.Log("Bark");
        }
        else
        {
            animator.SetBool("Bark",false);
        }

        if(Player.isSleep0)
        {
            animator.SetBool("Sleep0",true);
            //寝る（音無）
        }
        else
        {
            animator.SetBool("Sleep0",false);
        }

        if(Player.isSleep)
        {
            animator.SetBool("Sleep",true);
            //寝る(音有)
        }
        else
        {
            animator.SetBool("Sleep",false);
        }
        
    }

    //public void Sleep()
    //{
        //AS.PlayOneShot(sleep);
    //}
    //public void Speak()
    //{
       // AS.PlayOneShot(speak);
    //}
}
