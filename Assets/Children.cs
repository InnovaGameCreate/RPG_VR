using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Children : TalkEvent
{

    enum TALK_NO
    {
        eSerif_NI,
        eSerif2_NI,
        eSerif_RU,
        eSerif2_RU,
        eSerif_RO,
        eSerif2_RO,
        eSerif3_RO,
        eTalkNum,
        eNone,
    };

    private enum EVENT_NO
    {
        eNINA,
        eNINA2,
        eRUDI,//UIのボタンを押したときの通常会話
        eRUDI2,
        eROLF,
        eROLF2,
        eEventNum,
        eEventNone,
    };

    bool[] talkFlag = new bool[(int)EVENT_NO.eEventNum];
    private EVENT_NO nextEventNum;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        talkNum = (int)TALK_NO.eTalkNum;
        nextEventNum = EVENT_NO.eEventNone;

    }

    // Update is called once per frame
    void Update()
    {

        //普通の会話終了で選択画面描画
        //if (IsFinished(TalkAudioSource((int)EVENT_NO.eROLF)))
        //{
        //    isTalk = false;
        //    isPlaying = false;
        //    //PrintSelectUI();
        //}


        //ロルフ→ルディ→ニーナ
        //最初にしゃべるセリフ
        if (IsTalk())
        {
            //talkFlag[(int)EVENT_NO.eROLF] = true;
            if(nextEventNum == EVENT_NO.eEventNone)
            nextEventNum = EVENT_NO.eROLF;

        }

        Debug.Log(nextEventNum);

        switch (nextEventNum)
        {
            case EVENT_NO.eROLF:

                Talk((int)TALK_NO.eSerif_RO);
                nextEventNum = EVENT_NO.eRUDI;
                isTalk = false;
                isPlaying = true;

                break;

            case EVENT_NO.eRUDI:
                if (IsFinished(TalkAudioSource((int)TALK_NO.eSerif_RO)))
                {
                    Talk((int)TALK_NO.eSerif_RU);
                    nextEventNum = EVENT_NO.eNINA;
                    isTalk = false;
                    isPlaying = true;

                }
                break;
            case EVENT_NO.eNINA:
                if (IsFinished(TalkAudioSource((int)TALK_NO.eSerif_RU)))
                {
                    Talk((int)TALK_NO.eSerif_NI);
                    isTalk = false;
                    isPlaying = true;
                    //nextEventNum = EVENT_NO.eRUDI;
                    nextEventNum = EVENT_NO.eROLF;


                }
                break;

        }


        //if (talkFlag[(int)EVENT_NO.eROLF])
        //{

        //    talkFlag[(int)EVENT_NO.eROLF] = false;
        //    Talk((int)TALK_NO.eSerif2_RO);

        //}

        //if (talkFlag[(int)EVENT_NO.eNormal2])
        //{

        //    talkFlag[(int)EVENT_NO.eNormal2] = false;
        //    Talk((int)TALK_NO.eSerif2);

        //}

        //if (talkFlag[(int)EVENT_NO.eQuest])
        //{
        //    talkFlag[(int)EVENT_NO.eQuest] = false;

        //    Talk((int)TALK_NO.eQuest);
        //}

        //if (false /*クエスト2のセリフを言う時*/)
        //{
        //    Talk((int)TALK_NO.eQuest2);
        //}


    }

    //public void SetNormalTalk2()
    //{
    //    talkFlag[(int)EVENT_NO.eNormal2] = true;
    //}
}
