using UnityEngine;
using UnityEngine.UI;

public class StageSlider : MonoBehaviour
{
    public Slider stageSlider;
    public Transform Player;//プレイヤー
    public Transform Goal;//ゴール
    private float maxDistance;//プレイヤーとゴールの最大距離
 


    void Start()
    {
        maxDistance = Vector3.Distance(Player.position,Goal.position);
        //スタート時のプレイヤーとゴールの距離を算出しmaxDistanceに入れる。
        stageSlider.maxValue = maxDistance;
        //maxValueに入れ込む
    }

    void Update()
    {
        float Distance = Vector3.Distance(Player.position,Goal.position);
        //プレイヤーとゴールの距離を取得
        stageSlider.value = maxDistance - Distance;
        //スライダーの動き。最大距離－プレイヤーとゴールの距離
    }
}


