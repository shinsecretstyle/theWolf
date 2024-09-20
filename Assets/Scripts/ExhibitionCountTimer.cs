using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExhibitionCountTimer : MonoBehaviour
{

    //リセット用のタイムカウント
    private float step_time;

    void Start()
    {
        //時間の初期化
        step_time = 0.0f;
    }

    void Update()
    {
        //経過時間のカウント
        step_time += Time.deltaTime;

        var horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxisRaw("Vertical");

        //2分間操作しなかった場合はリセット処理を実行する
        if (step_time > 180.0f)
        {
            ResetGame();
        }

        //ジャンプボタンの入力
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("jump");
            step_time = 0.0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            step_time = 0.0f;
        }

        //左右移動の入力
        if (horizontal < 0 | horizontal > 0)
        {
            //Debug.Log("horizontal");
            step_time = 0.0f;
        }

        if (Vertical < 0 | Vertical > 0)
        {
            //Debug.Log("Vertical");
            step_time = 0.0f;
        }
    }

    //リセット処理
    void ResetGame()
    {
        SceneManager.LoadScene("Title2");
    }
}