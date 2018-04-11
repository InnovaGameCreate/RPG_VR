using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adler : TalkEvent
{

    enum TALK_NO
    {
        eSerif,//ランダムセリフ
        eSerif2,
        eSerif3,
        eQuest,//クエスト依頼
        eQuest2,//クエスト終了時
        eDecline,//断ったとき
        eTalkNum,
    };

    enum EVENT_NO
    {
        eNormal,
        eQuest,
        eQuest2,
        eDecline,
        eEventNum,
    }


    bool[] talkFlag = new bool[(int)EVENT_NO.eEventNum];

    // Use this for initialization
    protected override void Start()
    {
        //Array.Resize(ref talkFlag, (int)TALK_NO.eTalkNum);
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
            Debug.Log("tanasu12");

            //ランダムで話す内容を変える
            talkFlag[(int)EVENT_NO.eNormal] = false;
            Talk(Random.Range((int)TALK_NO.eSerif, (int)TALK_NO.eSerif3 + 1));

        }

        if (talkFlag[(int)EVENT_NO.eQuest])
        {
            talkFlag[(int)EVENT_NO.eQuest] = false;

            Talk((int)TALK_NO.eQuest);
        }

        if (false /*クエスト2の依頼を受けた時*/)
        {
            Talk((int)TALK_NO.eQuest2);
        }

        if (false/*断った時*/)
        {
            Talk((int)TALK_NO.eDecline);
        }
    }


    public void ControllFlag()
    {

    }

    public void SetNormalTalk()
    {
        talkFlag[(int)EVENT_NO.eNormal] = true;
    }

    public void SetQuest1()
    {
        talkFlag[(int)EVENT_NO.eQuest] = true;

    }
}


