using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ItemBase {
    [SerializeField, TooltipAttribute("パラメータ上昇量")]
    Parameters up;

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.Equipment;
    }

    //装備を装着した
    public override bool ItemUse()
    {
      //永続バフリストに追加
        return true;
    }
    //装備を外した
    public bool unEquip()
    {
        //永続バフリストから探索して外す
        return true;
    }
}
