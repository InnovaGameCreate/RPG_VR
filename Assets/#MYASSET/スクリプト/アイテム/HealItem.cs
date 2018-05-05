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

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.Healing;
    }

    //回復アイテム　
    public override bool ItemUse()
    {
        if (usedtime + IntervalTime > Time.deltaTime)
            return false;
        usedtime = Time.deltaTime;
        Debug.Log("アイテム使用");
        playerstatus.recoveryHp(heal);
        return true;
    }
}
