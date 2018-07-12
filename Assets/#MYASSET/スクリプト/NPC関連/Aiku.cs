using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aiku : TalkEvent
{
    //セリフ
    enum TALK_NO
    {
        eSerif,//ランダムセリフ
        eSerif2,

        eTalkNum,
    };

    //イベントの種類
    enum EVENT_NO
    {
        eNormal,
        eDecline,
        eEventNum,
    }


    bool[] talkFlag = new bool[(int)EVENT_NO.eEventNum];


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        talkNum = (int)TALK_NO.eTalkNum;
        //GetComponent<HumanNPC>().sendQuest();//クエスト受注

    }

    private bool NormalEventEnd()
    {
        return IsFinished(TalkAudioSource((int)TALK_NO.eSerif)) ||
            IsFinished(TalkAudioSource((int)TALK_NO.eSerif2));
    }

    // Update is called once per frame
    protected override void Update()
    {
        //普通の会話終了で選択画面描画
        if (NormalEventEnd())
        {
            isTalk = false;
            isPlaying = false;
            PrintSelectUI();

        }
        if (IsTalk())
            talkFlag[(int)EVENT_NO.eNormal] = true;



        //通常会話であるならば
        if (talkFlag[(int)EVENT_NO.eNormal])
        {
            //Debug.Log("tanasu12");
            SetInputDevice(false);//リモコン操作停止
            //ランダムで話す内容を変える
            talkFlag[(int)EVENT_NO.eNormal] = false;
            Talk(Random.Range((int)TALK_NO.eSerif, (int)TALK_NO.eSerif2 + 1));

        }

    }


    public void ControllFlag()
    {

    }

    public void SetNormalTalk()
    {
        SetInputDevice(false);//リモコン操作停止
        talkFlag[(int)EVENT_NO.eNormal] = true;
    }

    public void SetDecline()
    {
        SetInputDevice(false);//リモコン操作停止
        talkFlag[(int)EVENT_NO.eDecline] = true;


    }

    public void YesSelect()
    {
        GetComponent<HumanNPC>().sendQuest();//クエスト受注
        YesNoUI.SetActive(false);
        IsMove = true;
    }
}
