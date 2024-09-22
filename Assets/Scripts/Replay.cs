using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void replay()
    {
            // LoadSceneの引数にシーンの名前を入れて読み込む
            SceneManager.LoadScene("WolfMap2(アニメーションあり)");
    }
            
}