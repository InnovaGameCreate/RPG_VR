using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parameters {
    [SerializeField]
    int hp =100;     //
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

    public int HP
    {
        get { return hp; }
        set {  hp = value > 0 ? maxhp > hp ? value : maxhp : 0; }
    }
    public int MP
    {
        get { return mp; }
    }
    public int MAXHP
    {
        get { return maxhp; }
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
    


    //演算子オーバーロード
    public static Parameters operator +(Parameters a, Parameters b)
    {   
        Parameters result = new Parameters();
        result.hp = a.HP + b.HP;
        result.mp = a.MP + b.MP;
        result.maxhp = a.MAXHP + b.MAXHP;
        result.maxmp = a.MAXMP + b.MAXMP;
        result.atk = a.ATK + b.ATK;
        result.def = a.DEF + b.DEF;
        result.magicatk = a.MAGICDEF + b.MAGICDEF;
        result.speed = a.SPEED + b.speed;
        result.flyspeed = a.FLYSPEED + b.FLYSPEED;
        
        return result ;
    }

    //インスペクター用クラスとの和算
    public static Parameters operator +(Parameters a, DisplayParameters b)
    {
        Parameters result = new Parameters();
        result.hp = a.HP + b.HP;
        result.mp = a.MP + b.MP;
        result.maxhp = a.MAXHP + b.MAXHP;
        result.maxmp = a.MAXMP + b.MAXMP;
        result.atk = a.ATK + b.ATK;
        result.def = a.DEF + b.DEF;
        result.magicatk = a.MAGICDEF + b.MAGICDEF;
        result.speed = a.SPEED + b.SPEED;
        result.flyspeed = a.FLYSPEED + b.FLYSPEED;

        return result;
    }

    public static Parameters operator *(Parameters a, Parameters b)
    {
        Parameters result = new Parameters();
        result.hp = a.HP * b.HP;
        result.mp = a.MP * b.MP;
        result.maxhp = a.MAXHP * b.MAXHP;
        result.maxmp = a.MAXMP * b.MAXMP;
        result.atk = a.ATK * b.ATK;
        result.def = a.DEF * b.DEF;
        result.magicatk = a.MAGICDEF * b.MAGICDEF;
        result.speed = a.SPEED * b.speed;
        result.flyspeed = a.FLYSPEED * b.FLYSPEED;

        return result;
    }
}   
