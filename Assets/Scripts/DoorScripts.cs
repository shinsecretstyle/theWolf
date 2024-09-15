using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScripts : MonoBehaviour
{
    float defaultY;
    float openY = 5.5f;
    //ドアが開く範囲
    float speed = 1.6f;
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
            //開く
        }
        else if(!isOpen && transform.position.y > defaultY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            //閉じる
        }
    }
}
