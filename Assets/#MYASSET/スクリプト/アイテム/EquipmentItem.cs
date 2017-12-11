﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ItemBase {
    [SerializeField, TooltipAttribute("攻撃力上昇量")]
    public int atkup;
    [SerializeField, TooltipAttribute("防御力上昇量")]
    public int defup;

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.Equipment;
    }

    //装備を装着した
    public override bool ItemUse()
    {
        playerstatus.equipatk = atkup;
        playerstatus.equipdef = defup;
        return true;
    }
    //装備を外した
    public bool unEquip()
    {
        playerstatus.equipatk = 0;
        playerstatus.equipdef = 0;
        return true;
    }
}
