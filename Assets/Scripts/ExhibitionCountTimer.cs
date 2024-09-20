using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExhibitionCountTimer : MonoBehaviour
{

    //���Z�b�g�p�̃^�C���J�E���g
    private float step_time;

    void Start()
    {
        //���Ԃ̏�����
        step_time = 0.0f;
    }

    void Update()
    {
        //�o�ߎ��Ԃ̃J�E���g
        step_time += Time.deltaTime;

        var horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxisRaw("Vertical");

        //2���ԑ��삵�Ȃ������ꍇ�̓��Z�b�g���������s����
        if (step_time > 180.0f)
        {
            ResetGame();
        }

        //�W�����v�{�^���̓���
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("jump");
            step_time = 0.0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            step_time = 0.0f;
        }

        //���E�ړ��̓���
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

    //���Z�b�g����
    void ResetGame()
    {
        SceneManager.LoadScene("Title2");
    }
}