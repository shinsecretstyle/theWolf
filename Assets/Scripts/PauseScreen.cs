using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private bool PauseMenu;
    public GameObject MenuScreen;
    public GameObject Moon;
    public GameObject UI;
    
    void Start()
    {
        MenuScreen.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            PauseMenu = !PauseMenu;
        }

        if(!PauseMenu)
        {
            Time.timeScale = 1;
            MenuScreen.SetActive(false);
            Moon.SetActive(true);
            UI.SetActive(true);

        }
        else if(PauseMenu)
        {
            Time.timeScale =0;
            MenuScreen.SetActive(true);
            Moon.SetActive(false);
            UI.SetActive(false);
        }
    }
}
