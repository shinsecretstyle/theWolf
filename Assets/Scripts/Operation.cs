using UnityEngine;
using UnityEngine.SceneManagement;

public class Operation : MonoBehaviour
{
    private string nextSceneName = "WolfMap2(�A�j���[�V��������)";
    private float waitTime = 5.0f;

    void Start()
    {
        Invoke("LoadScene", waitTime);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
