using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    [SerializeField, TooltipAttribute("アイテムID")]
    public int ID;//アイテムID
    [SerializeField, TooltipAttribute("スタック可能かどうか")]
    public bool CanStack;//アイテムがスタック可能かどうか

    private const float ObjectBreakTime = 5;//フィールドのアイテムが消える時間

    [SerializeField, TooltipAttribute("説明文")]
    [Multiline]
    public string FlavorText;//説明文
    [SerializeField, TooltipAttribute("プレイヤーステータスのインスタンス")]
    protected PlayerStatus playerstatus;  //プレイヤーステータスのインスタンス

    virtual protected void Start()
    {
        playerstatus = GameObject.Find("プレイヤーステータス管理").GetComponent<PlayerStatus>();
    }
    // Use this for initialization
    protected abstract bool ItemUse();  //使ったらtrueを返す
}
