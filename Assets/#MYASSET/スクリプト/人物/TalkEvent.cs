using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TalkEvent : MonoBehaviour {

    float deltaTime;
    bool isPlaying;
    AudioSource[] talkAudio;
    [SerializeField]
    RPGVR_HandCtrl rightCtrl;
    [SerializeField]
    RPGVR_HandCtrl leftCtrl;
    public GameObject test;

    //List<AudioSource> playerAudioList = new List<AudioSource>();

    // Use this for initialization
    void Awake() {
        talkAudio = GetComponents<AudioSource>();
        //test = GameObject.Find("ゲームマネージャ/[VRTK_SDKManager]/SteamVR/Controller (right)/RightHand");
        //test = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/Controller (right)/RightHand");
        rightCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/RightHand").GetComponent<RPGVR_HandCtrl>();
        leftCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftHand").GetComponent<RPGVR_HandCtrl>();

    }

    // Update is called once per frame
    void Update() {
        //if (rightCtrl.isTrigger)
        //{
        //Debug.Log("あたって");
        //    Talk(0);
        //}
        //rightCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SteamVR/[CameraRig]/Controller (right)/RightHand")
        //   .GetComponent<RPGVR_HandCtrl>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("tes");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rightCtrl.isTrigger)
                {
            Debug.Log("あたって");
                Talk(0);
                }
        }
    }

    private void OnTrigger(Collider other)
    {
      
    }

    //話してないならば
    private void Talk(int talklNo)
    {
        if(!talkAudio[talklNo].isPlaying)
        talkAudio[talklNo].PlayOneShot(talkAudio[talklNo].clip);
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



        return false;



    }
    
}
