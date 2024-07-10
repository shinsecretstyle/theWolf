using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueTransition : MonoBehaviour
{
    private string nextSceneName = "WolfMap2(アニメーションあり)";
    private float waitTime = 31.0f;

    void Start()
    {
        Invoke("LoadScene", waitTime);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
