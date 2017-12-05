using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ItemBase {
    [SerializeField, TooltipAttribute("攻撃力上昇量")]
    public int atkup;
    [SerializeField, TooltipAttribute("防御力上昇量")]
    public int defup;

    //装備を装着した
    protected override bool ItemUse()
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
