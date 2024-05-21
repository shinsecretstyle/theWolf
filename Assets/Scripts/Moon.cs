using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Moon : MonoBehaviour
{
    public Transform player;
    //public Light2D lightObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, transform.position - player.position, Mathf.Infinity);
        if (hit.collider.CompareTag("Moon"))
        {
            Debug.Log("Light 2D hits the player!");
        }
    }
}
