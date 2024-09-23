using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueTransition : MonoBehaviour
{
    private string nextSceneName = "Operation instructions";
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
