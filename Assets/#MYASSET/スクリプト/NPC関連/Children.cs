using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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
        eSERIF2_END,
        eEventNum,
        eEventNone,
    };

    public static bool[] talkFlag = new bool[(int)EVENT_NO.eEventNum];
    private EVENT_NO nextEventNum;
    private EVENT_NO beforeEvent;

    public NavMeshAgent nina;
    public NavMeshAgent Roruhu;
    public NavMeshAgent rudy;

    public Animator ninaAnimator;
    public Animator roruhuAnimator;
    public Animator rudyAnimator;


    // Use this for initialization
   
    protected override void Start()
    {
        base.Start();
        talkNum = (int)TALK_NO.eTalkNum;
        nextEventNum = EVENT_NO.eEventNone;
        beforeEvent = EVENT_NO.eEventNone;
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
        //第二セリフをそれぞれが言い終わると次からは第一セリフだけを繰り返す
        //最初にしゃべるセリフ
        if (IsTalk())
        {
            //talkFlag[(int)EVENT_NO.eROLF] = true;

            //ナビを止める
            nina.isStopped = true;
            Roruhu.isStopped = true;
            rudy.isStopped = true;

            //アニメーションをstandにする
            ninaAnimator.SetBool("isTalk", true);
            roruhuAnimator.SetBool("isTalk", true);
            rudyAnimator.SetBool("isTalk", true);



            //一度も会話したことのない場合
            if (beforeEvent == EVENT_NO.eEventNone && nextEventNum == EVENT_NO.eEventNone)
                nextEventNum = EVENT_NO.eROLF;


            if (beforeEvent == EVENT_NO.eNINA)
            {

                //セリフ２は一度聞いているならば
                if (talkFlag[(int)EVENT_NO.eSERIF2_END])
                    nextEventNum = EVENT_NO.eROLF;
                else
                    nextEventNum = EVENT_NO.eROLF2;

            }


        }

        //Debug.Log(nextEventNum);

        switch (nextEventNum)
        {
            case EVENT_NO.eROLF:

                Talk((int)TALK_NO.eSerif_RO);
                nextEventNum = EVENT_NO.eRUDI;
                isTalk = false;
                isPlaying = true;

                break;

            case EVENT_NO.eRUDI:
                //Debug.Log("rudy1");
                //Debug.Log(isPlaying);
                //Debug.Log(isTalk);

                if (IsChildrenFinished(TalkAudioSource((int)TALK_NO.eSerif_RO)))
                {
                    //Debug.Log("rudy2");
                    Talk((int)TALK_NO.eSerif_RU);
                    nextEventNum = EVENT_NO.eNINA;
                    isTalk = false;
                    isPlaying = true;

                }
                break;
            case EVENT_NO.eNINA:
                //Debug.Log("nina1");
                if (IsChildrenFinished(TalkAudioSource((int)TALK_NO.eSerif_RU)))
                {
                    //Debug.Log("nina2");


                    Talk((int)TALK_NO.eSerif_NI);
                    isTalk = false;
                    isPlaying = true;
                    beforeEvent = nextEventNum;//直前のイベントを保存
                    nextEventNum = EVENT_NO.eEventNone;
                }
                break;

            //一度だけ呼ばれるセリフ
            case EVENT_NO.eROLF2:

                Talk((int)TALK_NO.eSerif2_RO);
                nextEventNum = EVENT_NO.eRUDI2;
                isTalk = false;
                isPlaying = true;

                break;

            case EVENT_NO.eRUDI2:
                if (IsChildrenFinished(TalkAudioSource((int)TALK_NO.eSerif2_RO)))
                {
                    Talk((int)TALK_NO.eSerif2_RU);
                    nextEventNum = EVENT_NO.eNINA2;
                    isTalk = false;
                    isPlaying = true;

                }
                break;
            case EVENT_NO.eNINA2:
                if (IsChildrenFinished(TalkAudioSource((int)TALK_NO.eSerif2_RU)))
                {
                    Talk((int)TALK_NO.eSerif2_NI);
                    isTalk = false;
                    isPlaying = true;
                    //nextEventNum = EVENT_NO.eRUDI;
                    nextEventNum = EVENT_NO.eEventNone;
                    talkFlag[(int)EVENT_NO.eSERIF2_END] = true;
                }
                break;

            //イベントがないときや終了したらここに移動する
            case EVENT_NO.eEventNone:
                Debug.Log("noneEventChildren");

                if (IsChildrenFinished(TalkAudioSource((int)TALK_NO.eSerif2_NI))
                    || IsChildrenFinished(TalkAudioSource((int)TALK_NO.eSerif_NI)))
                {
                    StartCoroutine(DelayAnim(3.0f));
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

     //アニメーションコルーチン
    public IEnumerator DelayAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        nina.isStopped = false;
        Roruhu.isStopped = false;
        rudy.isStopped = false;


        //アニメーションをrunにする
        ninaAnimator.SetBool("isTalk", false);
        roruhuAnimator.SetBool("isTalk", false);
        rudyAnimator.SetBool("isTalk", false);

        StopCoroutine("DelayAnim");

    }
}
