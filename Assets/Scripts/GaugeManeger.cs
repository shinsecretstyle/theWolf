using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManeger : MonoBehaviour
{
    public GameObject player;
    public Sprite p1;
    public Sprite p2;
    public Sprite p3;

    Image nowImage;
    PlayerWithAnim playerStatus;
    void Start()
    {
        playerStatus = player.GetComponent<PlayerWithAnim>();
        nowImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatus.phaseID == 1)
        {
            nowImage.sprite = p1;
        }else if(playerStatus.phaseID == 2) 
        { 
            nowImage.sprite = p2; 
        }else if(playerStatus.phaseID == 3) 
        { 
            nowImage.sprite = p3; 
        }
    }
}
