using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayer : HumanBase
{
    [SerializeField, TooltipAttribute("現在のLV")]
    private int LV;
    [SerializeField, TooltipAttribute("現在の経験値")]
    private int Exp;
    public int EXP
    {
        get { return Exp; }
        set
        {
            Exp = value;
            if (Exp >= ExpSystem(LV) )//レベル上昇処理
            {
                exp_num = Exp - ExpSystem(LV);
                LV++;
                LvText.text = "Lv." + LV;
                Exp = (exp_num / 2);
            }
        }
    }

    [SerializeField, TooltipAttribute("HPバー")]
    private Slider hpSlider;      //体力バー
    [SerializeField, TooltipAttribute("MPバー")]
    private Slider mpSlider;
    [SerializeField, TooltipAttribute("Lvテキスト")]
    private Text LvText;      //体力バー

    private EquipmentItem[] PlayerEquipItem = new EquipmentItem[5];//適当に5個
                                                                   //0:武器,1:防具,*****

    private int exp_num;//一時保存

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

    protected override void Start()
    {
        base.Start();
        LvText.text = "Lv." + LV;
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

    //レベルを入力することで次のレベルまでの経験値量を出力
    private int ExpSystem(int Lv)
    {
        return 500 * (int)Mathf.Pow(1.5f , Lv);
    }
}
