using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Dognear = false;
    public bool Dogfar = true;
    void Start()
    {
        GameObject obj1 = GameObject.Find("Player");
        GameObject obj2 = GameObject.Find("Enemy Dog");
        //float distance = Vector3.Distance(obj1.transform.position,obj2.transform.position);
        //Debug.Log("Distance between Object1 and Object2:" + distance);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj1 = GameObject.Find("Player");
        GameObject obj2 = GameObject.Find("Enemy Dog");
        float distance = Vector3.Distance(obj1.transform.position,obj2.transform.position);
        Debug.Log("Distance between Object1 and Object2:" + distance);

        if(distance<10)
        {
            Debug.Log("10以下");
            Dognear = true;
            Dogfar = false;
        }
        else
        {
            Debug.Log("10以上");
            Dogfar = true;
            Dognear = false;
        }
    }
}
