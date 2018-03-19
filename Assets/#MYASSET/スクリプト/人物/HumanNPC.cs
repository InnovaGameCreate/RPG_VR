using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanNPC : HumanBase
{

    private bool questing;  //プレイヤーにクエストを与えているかどうか

    private void Update()
    {


    }
    //プレイヤーと会話したときに呼ぶ
    void talkWithPlayer()
    {
        if (questing)
            if (GameManager.Instance.QMANAGER.talkNPC(GetComponent<Quest>()))
                questing = false;
    }

    //TODO  QuestManager の AddQuest関数で　クエストを受ける
    //引数にクエストを入れるが GetComponent<Quest>()で入れること
    //NPCがクエスト複数持ちの際　一番上のクエストから一つずつ引き受けることにする
    //プレイヤーにクエストを与える
    void sendQuest()
    {
        if (!questing)
        {
            GameManager.Instance.QMANAGER.AddQuest(GetComponent<Quest>());
            questing = true;
        }
    }
}
