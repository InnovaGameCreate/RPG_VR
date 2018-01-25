using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayer : HumanBase {
    [SerializeField, TooltipAttribute("体力バー")]
    private Slider slider;      //体力バー

    private void Update()
    {
        //体力バー計算
        float varhp = Status.Parameter.HP / (float)Status.Parameter.MAXHP;
        if (varhp > 1)
        {
            // 最大を超えたら0に戻す
            varhp = 0;
        }
        if (slider != null)
            // HPゲージに値を設定
            slider.value = varhp;
    }

    //攻撃を受けたときの演出
    private void OnCollisionEnter(Collision collision)
    {

    }
}
