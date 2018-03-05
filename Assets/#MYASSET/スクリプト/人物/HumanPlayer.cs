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

    public EquipmentItem[] PlayerEquipItem = new EquipmentItem[5];//適当に5個
                                                                   //0:武器,1:防具,*****
    //public Buff[] EquipBuff = new Buff[5];//装備品のバフ格納 上と同じ数にすること


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
        //Debug.Log("me" + Status.Parameter.DEF);
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

    //protected override IEnumerator ApplyReceiveBuff()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < AwakeBuff.Length; i++)//配列用
    //        {
    //            if (AwakeBuff[i] == null)
    //                continue;
    //            //ステータス変更処理
    //            humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[i];
    //            humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[i];

    //            //有効時間を減らす
    //            AwakeBuff[i].AvailableSeconds--;
    //            //Permanence = AwakeBuff[i].PARMANENT;
    //            if (AwakeBuff[i].PARMANENT == false && AwakeBuff[i].AvailableSeconds <= 0)//有効時間が切れた要素の削除
    //                AwakeBuff[i] = null;
    //        }

    //        ////装備バフ用
    //        //for (int i = 0; i < EquipBuff.Length; i++)
    //        //{
    //        //    if (EquipBuff[i] == null)
    //        //        continue;
    //        //    //ステータス変更処理
    //        //    humanstatus.Parameter = humanstatus.Parameter + EquipBuff[i];
    //        //    humanstatus.Parameter = humanstatus.Parameter * EquipBuff[i];
    //        //}
    //        yield return new WaitForSeconds(1.0f);
    //    }
    //}
}
