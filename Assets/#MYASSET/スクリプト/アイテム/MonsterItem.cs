using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItem : ItemBase
{
    //モンスターからドロップされる素材

    //使えない
    protected override bool ItemUse()
    {
        return true;
    }
}
