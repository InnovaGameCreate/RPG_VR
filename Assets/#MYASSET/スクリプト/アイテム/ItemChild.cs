using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChild : ItemBase
{

    //各々のenumのサブクラスを利用すること　　没スクリプト
    /*
  *  アイテムサブクラス
  *  実装時にはこちらをアタッチ(ひな形)
  */
    public enum ItemType
    {
        Healing,    //回復アイテム
        StatusUp,    //ステータスアップアイテム
        Equipment,  //装備アイテム
        Important   //鍵等の進行系アイテム
    };
    [SerializeField, TooltipAttribute("Healing : 回復アイテム\nStatusUp : ステータスアップアイテム\nEquipment : 装備アイテム\nImportant : 鍵等の進行系アイテム")]
    public ItemType Kind;


    [SerializeField, TooltipAttribute("使用クールタイム")]
    public float IntervalTime;  //使用クールタイム
    [SerializeField, TooltipAttribute("腰に装備可能かどうか")]
    public bool CanSetWaist;    //腰に装備可能かどうか
    [SerializeField, TooltipAttribute("捨てれるかどうか")]
    public bool CanThrowAway;   //捨てれるかどうか

    //回復アイテム　ステータスアップ
    public override bool ItemUse()
    {
        return true;
    }
}
