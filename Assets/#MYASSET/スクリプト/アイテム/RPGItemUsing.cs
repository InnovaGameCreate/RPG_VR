using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGItemUsing : MonoBehaviour {

    /*
     *  アイテムサブクラス
     *  実装時にはこちらをアタッチ
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
    public 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
