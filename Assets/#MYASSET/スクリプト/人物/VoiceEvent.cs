using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class VoiceEvent : MonoBehaviour
{

    private AudioSource nowAudioState;//現在の再生される音声
    private bool isMove = true;//移動できるかどうか
    private bool isPlay = false;//再生可能かどうか
    private bool isPlaying = false; //再生中かどうか
    private bool finishFlag = false;
    private bool isLoadAudio = false;

    private float deltaTime = 0;//再生時間との比較(再生終了判定に使う）
    public GameObject player;
    public bool isEventHappen = false;//イベントが起こったかどうか
    public int villageVoiceState = 0;


    public enum VoiceKind
    {
        MY_DREAM,
        WAKE_UP,
        T_B_ELFY,//チュートリアル前エルフィ
        T_B_THOMAS,
        T_A_THOMAS,//チュートリアル後トーまマス
        T_A_THOMAS2,
        BACK_B_THOMAS,
        BACK_OBERON,
        BACK_A_THOMAS,
        VOICE_NUM,
        NONE,

    };

    public VoiceKind nowVoiceNo;//現在のボイス番号
    private List<AudioClip> clipList = new List<AudioClip>();
    private string voiceFolder = "Assets/#MYASSET/セリフ/";
    private string[] voiceName = new string[9]
    {
        "オベロン/オベロン/主人公の夢.wav",
        "エルフィ/エルフィ/夢から覚めた時.wav",
        "エルフィ/エルフィ/チュートリアル前エルフィ.wav",
        "トーマス/トーマス/チュートリアル前トーマス.wav",
        "トーマス/トーマス/チュートリアル後トーマス.wav",
        "トーマス/トーマス/チュートリアル後2.wav",
        "トーマス/トーマス/回想1.wav",
        "トーマス/トーマス/回想1.wav",
        "トーマス/トーマス/回想後.wav",

    };


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        nowAudioState = GetComponent<AudioSource>();
        nowVoiceNo = VoiceEvent.VoiceKind.MY_DREAM;
        //foreach (AudioSource test in audioClip) {
        //    audioClip.Add(test);
        //        }
        // LoadPlayerEvent();
        //nowAudioState.clip = null;
        //Debug.Log(AssetDatabase.LoadAssetAtPath<AudioClip>(voiceFolder + voiceName[4]));

        for (int i = 0; i < 9; i++)
        {
            clipList.Add(AssetDatabase.LoadAssetAtPath<AudioClip>(voiceFolder + voiceName[i]));
        }


        nowAudioState.clip = clipList[(int)VoiceKind.MY_DREAM];
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(isPlay);


        //if (isLoadAudio)
        //{
        //    isLoadAudio = false;
        //    Debug.Log(isLoadAudio);
        //    LoadPlayerEvent();
        //}

        //現在のセリフ番号によって処理を行う
        switch (nowVoiceNo)
        {
            case VoiceKind.MY_DREAM:
                Voice();
                if (nowVoiceNo == VoiceKind.WAKE_UP)
                    GameManager.Instance.SceneChengeManager("民家1", "Myhouse/bed/Bed_Child_Size_Module_1A (1)/Bed_Child_Size_1A");
                break;
            case VoiceKind.WAKE_UP:
                Voice();
                //デバッグ
                if (nowVoiceNo == VoiceKind.T_B_ELFY)
                    GameManager.Instance.SceneChengeManager("村", "家の前");
                break;
            case VoiceKind.T_B_ELFY:
                if (isEventHappen)
                    Voice();
                break;
            case VoiceKind.T_B_THOMAS:
                Voice();
                if (nowVoiceNo == VoiceKind.T_A_THOMAS)
                    GameManager.Instance.SceneChengeManager("チュートリアル試し", "box");

                break;
            case VoiceKind.T_A_THOMAS:
                if (isEventHappen)
                    Voice();

                break;
            case VoiceKind.T_A_THOMAS2:
                Voice();
                if (nowVoiceNo == VoiceKind.BACK_B_THOMAS)
                    GameManager.Instance.SceneChengeManager("民家1", "WarpPos");

                break;
            case VoiceKind.BACK_B_THOMAS:
                Voice();
                if (nowVoiceNo == VoiceKind.BACK_OBERON)
                    SceneManager.LoadScene("真暗2");
                break;
            case VoiceKind.BACK_OBERON:
                Voice();
                if (nowVoiceNo == VoiceKind.BACK_A_THOMAS)
                    GameManager.Instance.SceneChengeManager("民家1", "WarpPos");
                break;
            case VoiceKind.BACK_A_THOMAS:
                Voice();
                break;
            case VoiceKind.NONE:
                break;

        }


    }

    //指定された時間とめてセリフを話す
    public IEnumerator DelayTalk(float waitTime)
    {
        isPlaying = true;
        yield return new WaitForSeconds(waitTime);
        isPlay = true;
    }




    //↓おそらくもう使わない？
    private void LoadPlayerEvent()
    {

        //真暗からのエルフィイベント
        if (SceneManager.GetActiveScene().name == "民家1")
        {

            Vector3 startVec;
            Debug.Log(GameObject.Find("エルフィ"));
            // nowAudioState = GameObject.Find("エルフィ").GetComponent<AudioSource>();
            nowAudioState.clip = clipList[1];
            startVec = GameObject.Find("Myhouse/bed").transform.position;
            //後で位置かえる
            player.transform.position = new Vector3(startVec.x, startVec.y + 5, startVec.z);

        }


        //チュートリアルの場所まで来たとき
        if (SceneManager.GetActiveScene().name == "村")
        {


            switch (villageVoiceState)
            {
                //チュートリアル前
                case 0:
                    nowAudioState.clip = clipList[3];
                    break;
                //チュートリアル後
                case 1:
                    nowAudioState.clip = AssetDatabase.LoadAssetAtPath<AudioClip>(voiceFolder + voiceName[4]);
                    break;


            }
        }



        isLoadAudio = false;

    }


    //再生終了判定
    public bool IsFinished(AudioSource audio)
    {


        //デバッグ用右クリックで再生終了判定（たぶん上手く動いていない）
        if (Input.GetMouseButtonUp(1))
            return true;

        //Debug.Log((audio.time));
        // Debug.Log(audio.clip.length);
        //再生終了直前で終了フラグを立てる（フラグを立てないと再生していない時にもtrueが返ってしまう）


        //今のところこれだとうまくいく・・・？
        if (isPlaying)
            deltaTime += Time.deltaTime;
        if (audio.clip.length + 2.0f <= deltaTime)//間隔考量した２秒
        {
            deltaTime = 0;
            return true;
        }

        //成立しない場合あり（３割りくらい？）
        //if ((audio.time + Time.deltaTime) > audio.clip.length && audio.isPlaying /*|| isPlay*/)//||より左はなんかうまくいかなかった
        //        finishFlag = true;
        //return true;


        //float endTime = audio.clip.length * Time.timeScale
        //if(audio.time == endTime)

        //if (finishFlag)
        //{
        //    //Debug.Log("成功");
        //    if (audio.time == 0.0f && !audio.isPlaying)
        //    {

        //        return true;
        //    }
        //}




        return false;



    }


    // セリフを流すメソッド
    public void Voice()
    {


        if (!isPlaying && !nowAudioState.isPlaying)
            StartCoroutine(DelayTalk(2.0f));

        //if (Input.GetMouseButtonDown(0))
        //    isPlay = true;

        if (isPlay)
        {
            nowAudioState.Play();
            StopAllCoroutines();
            isPlay = false;//複数回再生防止
            isMove = false;
        }
        //↑が共通



        if (IsFinished(nowAudioState))
        {
            //シーンを民家に
            nowAudioState.clip = null;
            nowVoiceNo++;//次のセリフの番号
            finishFlag = false;
            isPlaying = false;
            isMove = true;
            isEventHappen = false;
            //次のセリフロード（夢から覚めた時)
            //isLoadAudio = true;
            nowAudioState.clip = clipList[(int)nowVoiceNo];
        }

    }

    public bool ControlIsMove()
    {
        if (isMove)
            return true;
        else
            return false;
    }

    public VoiceKind EventNo()
    {
        return nowVoiceNo;
    }


}
