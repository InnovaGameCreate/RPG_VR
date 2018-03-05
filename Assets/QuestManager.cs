using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestManager : MonoBehaviour {


    [SerializeField]
    List<Quest> questList = new List<Quest>();


    //受諾したクエストすべてに対して　進展対象かどうか調べる
    public void CheckQuestAchievement(string tName)
    {
        foreach(var the_quest in questList)
        {
            the_quest.checkTarget(tName);
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
   

    //TODO    UI環境でクエスト報酬受領後　リストから外す
}
