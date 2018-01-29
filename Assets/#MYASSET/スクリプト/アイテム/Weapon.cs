using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon :ItemBase
{
    [SerializeField, TooltipAttribute("武器の持ち主（固定）")]
    private HumanBase owner;          //武器の持ち主（固定）
    private AudioSource atkSe;        //攻撃ヒット時効果音
    //剣
    [SerializeField]
    private int endurance;  //耐久
    protected override void Start()
    {
        atkSe = GetComponent<AudioSource>();
        owner = GetComponentInParent<HumanBase>();
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

    //ダメージを受けた際のアニメーションは攻撃を受けた側(つまり人物クラス　　　ダメージ計算を呼ぶのはダメージを与えた側(つまり武器クラスから
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<HumanBase>() != null && collision.gameObject.GetComponent<HumanBase>() != owner)
        {
            if(!atkSe.isPlaying)
            atkSe.Play();
            DamageCalculate dmg = new DamageCalculate(owner.GetComponent<HumanBase>().Status,100,false);//, owner.GetComponent<HumanBase>().SendBuff, owner.GetComponent<HumanBase>().ReceiveBuff);
            collision.gameObject.GetComponent<HumanBase>().ReceiveAttack(dmg);
        }
    }

}
