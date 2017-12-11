using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantItem : ItemBase
{
    [SerializeField, TooltipAttribute("特定の座標付近で使用するものかどうか")]
    public bool mapposuse;  //特定の座標付近で使用するものかどうか
    [SerializeField, TooltipAttribute("アイテムを使う場所")]
    public Transform targetpos; //アイテムを使う場所
    [SerializeField, TooltipAttribute("アイテム使用可能距離")]
    public int usedistance = 5;
    private Transform playerpos;

    protected override void Start()
    {
        base.Start();
        playerpos = playerstatus.Pos;
        item_type = ItemType.Important;
    }
    private void Update()
    {

    }
    public override bool ItemUse()
    {
        //目的地周辺で使用したか
        if (Vector3.Distance(playerpos.position, targetpos.position) > usedistance)
        {

            return false;
        }

        return true;
    }
}
