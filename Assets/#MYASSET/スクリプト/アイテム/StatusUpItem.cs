using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUpItem : ItemBase{

    [SerializeField, TooltipAttribute("効果量")]
    public int effect = 10;         //効果量
    [SerializeField, TooltipAttribute("効果時間")]
    public int effecttime = 10;         //効果時間
    public enum StatusUp
    {
        HP,             //体力影響
        ATK,            //攻撃力影響
        DEF,            //防御影響
        SPEED           //スピード影響
    };
    [SerializeField, TooltipAttribute("ステータス影響内容")]
    public StatusUp content;

    [SerializeField, TooltipAttribute("使用クールタイム")]
    public float IntervalTime;  //使用クールタイム
    private float usedtime;

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.StatusUp;
    }

    //ステータスアップ
    public override bool ItemUse()
    {
        if (usedtime + IntervalTime > Time.deltaTime)
            return false;
            usedtime = Time.deltaTime;
        switch (content)
        {
            case StatusUp.HP:
                playerstatus.maxHpUp(effect, effecttime);
                break;
            case StatusUp.ATK:
                playerstatus.atkUp(effect,effecttime);
                break;
         case StatusUp.DEF:
                playerstatus.defUp(effect, effecttime);
                break;
            case StatusUp.SPEED:
                playerstatus.speedUp(effect, effecttime);
                break;
        }
        return true;
    }
}
