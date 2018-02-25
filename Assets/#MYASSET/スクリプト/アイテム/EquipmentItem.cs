using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ItemBase {
    //[SerializeField, TooltipAttribute("パラメータ上昇量")]
    //Parameters up;

    protected Buff EquipBuff = new Buff();

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.Equipment;
        EquipBuff = GetComponent<Buff>();//オブジェクトについてるバフを取得
    }

    //装備を装着した
    public override bool ItemUse()
    {
        //永続バフリストに追加
        //GameManager.Instance.PLAYER.PlayerEquipItem
        return true;
    }
    //装備を外した
    public virtual bool unEquip()
    {
        //永続バフリストから探索して外す

        return true;
    }
}
