using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    [SerializeField, TooltipAttribute("回復量")]
    public int heal = 10;         //回復量
    [SerializeField, TooltipAttribute("使用クールタイム")]
    public float IntervalTime;  //使用クールタイム
    private float usedtime;
    //回復アイテム　
    protected override bool ItemUse()
    {
        if (usedtime + IntervalTime > Time.deltaTime)
            return false;
        usedtime = Time.deltaTime;
        playerstatus.recoveryHp(heal);
        return true;
    }
}
