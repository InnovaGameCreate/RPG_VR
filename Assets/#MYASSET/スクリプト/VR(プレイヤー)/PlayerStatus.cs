using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーに対する影響はすべてこのクラスで関数化して利用すること
//シーン上にプレイヤーステータスコントローラ専用のオブジェクトを配置する
public class PlayerStatus : MonoBehaviour
{
    //一時的ステータス向上時に対応させるため base....の変数を用意
    [SerializeField]
    private Slider slider;      //体力バー
    private int nowhp;     //現状体力
    private int nowmaxhp;     //現状最大体力
    private int basemaxhp = 100;    //最大体力      
    private bool hpuped;   //HP上昇系がついてるかどうか


    private int nowatk;     //現状攻撃力
    public int Atk
    {
        get { return nowatk; }
        private set { nowatk = value; }
    }
    private int baseatk = 50;  //基礎攻撃力
    [System.NonSerialized]
    public int equipatk;     //装備攻撃力
    private bool atkuped;   //攻撃力上昇系がついてるかどうか


    private int nowdef;     //現状防御力
    public int Def
    {
        get { return nowdef; }
        private set { nowdef = value; }
    }
    private int basedef = 5;  //基礎防御力
    [System.NonSerialized]
    public int equipdef;     //装備防御力
    private bool defuped;   //攻撃力上昇系がついてるかどうか

    private int nowspeed;     //現状移動速度
    public int Speed
    {
        get { return nowspeed; }
        private set { nowspeed = value; }
    }
    private int basespeed = 10;  //基礎移動速度
    private bool speeduped;   //移動速度上昇系がついてるかどうか


    private int lv = 1; //レベル
    private int exp;    //現在の経験値
    private int expmax = 100;        //経験値最大量

    [SerializeField, TooltipAttribute("プレイヤー座標")]
    private Transform playerpos;     //プレイヤー座標 eyeを選択
    public Transform Pos
    {
        get { return playerpos; }
        private set { playerpos = value; }
    }

    // Use this for initialization
    void Start()
    {
        nowhp = nowmaxhp = basemaxhp;
        nowatk = baseatk+equipatk;
        nowdef = basedef+equipdef;
        nowspeed = basespeed;
    
    }
    //private void Awake()
    //{
    //    Pos = GameObject.Find("[VRTK_SDKManager]/SteamVR/[CameraRig]/Camera (head)/Camera (eye)").transform;
   
    //}
    private void Update()
    {
        float varhp = nowhp / (float)nowmaxhp;
        if (varhp > 1)
        {
            // 最大を超えたら0に戻す
            varhp = 0;
        }
        if (slider != null)
            // HPゲージに値を設定
            slider.value = varhp;
    }

    //体力回復   trueなら回復効果を発揮しない(アイテム数を減らさないなど
    public bool recoveryHp(int hp)
    {
        if (this.nowhp == this.basemaxhp)
            return true;

        this.nowhp += hp;
        if (this.nowhp > basemaxhp)
            this.nowhp = basemaxhp;
        return false;
    }

    //アイテムなどによる一時的体力最大値上昇
    public void maxHpUp(int plushp, int time)
    {
        if (hpuped)
            return;
        else
        {
            hpuped = true;
            StartCoroutine(setMaxHpUp(plushp, time));

        }
    }

    //指定時間体力最大値上昇
    private IEnumerator setMaxHpUp(int plushp, int time)
    {
        nowmaxhp = basemaxhp + plushp;
        yield return new WaitForSeconds(time);

        hpuped = false;
        nowmaxhp = basemaxhp;
    }



    //アイテムなどによる一時的攻撃力上昇
    public void atkUp(int plusatk, int time)
    {
        if (atkuped)
            return;
        else
        {
            atkuped = true;
            StartCoroutine(setAtkUp(plusatk, time));

        }
    }
    //指定時間攻撃力上昇
    private IEnumerator setAtkUp(int plusatk, int time)
    {
        nowatk = baseatk + plusatk + equipatk;
        yield return new WaitForSeconds(time);

        atkuped = false;
        nowatk = baseatk;
    }


    //アイテムなどによる一時的防御力上昇
    public void defUp(int plusdef, int time)
    {
        if (defuped)
            return;
        else
        {
            defuped = true;
            StartCoroutine(setDefUp(plusdef, time));

        }
    }
    //指定時間防御力上昇
    private IEnumerator setDefUp(int plusdef, int time)
    {
        nowdef = basedef + plusdef + equipdef;
        yield return new WaitForSeconds(time);

        defuped = false;
        nowdef = baseatk;
    }

    //アイテムなどによる一時的移動速度上昇
    public void speedUp(int plusspeed, int time)
    {
        if (speeduped)
            return;
        else
        {
            speeduped = true;
            StartCoroutine(setSpeedUp(plusspeed, time));

        }
    }
    //指定時間移動速度上昇
    private IEnumerator setSpeedUp(int plusspeed, int time)
    {
        nowspeed = basespeed + plusspeed;
        yield return new WaitForSeconds(time);

        speeduped = false;
        nowspeed = baseatk;
    }

    //経験値を与える
    public void getExp(int exp)
    {
        this.exp += exp;
        if (this.exp > expmax)
        {
            lv++;
            this.exp = 0;
            expUpBonus();
            expmax = lv*lv * 5 + expmax;
        }
    }

    //レベルアップに伴うステータス向上
    private void expUpBonus()
    {
       if (lv < 5)
        {
            basemaxhp += 10;
            baseatk += 10;
            basedef += 10;
        }
        else
        {
            basemaxhp += 5;
            baseatk += 5;
            basedef += 5;
        }

    }

//ダメージを受ける
public void damaged(int atk)
{
   int deffend = atk - nowdef;
        if (deffend <= 0)
            deffend = 1;
    nowhp -= deffend;

    if (nowhp < 0)
        ;//ゲームオーバーシーンへ
}
}
