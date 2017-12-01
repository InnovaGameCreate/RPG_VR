using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    public int ID;//アイテムID
    public bool CanStack;//アイテムがスタック可能かどうか
    public float ObjectBreakTime;//フィールドのアイテムが消える時間
    public string FlavorText;//説明文
    protected PlayerStatus player;  //プレイヤーステータスのインスタンス
    virtual protected void Start()
    {
        player = GameObject.Find("プレイヤーステータス管理").GetComponent<PlayerStatus>();
    }
    // Use this for initialization
    protected abstract void ItemUse();  
}
