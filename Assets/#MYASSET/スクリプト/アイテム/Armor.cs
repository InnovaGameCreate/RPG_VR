using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : EquipmentItem {
    //[SerializeField]
    //int hp;     //体力残量
    //[SerializeField]
    //int mp;     //魔力残量
    //[SerializeField]
    //int maxhp;  //最大体力
    //[SerializeField]
    //int maxmp;  //最大魔力
    //[SerializeField]
    //int atk;    //攻撃力
    //[SerializeField]
    //int def;    //防御力
    //[SerializeField]
    //int magicatk;//魔法攻撃力
    //[SerializeField]
    //int magicdef;//魔法防御力
    //[SerializeField]
    //float speed;  //移動速度
    //[SerializeField]
    //float flyspeed;//空中時の移動速度

    public bool TestBool = false;//仮置き1
    public bool TestBool2 = false;//2

    //防具クラス
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (TestBool)
        {
            ItemUse();
            TestBool = false;
            
        }
        if (TestBool2)
        {
            unEquip();
            TestBool2 = false;
        }
	}
    
    public override bool ItemUse()
    {
        GameManager.Instance.PLAYER.PlayerEquipItem[1] = this;//クラス登録
        //GameManager.Instance.PLAYER.EquipBuff[1] = EquipBuff;//バフ登録
        GameManager.Instance.PLAYER.Status.Parameter = GameManager.Instance.PLAYER.Status.Parameter + EquipBuff;
        if(OtherBuff != null)
            GameManager.Instance.PLAYER.AwakeBuff[(int)Buff.BuffType.Equip_Fix] += OtherBuff;
        return true;
    }

    public override bool unEquip()
    {
        //永続バフリストから探索して外す
        GameManager.Instance.PLAYER.PlayerEquipItem[1] = null;
        GameManager.Instance.PLAYER.Status.Parameter = GameManager.Instance.PLAYER.Status.Parameter - EquipBuff;
        if (OtherBuff != null)
            GameManager.Instance.PLAYER.AwakeBuff[(int)Buff.BuffType.Equip_Fix] -= OtherBuff;
        return true;
    }
}
