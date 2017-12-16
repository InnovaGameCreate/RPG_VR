using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour {
    int hp;     //体力
    int maxhp;  //最大体力
    int mp;     //魔力
    int maxmp;  //最大魔力
    int atk;    //攻撃力
    int def;    //防御力
    int magicatk;//魔法攻撃力
    int magicdef;//魔法防御力
    float speed;  //移動速度
    float flyspeed;//空中時の移動速度

    public int HP
    {
        get { return hp; }
    }
    public int MAXHP
    {
        get { return maxhp; }
    }
    public int MP
    {
        get { return mp; }
    }
    public int MAXMP
    {
        get { return maxmp; }
    }
    public int ATK
    {
        get { return atk; }
    }
    public int DEF
    {
        get { return def; }
    }
    public int MAGICATK
    {
        get { return magicatk; }
    }
    public int MAGICDEF
    {
        get { return magicdef; }
    }
    public float SPEED
    {
        get { return speed; }
    }
    public float FLYSPEED
    {
        get { return flyspeed; }
    }

    //TODO 演算子オーバーロード

}
