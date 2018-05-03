using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{

    //クエストタイプ
    public enum QuestType
    {
        FIGHT,          //討伐
        COLLECT,       //採集
        TALK           //会話
    }

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

    [SerializeField, TooltipAttribute("クエスト名")]
    public string questName;//クエスト名

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

    }

    //ターゲットなら達成数を増やす 
    //引数に対象の名前を入れてやる
    //TODO 敵を倒したとき　　アイテムを入手したときにこの関数を呼ぶ //話した時も
    public bool checkTarget(string tName)
    {
        // 手に入れたアイテム　or 倒した敵　がtargetと一致してるかどうか
        var myRegExp = new Regex(target.name);
        if (!ISCLEAR)
        {
            Debug.Log("quest " + achieveCount);
            if (myRegExp.IsMatch(tName))
                achieveCount++;
            if (achieveCount >= ClearNum)
            {
                isClear = true;
                //if (questType == QuestType.FIGHT)
                //    GameManager.Instance.UMANAGER.LOGCONTROLLER.RegisterLog("[討伐クエスト]　「" + questName + "」　クリア", Color.white);
                //else if (questType == QuestType.COLLECT)
                //    GameManager.Instance.UMANAGER.LOGCONTROLLER.RegisterLog("[採取クエスト]　「" + questName + "」　クリア", Color.white);
                //else if(questType == QuestType.TALK)
                //    GameManager.Instance.UMANAGER.LOGCONTROLLER.RegisterLog("[会話クエスト]　「" + questName + "」　クリア", Color.white);

                return true;
            }
        }
        return false;
    }
}
