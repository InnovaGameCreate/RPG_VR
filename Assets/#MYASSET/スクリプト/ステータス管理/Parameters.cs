using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parameters {
    [SerializeField]
    int hp;     //
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
        set {  hp = value > 0 ? (maxhp >= hp ? value : maxhp ): 0; }
        //{
        //    Debug.Log("moriyama"+hp);
        //    hp = value;
        //}
    }
    public int MP
    {
        get { return mp; }
        set { mp = value > 0 ? (maxmp >= mp ? value : maxmp) : 0; }
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
        set {
            atk = value;
            if (atk <= 0)
                atk = 1;
        }
    }
    public int DEF
    {
        get { return def; }
        set {
            def = value;
            if (def < 0)
                def = 0;
        }
    }
    public int MAGICATK
    {
        get { return magicatk; }
        set {
            magicatk = value;
            if (magicatk <= 0)
                magicatk = 1;
        }
    }
    public int MAGICDEF
    {
        get { return magicdef; }
        set {
            magicdef = value;
            if (magicdef < 0)
                magicdef = 0;
        }
    }
    public float SPEED
    {
        get { return speed; }
        set { speed = value; }
    }
    public float FLYSPEED
    {
        get { return flyspeed; }
        set { flyspeed = value; }
    }
    


    //演算子オーバーロード
    public static Parameters operator +(Parameters a, Parameters b)
    {   
        Parameters result = new Parameters();
        result.HP = a.HP + b.HP;
        result.MP = a.MP + b.MP;
        result.maxhp = a.MAXHP + b.MAXHP;//これいる？
        result.maxmp = a.MAXMP + b.MAXMP;
        result.ATK = a.ATK + b.ATK;
        result.DEF = a.DEF + b.DEF;
        result.MAGICATK = a.MAGICATK + b.MAGICATK;
        result.MAGICDEF = a.MAGICDEF + b.MAGICDEF;
        result.speed = a.SPEED + b.speed;
        result.flyspeed = a.FLYSPEED + b.FLYSPEED;
        
        return result ;
    }

    //インスペクター用クラスとの和算
    public static Parameters operator +(Parameters a, DisplayParameters b)
    {
        Parameters result = new Parameters();
        result.HP = a.HP + b.HP;
        result.MP = a.MP + b.MP;
        result.maxhp = a.MAXHP + b.MAXHP;
        result.maxmp = a.MAXMP + b.MAXMP;
        result.ATK = a.ATK + b.ATK;
        result.DEF = a.DEF + b.DEF;
        result.MAGICATK = a.MAGICATK + b.MAGICATK;
        result.MAGICDEF = a.MAGICDEF + b.MAGICDEF;
        result.speed = a.SPEED + b.SPEED;
        result.flyspeed = a.FLYSPEED + b.FLYSPEED;

        return result;
    }

    public static Parameters operator *(Parameters a, Parameters b)
    {
        Parameters result = new Parameters();
        result.HP = a.HP * b.HP;
        result.MP = a.MP * b.MP;
        result.maxhp = a.MAXHP * b.MAXHP;
        result.maxmp = a.MAXMP * b.MAXMP;
        result.ATK = a.ATK * b.ATK;
        result.DEF = a.DEF * b.DEF;
        result.MAGICATK = a.MAGICATK * b.MAGICATK;
        result.MAGICDEF = a.MAGICDEF * b.MAGICDEF;
        result.speed = a.SPEED * b.speed;
        result.flyspeed = a.FLYSPEED * b.FLYSPEED;

        return result;
    }

    //バフとの計算
    public static Parameters operator +(Parameters a, Buff b)
    {
        Parameters re = new Parameters();
        re.HP = a.HP + b.HP;
        re.MP = a.MP + b.MP;
        re.maxhp = a.MAXHP + b.MAXHP;
        re.maxmp = a.MAXMP + b.MAXMP;
        re.ATK = a.ATK + b.ATK;
        re.DEF = a.DEF + b.DEF;
        re.MAGICATK = a.MAGICATK + b.MAGICATK;
        re.MAGICDEF = a.MAGICDEF + b.MAGICDEF;
        re.speed = a.SPEED + b.SPEED;
        re.flyspeed = a.FLYSPEED + b.FLYSPEED;
        return re;
    }

    public static Parameters operator -(Parameters a, Buff b)
    {
        Parameters re = new Parameters();
        re.HP = a.HP - b.HP;
        re.MP = a.MP - b.MP;
        re.maxhp = a.MAXHP - b.MAXHP;
        re.maxmp = a.MAXMP - b.MAXMP;
        re.ATK = a.ATK - b.ATK;
        re.DEF = a.DEF - b.DEF;
        re.MAGICATK = a.MAGICATK - b.MAGICATK;
        re.MAGICDEF = a.MAGICDEF - b.MAGICDEF;
        re.speed = a.SPEED - b.SPEED;
        re.flyspeed = a.FLYSPEED - b.FLYSPEED;
        return re;
    }

    public static Parameters operator *(Parameters a, Buff b)
    {
        Parameters re = new Parameters();
        re.HP = a.HP * b.MHP;
        re.MP = a.MP * b.MMP;
        re.maxhp = a.MAXHP * b.MMAXHP;
        re.maxmp = a.MAXMP * b.MMAXMP;
        re.ATK = a.ATK * b.MATK;
        re.DEF = a.DEF * b.MDEF;
        re.MAGICATK = a.MAGICATK * b.MMAGICATK;
        re.MAGICDEF = a.MAGICDEF * b.MMAGICDEF;
        re.speed = a.SPEED * b.MSPEED;
        re.flyspeed = a.FLYSPEED * b.MFLYSPEED;
        return re;
    }
}   
