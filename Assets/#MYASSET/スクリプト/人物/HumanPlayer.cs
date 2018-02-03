using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayer : HumanBase
{
    [SerializeField, TooltipAttribute("HPバー")]
    private Slider hpSlider;      //体力バー
    [SerializeField, TooltipAttribute("MPバー")]
    private Slider mpSlider;

    void calculateVar(Slider target, int now, float max)
    {
        //体力バー計算
        float var = now / max;
        if (var > 1)
        {
            // 最大を超えたら0に戻す
            var = 0;
        }
        if (hpSlider != null)
            // HPゲージに値を設定
            target.value = var;
    }
    private void Update()
    {
        calculateVar(hpSlider, Status.Parameter.HP, (float)Status.Parameter.MAXHP);
        calculateVar(mpSlider, Status.Parameter.MP, (float)Status.Parameter.MAXMP);
    }

    //攻撃を受けたときの演出
    private void OnCollisionEnter(Collision collision)
    {

    }
}
