using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestManager : MonoBehaviour {


    [SerializeField]
    List<Quest> questList = new List<Quest>();      //未クリアクエストリスト

    [SerializeField]
    List<Quest> questClearList = new List<Quest>(); //クリアクエストリスト　後は依頼人と会話すればおｋ

    //受諾したクエストすべてに対して　進展対象かどうか調べる
    public void CheckQuestAchievement(string tName)
    {
        foreach(var the_quest in questList)
        {
            if (the_quest.checkTarget(tName))
            {
                questClearList.Add(the_quest);
                questList.Remove(the_quest);

                //クエストタイプでソート
                questClearList.Sort((a, b) => {
                    int result = a.questType - b.questType;
                    return result;
                });
            }
        }
    }

    //クエストをリストに挿入する　クエストタイプに応じてソート  その後　達成度が大きい順にソート
    public void AddQuest(Quest addQ)
    {
        questList.Add(addQ);
        questList.Sort((a, b) => {
            int result = a.questType - b.questType;
            return result != 0 ? result :  b.ACHIEVEPERCENT- a.ACHIEVEPERCENT;
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
                Destroy(hasQ);
                questClearList.Remove(the_quest);
                return true;
                //TODO　報酬を受け取る
            }
        }
        return false;


    }
}
