using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーに対する影響はすべてこのクラスで関数化して利用すること
//シーン上にプレイヤーステータスコントローラ専用のオブジェクトを配置する
public class PlayerStatus : MonoBehaviour {
    //一時的ステータス向上時に対応させるため base....の変数を用意
    [SerializeField]
    private Slider slider;      //体力バー
    private int hp;     //現状体力

    private int nowmaxhp;     //現状最大体力
    private int basemaxhp = 100;    //最大体力      
    private bool hpuped;   //HP上昇系がついてるかどうか

    private int nowatk=50;
    //現状攻撃力
    public int Atk {
        get {return nowatk;}
        private set { nowatk = value; }
    }   
    private int baseatk =  10;  //基礎攻撃力
    private bool atkuped;   //攻撃力上昇系がついてるかどうか

    private int lv = 1; //レベル
    private int exp;    //現在の経験値
    private const int expmax = 1000;        //経験値最大量



    // Use this for initialization
    void Start () {
        hp = nowmaxhp = basemaxhp;
	}

    private void Update()
    {
        float varhp = hp / (float)nowmaxhp;
        if (varhp > 1)
        {
            // 最大を超えたら0に戻す
            varhp = 0;
        }
        if(slider!=null)
        // HPゲージに値を設定
        slider.value = varhp;
    }
    //アイテムなどによる一時的攻撃力上昇
    public void atkUp(int plusatk, int time)
    {
        if (atkuped)
            return;
        else {
            atkuped = true;
            StartCoroutine(setAtkUp(plusatk,time));

            }
    }
    //指定時間攻撃力上昇
    private IEnumerator setAtkUp(int plusatk,int time)
    {
        nowatk = baseatk + plusatk;
        yield return new WaitForSeconds(time);

        atkuped = false;
        nowatk = baseatk;
    }

    //アイテムなどによる一時的体力最大値上昇
    public void hpUp(int plushp, int time)
    {
        if (hpuped)
            return;
        else
        {
            hpuped = true;
            StartCoroutine(setHpUp(plushp, time));

        }
    }
    //指定時間体力最大値上昇
    private IEnumerator setHpUp(int plushp, int time)
    {
        nowmaxhp = basemaxhp + plushp;
        yield return new WaitForSeconds(time);

        hpuped = false;
        nowmaxhp = basemaxhp;
    }

    //体力回復   trueなら回復効果を発揮しない(アイテム数を減らさないなど
    public bool recoveryHp(int hp)
    {
        if(this.hp == this.basemaxhp)
        return true;

        this.hp += hp;
        if (this.hp > basemaxhp)
            this.hp = basemaxhp;
        return false;
    }

    //経験値を与える
    public void getExp(int exp)
    {
        this.exp += exp;
        if (this.exp > expmax)
        {
            lv++;
            this.exp = 0;
        }
    }
    //ダメージを受ける
    public void damaged(int atk)
    {
        hp -= atk;
        Debug.Log(hp);
        if (hp < 0)
            ;//ゲームオーバーシーンへ
    }
}
