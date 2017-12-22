using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : EquipmentItem {
    [SerializeField]
    int hp;     //体力残量
    [SerializeField]
    int mp;     //魔力残量
    [SerializeField]
    int maxhp;  //最大体力
    [SerializeField]
    int maxmp;  //最大魔力
    [SerializeField]
    int atk;    //攻撃力
    [SerializeField]
    int def;    //防御力
    [SerializeField]
    int magicatk;//魔法攻撃力
    [SerializeField]
    int magicdef;//魔法防御力
    [SerializeField]
    float speed;  //移動速度
    [SerializeField]
    float flyspeed;//空中時の移動速度

          //防具クラス
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
