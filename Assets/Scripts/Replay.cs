using UnityEngine;
using UnityEngine.SceneManagement;

//文字化け直す必要

public class Replay : MonoBehaviour
{
    public void replay()
    {
            // LoadScene�̈����ɃV�[���̖��O�����ēǂݍ���
            SceneManager.LoadScene("WolfMap2(アニメーションあり)");
    }
            
}