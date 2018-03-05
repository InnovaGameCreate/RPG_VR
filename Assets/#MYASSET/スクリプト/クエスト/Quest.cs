using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    [SerializeField, TooltipAttribute("クエストタイプ")]
    public QuestType questType;

    [SerializeField, TooltipAttribute("達成規定数：討伐数 or 採集数")]
    private int ClearNum = 10;          

    private int achieveCount;      //達成数

    public ClearItemData[] reward;

    [System.Serializable]
    public class ClearItemData
    {
        public GameObject Item;
        public int ItemNum;
    }


    public int CLEARNUM
    {
        get { return ClearNum; }
        set { ClearNum = value; }
    }
    public int ACHIEVECOUNT
    {

        get { return achieveCount; }
    }

    //達成率
    public int ACHIEVEPERCENT
    {
        get { return (int)((float)ACHIEVECOUNT / (float)CLEARNUM * 100); }
    }

    private bool isClear;       //クリアしたかどうか

    //ターゲット　　モンスター or アイテム
    public GameObject target;

    [SerializeField, TooltipAttribute("クエスト説明")]
    [Multiline]
    public string questText;//説明文



   public struct ItemSet
    {
        GameObject item;
        int num;
    }
    [SerializeField, TooltipAttribute("報酬アイテム")]
    public int[] ClearItem;
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
        if (questType == QuestType.FIGHT && ISCLEAR)
            Debug.Log(target.name+" 討伐クエストCLEAR!!");
    }

    //ターゲットなら達成数を増やす  
    //TODO 敵を倒したとき　　アイテムを入手したときにこの関数を呼ぶ
    public void checkTarget(string tName)
    {
        // 手に入れたアイテム　or 倒した敵　がtargetと一致してるかどうか
        var myRegExp = new Regex(target.name);
        if (myRegExp.IsMatch(tName) && !ISCLEAR)
            achieveCount++;
        if (achieveCount >= ClearNum)
            isClear = true;
    }
}
