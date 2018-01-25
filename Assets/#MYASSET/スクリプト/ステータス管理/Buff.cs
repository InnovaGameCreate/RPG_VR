using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    ////プラス
    //Parameters paraSingle;
    //public Parameters ParaSingle
    //{
    //    get { return paraSingle; }
    //}
    ////倍率
    //Parameters paraMagnification;
    //public Parameters ParaMagnification
    //{
    //    get { return paraMagnification; }
    //}

    //ADD
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
        //set { hp = value > 0 ? maxhp > hp ? value : maxhp : 0; }
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

    //MULTI
    [SerializeField]
    int Mhp;     //
    [SerializeField]
    int Mmp;     //魔力残量
    [SerializeField]
    int Mmaxhp;  //最大体力
    [SerializeField]
    int Mmaxmp;  //最大魔力
    [SerializeField]
    int Matk;    //攻撃力
    [SerializeField]
    int Mdef;    //防御力
    [SerializeField]
    int Mmagicatk;//魔法攻撃力
    [SerializeField]
    int Mmagicdef;//魔法防御力
    [SerializeField]
    float Mspeed;  //移動速度
    [SerializeField]
    float Mflyspeed;//空中時の移動速度

    public int MHP
    {
        get { return Mhp; }
        //set { hp = value > 0 ? maxhp > hp ? value : maxhp : 0; }
    }
    public int MMP
    {
        get { return Mmp; }
    }
    public int MMAXHP
    {
        get { return Mmaxhp; }
    }
    public int MMAXMP
    {
        get { return Mmaxmp; }
    }
    public int MATK
    {
        get { return Matk; }
    }
    public int MDEF
    {
        get { return Mdef; }
    }
    public int MMAGICATK
    {
        get { return Mmagicatk; }
    }
    public int MMAGICDEF
    {
        get { return Mmagicdef; }
    }
    public float MSPEED
    {
        get { return Mspeed; }
    }
    public float MFLYSPEED
    {
        get { return Mflyspeed; }
    }

    //有効時間
    int availableSeconds;
    public int AvailableSeconds
    {
        get { return availableSeconds; }
        set { availableSeconds = value; }
    }

    //パラメータ
    //[SerializeField]
    //private DisplayParameters add;
    //[SerializeField]
    //private DisplayParameters multi;


    // Use this for initialization
    void Start()
    {
        //受バフのみ受け付ける
        //parameterクラスはインスペクター内で上にあるのがプラス
        //Parameters[] inspector_parameters = GetComponents<Parameters>();
        //paraSingle = inspector_parameters[0];
        //paraMagnification = inspector_parameters[1];

        //paraSingle = new Parameters();
        //paraMagnification = new Parameters();
        //paraSingle = paraSingle + add;
        //paraMagnification = paraMagnification + multi;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Buff operator +(Buff a, Buff b)
    {
        Buff re = new Buff();
        if (!b)
            Debug.Log("b");
        //re.paraSingle = a.paraSingle + b.paraSingle;
        //re.paraMagnification = a.paraMagnification * b.paraMagnification;
        re.hp = a.HP + b.HP;
        re.mp = a.MP + b.MP;
        re.maxhp = a.MAXHP + b.MAXHP;
        re.maxmp = a.MAXMP + b.MAXMP;
        re.atk = a.ATK + b.ATK;
        re.def = a.DEF + b.DEF;
        re.magicatk = a.MAGICDEF + b.MAGICDEF;
        re.speed = a.SPEED + b.SPEED;
        re.flyspeed = a.FLYSPEED + b.FLYSPEED;
        return re;
    }

    


}
