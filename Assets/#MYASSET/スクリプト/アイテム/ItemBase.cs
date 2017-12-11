using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField, TooltipAttribute("アイテムID")]
    public int ID;//アイテムID
    [SerializeField, TooltipAttribute("スタック可能かどうか")]
    public bool CanStack;//アイテムがスタック可能かどうか

    //アイテムの種類
    public enum ItemType
    {
        None,
        Healing,    //回復アイテム
        StatusUp,    //ステータスアップアイテム
        Equipment,  //装備アイテム
        Important,   //鍵等の進行系アイテム
        Monster,    //モンスター素材
    };
    //アイテムの種類の数
    public static int ItemTypeTotalNum
    {
        get
        {
            return Enum.GetValues(typeof(ItemBase.ItemType)).Length - 1;
        }
    }
    protected ItemType item_type;

    private const float ObjectBreakTime = 5;//フィールドのアイテムが消える時間

    [SerializeField, TooltipAttribute("説明文")]
    [Multiline]
    public string FlavorText;//説明文

    protected PlayerStatus playerstatus;  //プレイヤーステータスのインスタンス

    //prtected かばんクラス　kaban; //後々のかばんくらす  アイテムが回収された時に増やす
    [SerializeField, TooltipAttribute("スタック可能かどうか")]
    public bool droppeditem;   //ステージに生成されたドロップアイテムかどうか    メニュー用とドロップ用で挙動区別するため

    private float time;         //ドロップアイテム回収可能かどうかの時間計測用
    private bool catch_ok;      //ドロップアイテム回収可能かどうか
    virtual protected void Start()
    {
        playerstatus = GameObject.Find("プレイヤーステータス管理").GetComponent<PlayerStatus>();
        item_type = ItemType.None;
    }

    virtual protected void Update()
    {
        if (droppeditem)
        {
            time += Time.deltaTime;
            //2秒後に回収可能となる
            if (time > 2 && !catch_ok)
            {
                catch_ok = true;

            }
        }
    }
    // Use this for initialization
    public abstract bool ItemUse();  //使ったらtrueを返す

    //アイテムが回収された
    private void OnTriggerEnter(Collider other)
    {
        if (catch_ok && GetComponent<BoxCollider>() != null && other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            //TODO   カバンクラスにアイテムを足す処理

        }
    }
}
