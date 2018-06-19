using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Franz : TalkEvent {

    //セリフ
    enum TALK_NO
    {
        eSerif1,//いらっしゃい
        eSerif2,//街からの物資があーだこーだ
        eTalkNum,
    };


    [SerializeField]
    private GameObject buyUI;//購入用のUI（アタッチしてやる）

    //セリフの数だけフラグを立てる
    bool[] talkFlag = new bool[(int)TALK_NO.eTalkNum];


	// Use this for initialization
	protected override void Start () {
        base.Start();
        base.talkNum = (int)TALK_NO.eTalkNum;

    }

    // Update is called once per frame
    protected override void Update () {
       // base.Update();

        Delta();
        if (IsTalk() && !buyUI.activeSelf)
            talkFlag[(int)TALK_NO.eSerif1] = true;
        else
            isTalk = false;


        //通常会話であるならば
        if (talkFlag[(int)TALK_NO.eSerif1])
        {
            //Debug.Log("tanasu12");
            SetInputDevice(false);//リモコン操作停止
            //ランダムで話す内容を変える
            talkFlag[(int)TALK_NO.eSerif1] = false;
            Talk((int)TALK_NO.eSerif1);

        }

        //通常会話終了でUIを描画
        if (IsFinished(TalkAudioSource((int)TALK_NO.eSerif1))){
            isTalk = false;
            isPlaying = false;
            SetUIObj(true);
            PrintSelectUI();
            Debug.Log("kiteru");
        }


       

        //セリフ２のフラグが立てば
        if(talkFlag[(int)TALK_NO.eSerif2])
        {
            SetTalk(TALK_NO.eSerif2);
        }

        //セリフ2終了で
        if (IsFinished(TalkAudioSource((int)TALK_NO.eSerif2)))
        {
            base.isTalk = false;
            base.isPlaying = false;
        }


    }

    private void SetTalk(TALK_NO no)
    {
        SetInputDevice(false);//リモコン操作停止
        talkFlag[(int)no] = false;
        Talk((int)no);
    }

    public void SetNormalTalk()
    {
        talkFlag[(int)TALK_NO.eSerif2] = true;
    }

    //購入を選択した場合
    public void SelectBuy()
    {
        //選択のUIを消して
        SetUIObj(false);
        //購入画面を表示する
        buyUI.SetActive(true);


    }
    
    //会話終了
    public void EndTalk()
    {
        SetInputDevice(true);//リモコン操作停止
        SetUIObj(false);
        buyUI.SetActive(false);

    }
}
