using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VoiceEvent : MonoBehaviour
{

    private AudioSource nowAudioState;//現在の再生される音声
    private AudioSource villageDaugther;
    private bool isMove = true;//移動できるかどうか
    private bool isPlay = false;
    private bool finishFlag = false;
    private bool isLoadAudio = false;
    public GameObject player;
    public int villageVoiceState = 0;


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        nowAudioState = GetComponent<AudioSource>();

        //foreach (AudioSource test in audioClip) {
        //    audioClip.Add(test);
        //        }
          LoadPlayerEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isLoadAudio)
            LoadPlayerEvent();




        //print(nowAudioState.clip.name);
        if (nowAudioState.clip.name == "主人公の夢")
            if (IsFinished(nowAudioState))
            {
                //シーンを民家に
                finishFlag = false;
                SceneManager.LoadScene("民家1");
                isLoadAudio = true;
            }


        if (nowAudioState.clip.name == "夢から覚めた時")
        {
            Debug.Log(isPlay);
            if (Input.GetMouseButtonDown(0))
                isPlay = true;          
            if (isPlay)
            {
                nowAudioState.Play();
                isPlay = false;
            }

            if (IsFinished(nowAudioState))
            {
                isMove = true;
                finishFlag = false;
                SceneManager.LoadScene("村");
                isLoadAudio = true;

            }
            else
                isMove = false;
        }

        if (nowAudioState.clip.name == "チュートリアル前")
        {
            bool isNextAudio = false;

            Debug.Log(finishFlag);

            if (IsFinished(nowAudioState))
            {
                //トーマスの発言終了なら
                if (nowAudioState.transform.gameObject.name == "トーマス")
                    SceneManager.LoadScene("チュートリアル");

                isMove = true;
                isNextAudio = true;
            }
            else
                isMove = false;

            if (isNextAudio)
            {

                nowAudioState = GameObject.Find("トーマス").GetComponent<AudioSource>();
                isPlay = true;
            }

            if (Input.GetMouseButtonDown(0))
                isPlay = true;

            if (isPlay)
            {
                nowAudioState.Play();
                isPlay = false;
            }

        }

        if (SceneManager.GetActiveScene().name == "チュートリアル")
        {
            if (Input.GetMouseButtonDown(0))
            {
                villageVoiceState = 1;
                SceneManager.LoadScene("村");
            }


        }

        if (nowAudioState.clip.name == "チュートリアル後")
        {

            if (Input.GetMouseButtonDown(0))
                isPlay = true;

            if (isPlay)
            {
                nowAudioState.Play();
                isPlay = false;
            }
        }

    }



    private void LoadPlayerEvent()
    {


        if (SceneManager.GetActiveScene().name == "民家1")
        {
            Vector3 startVec;
            nowAudioState = GameObject.Find("エルフィ").GetComponent<AudioSource>();
            startVec = GameObject.Find("Myhouse/bed").transform.position;
            player.transform.position = new Vector3(startVec.x, startVec.y + 5, startVec.z);
            isPlay = true;


        }

        if (SceneManager.GetActiveScene().name == "村")
        {
          

            switch (villageVoiceState)
            {
                case 1:
                    nowAudioState = GameObject.Find("エルフィ").GetComponent<AudioSource>();
                    break;
                case 0:
                   
                    nowAudioState = GameObject.Find("トーマス").GetComponent<AudioSource>();
                    break;


            }
        }



        isLoadAudio = false;

    }

    public bool IsFinished(AudioSource audio)
    {

        if (Input.GetMouseButtonDown(1))
            return true;

        //Debug.Log((audio.time));
        // Debug.Log(audio.clip.length);
        //再生終了直前で終了フラグを立てる（フラグを立てないと再生していない時にもtrueが返ってしまう）
        if ((audio.time + Time.deltaTime) > audio.clip.length && audio.isPlaying || isPlay)//||より左はなんかうまくいかなかった
            finishFlag = true;

        if (finishFlag)
        {
            if (audio.time == 0.0f && !audio.isPlaying)
            {
                return true;
            }
        }


        return false;



    }

}
