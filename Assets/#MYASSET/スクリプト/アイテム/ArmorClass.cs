using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorClass : MonoBehaviour {

    [SerializeField]
    private int hp;     //体力残量
    [SerializeField]
    private int mp;     //魔力残量
    [SerializeField]
    private int maxhp;  //最大体力
    [SerializeField]
    private int maxmp;  //最大魔力
    [SerializeField]
    private int atk;    //攻撃力
    [SerializeField]
    private int def;    //防御力
    [SerializeField]
    private int magicatk;//魔法攻撃力
    [SerializeField]
    private int magicdef;//魔法防御力
    [SerializeField]
    private float speed;  //移動速度
    [SerializeField]
    private float flyspeed;//空中時の移動速度

    private Buff Add_Buff;
    private Buff Multi_Buff;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
