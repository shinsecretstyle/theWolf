using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScripts : MonoBehaviour
{
    float defaultY;
    float openY = 5f;
    //ドアが開く範囲
    float speed = 1f;
    //ドアが開くスピード
    public bool isOpen;

    void Start()
    {
        defaultY = transform.position.y;
    }

    void Update()
    {
        if(isOpen && transform.position.y < openY)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if(!isOpen && transform.position.y > defaultY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }
}
