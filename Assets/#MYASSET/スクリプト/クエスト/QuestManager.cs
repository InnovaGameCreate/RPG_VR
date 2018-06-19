using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[System.Serializable]
public class QuestManager : MonoBehaviour
{


    [SerializeField]
    List<Quest> questList = new List<Quest>();      //未クリアクエストリスト

    [SerializeField]
    List<Quest> questClearList = new List<Quest>(); //クリアクエストリスト　後は依頼人と会話すればおｋ

    //受諾したクエストすべてに対して　進展対象かどうか調べる
    public void CheckQuestAchievement(string tName)
    {
        foreach (var the_quest in questList)
        {

            if (the_quest.checkTarget(tName))
            {
                questClearList.Add(the_quest);
                questList.Remove(the_quest);

                //クエストタイプでソート
                questClearList.Sort((a, b) =>
                {
                    int result = a.questType - b.questType;
                    return result;
                });
            }
        }
    }

    //そのクエストを受けているかどうか判定
    public bool IsReceiveQuest(string targetName)
    {
        foreach (var _quest in questList)
        {
            var myRegExp = new Regex(targetName);
            if (myRegExp.IsMatch(targetName))
                return true;
        }
        return false;
    }

    //クエストをリストに挿入する　クエストタイプに応じてソート  その後　達成度が大きい順にソート
    public void AddQuest(Quest addQ)
    {
        questList.Add(addQ);
        questList.Sort((a, b) =>
        {
            int result = a.questType - b.questType;
            return result != 0 ? result : b.ACHIEVEPERCENT - a.ACHIEVEPERCENT;
        });


    }



    //npcと会話したときに呼ぶ　　
    //クリア済みクエストとNPCが依頼したクエストが一致していればリストから除去するとともに
    //npcのクエストコンポーネントも除去  報酬を受け取る
    public bool talkNPC(Quest hasQ)
    {
        foreach (var the_quest in questClearList)
        {
            if (hasQ == the_quest)
            {

                BackPack backpack = GameManager.Instance.HEAD.GetComponentInChildren<BackPack>(); Destroy(hasQ);
                //backpack取ってくる(片方で取ってこれない場合がある)
                backpack = GameManager.Instance.HEAD.GetComponentInChildren<BackPack>();
                if (backpack == null)
                    backpack = GameManager.Instance.VRTKMANAGER.GetComponentInChildren<BackPack>();

                //報酬受取作業
                foreach(var back in hasQ.reward)
                {
                    //してされた個数だけ入手
                    for (int i = 0; i < back.ItemNum; i++) 
                    backpack.AcquireItem(back.Item.GetComponent<ItemBase>());

                }


                Destroy(hasQ);

                questClearList.Remove(the_quest);
                return true;
                //TODO　報酬を受け取る
                
            }
        }
        return false;


    }




}
