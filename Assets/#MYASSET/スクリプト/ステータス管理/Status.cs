﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status{

    private int Lv
    {
        get{ return Lv; }
    }


    Parameters baseparameters = new Parameters();
    public Parameters Parameter
    {
        get { return baseparameters; }
    }
    Buff GivingBuff;
    Buff ReceiveBuff;

    float WeaponProgress;//武器熟練度
    float MageicProgress;//魔法熟練度

    float NextExperiencePoint;//次のレベルまでの経験値量
    float Experience;//現在経験値

}
