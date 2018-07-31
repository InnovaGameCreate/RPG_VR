using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karura : TalkEvent
{

    public int count = 0;
    //セリフ
    enum TALK_NO
    {
        eSerif,//ランダムセリフ
        eSerif2,
        eQuest,//クエスト依頼
        eQuest2,//クエスト受注時
        eDecline,//断ったとき
        eTalkNum,
    };

    //イベントの種類
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
            SetUIObj(true);
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
            //Talk(Random.Range((int)TALK_NO.eSerif, (int)TALK_NO.eSerif2 + 1));
            Talk((int)TALK_NO.eSerif+count++);

            if(count >= 2)
            {
                count = 0;
            }


        }

        //クエスト１の会話状態であるならば
        if (talkFlag[(int)EVENT_NO.eQuest])
        {
            //クエスト１セリフ開始
            talkFlag[(int)EVENT_NO.eQuest] = false;
            Talk((int)TALK_NO.eQuest);
        }

        //クエスト１の発言終了で
        if (IsFinished(talkAudio[(int)TALK_NO.eQuest]))
        {
            //発言終了で再生終了を通知し
            //選択肢UIを表示
            isPlaying = false;
            SetUIObj(false);
            YesNoUI.SetActive(true);

        }

        if (false /*クエスト2の依頼を受けた時*/)
        {
            Talk((int)TALK_NO.eQuest2);
        }



        if (talkFlag[(int)EVENT_NO.eDecline])
        {
            //会話発生フラグを戻して会話
            talkFlag[(int)EVENT_NO.eDecline] = false;
            Talk((int)TALK_NO.eDecline);
        }

        if (IsFinished(talkAudio[(int)TALK_NO.eDecline]))
        {
            YesNoUI.SetActive(false);
            SetInputDevice(true);//リモコン操作解除

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

    public void SetQuest1()
    {

        SetInputDevice(false);//リモコン操作停止
        talkFlag[(int)EVENT_NO.eQuest] = true;

    }

    public void SetDecline()
    {
        SetInputDevice(false);//リモコン操作停止
        talkFlag[(int)EVENT_NO.eDecline] = true;


    }

    public void EndTalk()
    {
        IsMove = true;
        SetUIObj(false);


    }
    public void YesSelect()
    {
        GetComponent<HumanNPC>().sendQuest();//クエスト受注
        YesNoUI.SetActive(false);
        IsMove = true;
    }


    
}
