using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField, TooltipAttribute("クエストタイプ")]
    public QuestType questType;

    [SerializeField, TooltipAttribute("達成規定数：討伐数 or 採集数")]
    public int ClearNum = 10;

    public int achieveCount;      //達成数

    private bool isClear;       //クリアしたかどうか

    //ターゲット　　モンスター or アイテム
    public GameObject target;

    [SerializeField, TooltipAttribute("クエスト説明")]
    [Multiline]
    public string questText;//説明文

    //クエストタイプ
    public enum QuestType
    {
        FIGHT,          //討伐
        COLLECT         //採集
    }

    public bool ISCLEAR
    {
        get { return isClear; }
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.K))
            checkTarget("ゴブリン");
    }

    //ターゲットなら達成数を増やす  
    //TODO 敵を倒したとき　　アイテムを入手したときにこの関数を呼ぶ
    void checkTarget(string tName)
    {
        // 手に入れたアイテム　or 倒した敵　がtargetと一致してるかどうか
        var myRegExp = new Regex(target.name);
        if (myRegExp.IsMatch(tName))
            achieveCount++;
        if (achieveCount >= ClearNum)
            isClear = true;
    }
}
