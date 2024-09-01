using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceArea : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerWithAnim Player;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.isPoliceArea)
        {
            animator.SetBool("Police Area",true);
            //Debug.Log("Bark");
        }
        else
        {
            animator.SetBool("Police Area",false);
        }
    }
}
