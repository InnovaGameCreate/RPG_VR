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
    VRTK_TouchpadControl rightPad;
    VRTK_TouchpadControl leftPad;
    MagicTrigger rightMagic;
    MagicTrigger leftMagit;


    VoiceEvent mainEvent;
    protected int talkNum;
    protected bool isTalk;
    public static bool IsMove { set; get; }

    [SerializeField]
    private GameObject uiObj;
    [SerializeField]
    private GameObject yesNoUI;
    public GameObject YesNoUI { set { this.yesNoUI = value; }  get { return yesNoUI; } }
    

    //List<AudioSource> playerAudioList = new List<AudioSource>();

    // Use this for initialization
    protected virtual void Awake()
    {

        talkAudio = GetComponents<AudioSource>();
        IsMove = true;//最初はtrueで
        
        Debug.Log(talkAudio);

    }

    protected virtual void Start()
    {
        rightPad = GameManager.Instance.RIGHTCONTROLLER.GetComponentInChildren<VRTK_TouchpadControl>();
        leftPad = GameManager.Instance.LEFTCONTROLLER.GetComponentInChildren<VRTK_TouchpadControl>();
        rightCtrl = GameManager.Instance.RIGHTHAND.GetComponent<RPGVR_HandCtrl>();
        leftCtrl = GameManager.Instance.LEFTHAND.GetComponent<RPGVR_HandCtrl>();
        
        //rightCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/RightHand").GetComponent<RPGVR_HandCtrl>();
        //leftCtrl = GameObject.Find("ゲームマネージャー（確定）/[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftHand").GetComponent<RPGVR_HandCtrl>();
        //mainEvent = GameObject.Find("VoiceEvent").GetComponent<VoiceEvent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
    }

    //リモコン操作可能かどうかを変更する
    //ボイスイベントの移動制御のせいで
    //trueに強制更新されます・・・
    public void SetInputDevice(bool value)
    {
        rightCtrl.enabled = value;
        leftCtrl.enabled = value;
        rightPad.enabled = value;
        leftPad.enabled = value;
        IsMove = value;
        Debug.Log(IsMove);
        
 
    }
    

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //Debug.Log("あたって");

            //UIが表示されていない
            //      つまり
            //誰とも話していない状態で
            //どっちかのトリガーを引けば
            if (!uiObj.activeSelf && !yesNoUI.activeSelf)
                if (rightCtrl.isTrigger || leftCtrl.isTrigger)
                    isTalk = true;
                else
                    isTalk = false;



        }
    }

    private void OnTriggerExit(Collider other)
    {
        //会話の当たり判定から出ているならば
        //全てのフラグをリセット
        isTalk = false;
        isPlaying = false;
        uiObj.SetActive(false);
        YesNoUI.SetActive(false);
        

    }

    private void OnTrigger(Collider other)
    {

    }

    //引数はenumのセリフ番号を入れる
    public void Talk(int _talklNo)
    {
        //何かの会話が既に再生中でないか捜索
        //再生中であればその時点で関数を抜ける
        for (int i = 0; i < talkNum; i++)
        {
            if (talkAudio[i].isPlaying)
            {
                Debug.Log(i + "on");
                return;
            }

        }

        //再生中フラグを立て再生を開始
        isPlaying = true;
        talkAudio[_talklNo].PlayOneShot(talkAudio[_talklNo].clip);
    }

    public bool IsTalk()
    {
        return isTalk;
    }

    public void Delta()
    {
        Debug.Log(deltaTime);

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
        //経過時間と再生時間比較し
        //再生時間より長くなれば
        if (audio.clip.length <= deltaTime)
        {
            deltaTime = 0;
            isPlaying = false;
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

    public void SetUIObj(bool value)
    {
        uiObj.SetActive(value);
    }

  
}
