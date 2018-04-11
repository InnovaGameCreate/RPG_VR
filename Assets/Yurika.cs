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
    void Update()
    {

        //普通の会話終了で選択画面描画
        if (IsFinished(TalkAudioSource((int)EVENT_NO.eNormal)))
        {
            isTalk = false;
            isPlaying = false;
            PrintSelectUI();
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

        if (false /*クエスト2のセリフを言う時*/)
        {
            Talk((int)TALK_NO.eQuest2);
        }


    }

    public void SetNormalTalk2()
    {
        talkFlag[(int)EVENT_NO.eNormal2] = true;
    }
}
