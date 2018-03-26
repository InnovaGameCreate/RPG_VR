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
    public int villageVoiceState = 0;

    private List<AudioClip> clipList = new List<AudioClip>();

    private string voiceFolder = "Assets/#MYASSET/セリフ/";
    private string[] voiceName = new string[8]
    {
        "オベロン/オベロン/主人公の夢.mp3",
        "エルフィ/エルフィ/夢から覚めた時.mp3",
        "エルフィ/エルフィ/チュートリアル前エルフィ.mp3",
        "トーマス/トーマス/チュートリアル前トーマス.wav",
        "トーマス/トーマス/チュートリアル後トーマス.wav",
        "トーマス/トーマス/チュートリアル後2.wav",
        "トーマス/トーマス/回想1.wav",
        "トーマス/トーマス/回想後.wav",

    };


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        nowAudioState = GetComponent<AudioSource>();

        //foreach (AudioSource test in audioClip) {
        //    audioClip.Add(test);
        //        }
        // LoadPlayerEvent();
        //nowAudioState.clip = null;
        //Debug.Log(AssetDatabase.LoadAssetAtPath<AudioClip>(voiceFolder + voiceName[4]));

        for (int i = 0; i < 8; i++)
        {
            clipList.Add(AssetDatabase.LoadAssetAtPath<AudioClip>(voiceFolder + voiceName[i]));
        }

        nowAudioState.clip = clipList[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       // Debug.Log(isPlay);


        //if (isLoadAudio)
        //{
        //    isLoadAudio = false;
        //    Debug.Log(isLoadAudio);
        //    LoadPlayerEvent();
        //}


        //初めのイベント＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        //print(nowAudioState.clip.name);
        if (nowAudioState.clip.name == "主人公の夢")
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
            }

            if (IsFinished(nowAudioState))
            {
                //シーンを民家に
                finishFlag = false;
                isPlaying = false;
                SceneManager.LoadScene("民家1");
                //次のセリフロード（夢から覚めた時)
                //isLoadAudio = true;
                nowAudioState.clip = clipList[1];
            }
        }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝




        //起きた時のイベント＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (nowAudioState.clip.name == "夢から覚めた時")
        {
            //Debug.Log(isPlay);

            if (!isPlaying && !nowAudioState.isPlaying)
                StartCoroutine(DelayTalk(2.0f));//シーン移動から２秒後再生

            //if (Input.GetMouseButtonDown(0))
            //    isPlay = true;

            //再生状態であれば
            if (isPlay)
            {
                nowAudioState.Play();
                StopAllCoroutines();

                isPlay = false;//複数回再生防止
                isMove = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {
                //Debug.Log("終了");
                isMove = true;//動けるように（仮で変数定義）
                finishFlag = false;
                nowAudioState.clip = clipList[2];
                isPlaying = false;
                //デバッグ用
                SceneManager.LoadScene("村");
                //isLoadAudio = true;
            }

        }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝






        //外へ出てからのイベント＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        //  if (//指定した範囲に入っていたら)

        //指定した範囲に入ったら（取り合えず今は左クリックで）

        if (nowAudioState.clip.name == "チュートリアル前エルフィ")
        {
           // Debug.Log(isPlay);

            if (Input.GetMouseButtonDown(0) && !nowAudioState.isPlaying && !isPlaying)
            {
                StartCoroutine(DelayTalk(2.0f));
            }
            //bool isNextAudio = false;

            //Debug.Log(finishFlag);

            //if (IsFinished(nowAudioState))
            //{
            //    トーマスの発言終了なら
            //    if (nowAudioState.transform.gameObject.name == "トーマス")
            //        SceneManager.LoadScene("チュートリアル");

            //    isMove = true;
            //    isNextAudio = true;
            //}
            //else
            //    isMove = false;

            //if (isNextAudio)
            //{

            //    nowAudioState = GameObject.Find("トーマス").GetComponent<AudioSource>();
            //    isPlay = true;
            //}

            //if (Input.GetMouseButtonDown(0))
            //    isPlay = true;

            //if (isPlay)
            //{
            //    nowAudioState.Play();
            //    isPlay = false;
            //}

            if (isPlay)
            {
                StopAllCoroutines();
                isMove = false;
                nowAudioState.Play();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {
                isPlaying = false;
                finishFlag = false;
                nowAudioState.clip = clipList[3];//トーマス
                isPlay = true;

            }
        }

        if (nowAudioState.clip.name == "チュートリアル前トーマス")
        {
            if (!isPlaying && !nowAudioState.isPlaying)
                StartCoroutine(DelayTalk(2.0f));

            if (isPlay)
            {
                nowAudioState.Play();
                StopAllCoroutines();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {
                finishFlag = false;
                isPlaying = false;
                nowAudioState.clip = clipList[4];//トーマス
                isMove = true;
                SceneManager.LoadScene("チュートリアル");

            }
        }



        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝




        //チュートリアル中でのイベント＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (SceneManager.GetActiveScene().name == "チュートリアル")
        {
            //チュートリアル終了したら（とりあえず左クリックで終了）
            if (Input.GetMouseButtonDown(0))
            {

                SceneManager.LoadScene("村");
                villageVoiceState = 1;
            }


        }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝




        //チュートリアル終了後でのイベント＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        if (nowAudioState.clip.name == "チュートリアル後トーマス")
        {


            if (villageVoiceState == 1 && !nowAudioState.isPlaying && !isPlaying)
                StartCoroutine(DelayTalk(2.0f));

            if (isPlay)
            {

                isMove = false;
                nowAudioState.Play();
                StopAllCoroutines();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {
                //SceneManager.LoadScene("民家1");
                nowAudioState.clip = clipList[5];
                isPlaying = false;
                finishFlag = false;
                isMove = true;

            }
        }



        if (nowAudioState.clip.name == "チュートリアル後2")
        {
            if (!isPlaying && !nowAudioState.isPlaying)
                StartCoroutine(DelayTalk(2.0f));

            if (isPlay)
            {


                isMove = false;
                nowAudioState.Play();
                StopAllCoroutines();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {

                isPlaying = false;
                finishFlag = false;
                isMove = true;
                nowAudioState.clip = clipList[6];
                Debug.Log("終了してます");
                //デバッグ用
                SceneManager.LoadScene("民家1");

            }

        }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝


        //村長の家に入ったときのイベント＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝


        if (SceneManager.GetActiveScene().name == "民家1" && nowAudioState.clip.name == "回想1")
        {
            if (!nowAudioState.isPlaying)
                StartCoroutine(DelayTalk(2.0f));

            if (isPlay)
            {

                StopAllCoroutines();

                isMove = false;
                nowAudioState.Play();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {

                isPlaying = false;
                finishFlag = false;

                SceneManager.LoadScene("真暗2");

            }

        }


        //回想シーン
        if (SceneManager.GetActiveScene().name == "真暗2" && nowAudioState.clip.name == "回想1")
        {
            if (!nowAudioState.isPlaying)
                StartCoroutine(DelayTalk(2.0f));

            if (isPlay)
            {

                StopAllCoroutines();

                isMove = false;
                nowAudioState.Play();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {

                isPlaying = false;
                finishFlag = false;
                SceneManager.LoadScene("民家1");
                nowAudioState.clip = clipList[7];
            }
        }


        if (nowAudioState.clip.name == "回想後")
        {
            if (!nowAudioState.isPlaying)
                StartCoroutine(DelayTalk(2.0f));

            if (isPlay)
            {

                StopAllCoroutines();

                isMove = false;
                nowAudioState.Play();
                isPlay = false;
            }

            //セリフ終了したら
            if (IsFinished(nowAudioState))
            {

                isPlaying = false;
                finishFlag = false;
                isMove = true;
                nowAudioState.clip = null;
            }
        }

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

    }

    //指定された時間とめてセリフを話す
    private IEnumerator DelayTalk(float waitTime)
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

        if (Input.GetMouseButtonDown(1))
            return true;

        //Debug.Log((audio.time));
        // Debug.Log(audio.clip.length);
        //再生終了直前で終了フラグを立てる（フラグを立てないと再生していない時にもtrueが返ってしまう）


        //今のところこれだとうまくいく・・・？
        if(isPlaying)
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

        if (finishFlag)
        {
            //Debug.Log("成功");
            if (audio.time == 0.0f && !audio.isPlaying)
            {

                return true;
            }
        }




        return false;



    }

}
