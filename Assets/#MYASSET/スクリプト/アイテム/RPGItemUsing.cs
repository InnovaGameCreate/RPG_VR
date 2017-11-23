using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class RPGItemUsing : RPGItemObject
{

    /*
     *  アイテムサブクラス
     *  実装時にはこちらをアタッチ(ひな形)
     */
    public enum ItemType
    {
        Healing,    //回復アイテム
        StatsUp,    //ステアップアイテム
        Equipment,  //装備アイテム
        Important   //鍵等の進行系アイテム
    };

    public ItemType Kind;
    public float Effct;         //効果量
    public float IntervalTime;  //使用クールタイム
    public bool CanSetWaist;    //腰に装備可能かどうか
    public bool CanThrowAway;   //捨てれるかどうか
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    /*トリガー*/
    protected override void TriggerPressedHandler(object sender, ControllerInteractionEventArgs e)//トリガーを押したとき
    {
        triggerd = true;


    }

    protected override void TriggerReleasedHandler(object sender, ControllerInteractionEventArgs e)//トリガーを離したとき
    {
        triggerd = false;


    }
    /*タッチパッド*/
    protected override void TouchPadPressedHandler(object sender, ControllerInteractionEventArgs e)//タッチパッドを押したとき
    {
        Touched = true;


    }

    protected override void TouchPadReleasedHandler(object sender, ControllerInteractionEventArgs e)//タッチパッドを離したとき
    {
        Touched = false;


    }

    /*グリップ*/
    protected override void GripPressedHandler(object sender, ControllerInteractionEventArgs e)//グリップを押したとき
    {
        Griped = true;


    }

    protected override void GripReleasedHandler(object sender, ControllerInteractionEventArgs e)//グリップを離したとき
    {
        Griped = false;


    }
}
