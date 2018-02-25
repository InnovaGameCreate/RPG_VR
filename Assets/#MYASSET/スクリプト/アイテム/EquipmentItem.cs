using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Buff))]
public class EquipmentItem : ItemBase {
    //[SerializeField, TooltipAttribute("パラメータ上昇量")]
    //Parameters up;

    [SerializeField]
    protected Buff EquipBuff;
    [SerializeField]
    protected Buff OtherBuff;

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.Equipment;
        //EquipBuff = gameObject.GetComponent<Buff>();//オブジェクトについてるバフを取得
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
