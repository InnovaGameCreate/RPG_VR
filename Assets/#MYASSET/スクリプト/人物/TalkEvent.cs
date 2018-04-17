using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TalkEvent : MonoBehaviour
{

    float deltaTime;
    protected bool isPlaying;
    protected AudioSource[] talkAudio;
    RPGVR_HandCtrl rightCtrl;
    RPGVR_HandCtrl leftCtrl;
    VoiceEvent mainEvent;
    protected int talkNum;
    protected bool isTalk;

    [SerializeField]
    private GameObject uiObj;

    //List<AudioSource> playerAudioList = new List<AudioSource>();

    // Use this for initialization
   protected virtual void Awake()
    {
        talkAudio = GetComponents<AudioSource>();

        
        Debug.Log(talkAudio);
    }

    protected virtual void Start()
    {
        Debug.Log(rightCtrl || leftCtrl);

        rightCtrl = GameManager.Instance.RIGHTHAND.GetComponent<RPGVR_HandCtrl>();
        leftCtrl = GameManager.Instance.LEFTHAND.GetComponent<RPGVR_HandCtrl>();
        //rightCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/RightHand").GetComponent<RPGVR_HandCtrl>();
        //leftCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftHand").GetComponent<RPGVR_HandCtrl>();
        //mainEvent = GameObject.Find("VoiceEvent").GetComponent<VoiceEvent>();
        Debug.Log(rightCtrl || leftCtrl);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("あたって");

            //UIが表示されていないつまり誰とも話していない状態で
            //どっちかのトリガーを引けば
           
            if (!uiObj.activeSelf)
                if (rightCtrl.isTrigger || leftCtrl.isTrigger)
                {
                    //Debug.Log("あたって);
                    //Talk(talkNo);
                    isTalk = true;
                }
                else
                    isTalk = false;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTalk = false;
        isPlaying = false;
        uiObj.SetActive(false);

    }

    private void OnTrigger(Collider other)
    {

    }

    //話してないならば
    public void Talk(int _talklNo)
    {

        for (int i = 0; i < talkNum; i++)
        {
            if (talkAudio[i].isPlaying)
                return;

        }
        isPlaying = true;
        talkAudio[_talklNo].PlayOneShot(talkAudio[_talklNo].clip);
    }

    public bool IsTalk()
    {
        return isTalk;
    }

    //再生終了判定
    public bool IsFinished(AudioSource audio)
    {
        //まずその音声が再生中でないならここで止める
        if (!audio.isPlaying)
            return false;

        //今のところこれだとうまくいく・・・？
        if (isPlaying)
            deltaTime += Time.deltaTime;
        else
            deltaTime = 0;
        if (audio.clip.length <= deltaTime)
        {
            deltaTime = 0;
            isPlaying = false;
            Debug.Log("しゅうりょう");
            return true;
        }




        return false;

    }

    public AudioSource TalkAudioSource(int no)
    {
        return talkAudio[no];
    }

    //選択画面表示
    public void PrintSelectUI()
    {
        uiObj.SetActive(true);

    }

    public void DestroyUIObj()
    {
        uiObj.SetActive(false);
    }

  
}
