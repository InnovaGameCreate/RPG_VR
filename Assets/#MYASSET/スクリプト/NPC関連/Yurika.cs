using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yurika : TalkEvent
{

    enum TALK_NO
    {
        eSerif,//ランダムセリフ
        eSerif2,
        eQuest,//クエスト依頼
        eQuest2,//クエスト終了時
        eTalkNum,
    };

    enum EVENT_NO
    {
        eNormal,
        eNormal2,//UIのボタンを押したときの通常会話
        eQuest,
        eQuest2,
        eEventNum,
    }

    bool[] talkFlag = new bool[(int)EVENT_NO.eEventNum];


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        talkNum = (int)TALK_NO.eTalkNum;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //普通の会話終了で選択画面描画
        if (IsFinished(TalkAudioSource((int)EVENT_NO.eNormal)))
        {
            isTalk = false;
            isPlaying = false;
            PrintSelectUI();
        }

        if (IsFinished(TalkAudioSource((int)EVENT_NO.eQuest)))
        {
            isTalk = false;
            isPlaying = false;
            TalkAudioSource((int)EVENT_NO.eQuest).Stop();//次にいった時上手く行かないので
            talkFlag[(int)EVENT_NO.eQuest2] = true;
            

        }

        if (IsFinished(TalkAudioSource((int)EVENT_NO.eQuest2)))
        {
            isTalk = false;
            isPlaying = false;
            GameManager.Instance.QMANAGER.CheckQuestAchievement("ユリカ");

        }
        if (IsTalk())
            talkFlag[(int)EVENT_NO.eNormal] = true;


        if (talkFlag[(int)EVENT_NO.eNormal])
        {

            talkFlag[(int)EVENT_NO.eNormal] = false;
            Talk((int)TALK_NO.eSerif);

        }

        if (talkFlag[(int)EVENT_NO.eNormal2])
        {

            talkFlag[(int)EVENT_NO.eNormal2] = false;
            Talk((int)TALK_NO.eSerif2);

        }

        if (talkFlag[(int)EVENT_NO.eQuest])
        {
            talkFlag[(int)EVENT_NO.eQuest] = false;
            Talk((int)TALK_NO.eQuest);
        }


        if (talkFlag[(int)EVENT_NO.eQuest2])
        {
            Debug.Log(isPlaying);
            talkFlag[(int)EVENT_NO.eQuest2] = false;
            Talk((int)TALK_NO.eQuest2);
        }


    }

    public void SetNormalTalk2()
    {   //ユリカと話すクエストを受けていれば
        if (GameManager.Instance.QMANAGER.IsReceiveQuest("ユリカ"))
            talkFlag[(int)EVENT_NO.eQuest] = true;//クエストのイベント発生
        else
            talkFlag[(int)EVENT_NO.eNormal2] = true;
    }
}
