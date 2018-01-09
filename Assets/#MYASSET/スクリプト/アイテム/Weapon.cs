using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon :ItemBase
{   //剣
    [SerializeField]
    private int endurance;  //耐久
    protected override void Start()
    {
        ItemUse();
   //     Debug.Log(parent.SendBuff);
    }
    public int Endurance
    {
        get { return endurance; }
        set { endurance = value > 0 ? value : 0; }
    }
    public override bool ItemUse()
    {

        return true;
    }
        

}
