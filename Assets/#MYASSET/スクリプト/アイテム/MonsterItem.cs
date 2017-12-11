using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItem : ItemBase
{
    //モンスターからドロップされる素材

    protected override void Start()
    {
        base.Start();
        item_type = ItemType.Monster;
    }

    //使えない
    public override bool ItemUse()
    {
        return true;
    }
}
