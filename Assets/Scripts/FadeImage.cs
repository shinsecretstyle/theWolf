using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [Header("�ŏ�����t�F�[�h�C�����������Ă��邩�ǂ���")] public bool firstFadeInComp;
    private Image img = null;
    private int frameCount = 0;
    private float timer = 0.0f;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool compFadeIn = false;
    private bool compFadeOut = false;

    /// <summary>
    /// �t�F�[�h�C�����J�n����
    /// </summary>
    /// <returns><</returns>

    public void StartFadeIn()
    {
        Time.timeScale = 1;
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeIn = true;
        compFadeIn = false;
        timer = 0.0f;
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
    }

    /// <summary>
    /// �t�F�[�h�C���������������ǂ���
    /// </summary>
    /// <returns><</returns>

    public bool IsFadeInComplete()
    {
        return compFadeIn;
    }

    /// <summary>
    /// �t�F�[�h�A�E�g���J�n����
    /// </summary>
    /// <returns><</returns>

    public void StartFadeOut()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 0;
        img.raycastTarget = true;
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�������������ǂ���
    /// </summary>
    /// <returns><</returns>

    public bool IsFadeOutComplete()
    {
        return compFadeOut;
    }


    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        if (firstFadeInComp)
        {
            FadeInComplete();
        }
        else
        {
            StartFadeIn();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount > 10)
        {
            if (fadeIn)
            {
                FadeInUpdate();
            }
            else if (fadeOut)
            {
                FadeOutUpdate();
            }
        }
        ++frameCount;
    }

    //�t�F�[�h�C����
    private void FadeInUpdate()
    {
        if (timer < 4f)
        {
            img.color = new Color(1, 1, 1, 1 - timer);
            img.fillAmount = 6 - timer;
        }
        else
        {
            FadeInComplete();
        }
        timer += Time.deltaTime;
    }

    //�t�F�[�h�A�E�g��
    private void FadeOutUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, timer);
            img.fillAmount = timer;
        }
        else
        {
            FadeOutComplete();
        }
        timer += Time.deltaTime;
    }

    //�t�F�[�h�C������
    private void FadeInComplete()
    {
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 0;
        img.raycastTarget = false;
        timer = 0.0f;
        fadeIn = false;
        compFadeIn = true;
    }

    //�t�F�[�h�A�E�g����
    private void FadeOutComplete()
    {
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = false;
        timer = 0.0f;
        fadeOut = false;
        compFadeOut = true;
    }
}