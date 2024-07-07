using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    enum ProcessOnAwake
    {
        None,
        LoadScene,
        AddScene,
        UnloadScene
    }

    [SerializeField] ProcessOnAwake processOnAwake;
    [SerializeField] string sceneName;
    [SerializeField] float delay;

    private void Start()
    {
        switch (processOnAwake)
        {
            case ProcessOnAwake.LoadScene: LoadScene(); break;
            case ProcessOnAwake.AddScene: LoadAddScene(); break;
            case ProcessOnAwake.UnloadScene: UnloadScene(); break;
        }
    }

    /// <summary>
    /// �V�[���ړ�
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <param name="delay">�x������</param>
    public void LoadScene(string sceneName, float delay)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        StartCoroutine(DelayedCall(delay, () => operation.allowSceneActivation = true));
    }
    public void LoadScene(string sceneName) => LoadScene(sceneName, delay);
    public void LoadScene(float delay) => LoadScene(sceneName, delay);
    public void LoadScene() => LoadScene(sceneName, delay);

    /// <summary>
    /// �V�[���ǉ�
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <param name="delay">�x������</param>
    public void LoadAddScene(string sceneName, float delay)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        StartCoroutine(DelayedCall(delay, () => operation.allowSceneActivation = true));
    }
    public void LoadAddScene(string sceneName) => LoadAddScene(sceneName, delay);
    public void LoadAddScene(float delay) => LoadAddScene(sceneName, delay);
    public void LoadAddScene() => LoadAddScene(sceneName, delay);

    /// <summary>
    /// �V�[���j��
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <param name="delay">�x������</param>
    public void UnloadScene(string sceneName, float delay)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        StartCoroutine(DelayedCall(delay, () => operation.allowSceneActivation = true));
    }
    public void UnloadScene(string sceneName) => UnloadScene(sceneName, delay);
    public void UnloadScene(float delay) => UnloadScene(sceneName, delay);
    public void UnloadScene() => UnloadScene(sceneName, delay);

    /// <summary>
    /// �x������
    /// </summary>
    /// <param name="second">�x������</param>
    /// <param name="action">�������e</param>
    /// <returns></returns>
    private IEnumerator DelayedCall(float second, Action action)
    {
        yield return new WaitForSeconds(second);
        action.Invoke();
    }
}