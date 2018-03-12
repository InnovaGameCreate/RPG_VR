using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, TooltipAttribute("[VRTK_SDKManager]を指定")]
    GameObject vrtkManager;
    [SerializeField, TooltipAttribute("[VRTK_Scripts]を指定")]
    GameObject vrtkScripts;

    //実行後VRTK_ScriptsからVRTK_SDKManagerへ移動するためFindではなくここから呼ぶこと
    [SerializeField, TooltipAttribute("LeftControllerを指定")]
    GameObject LeftController;
    [SerializeField, TooltipAttribute("RightControllerを指定")]
    GameObject RightController;


    [SerializeField, TooltipAttribute("Camera (head)を指定")]
    GameObject Head;

    [SerializeField, TooltipAttribute("プレイヤークラスのHumanPlayerインスタンスを指定")]
    HumanPlayer player;

    [SerializeField, TooltipAttribute("クエストマネージャ インスタンスを指定")]
    QuestManager qManager;

    //次に移るシーンの名前
    private string nextSceneName;
    private FadeInOut fade; //フェードイン・アウト用

    public GameObject VRTKMANAGER
    {
        get { return vrtkManager; }
    }
    public GameObject VRTKSCRIPTS
    {
        get { return vrtkScripts; }
    }
    public GameObject LEFTCONTROLLER
    {
        get { return LeftController; }
    }
    public GameObject RIGHTCONTROLLER
    {
        get { return RightController; }
    }
    public GameObject HEAD
    {
        get { return Head; }
    }
    public HumanPlayer PLAYER
    {
        get { return player; }
    }

    public string NEXTSCENE
    {
        set { nextSceneName = value; }
    }
    public QuestManager QMANAGER
    {
        get { return qManager; }
    }


    bool instanced = false;         //ゲームマネージャがすでに生成されているかどうか
    /// <summary>
    /// ゲーム状態（大枠）
    /// </summary>
    public enum GameState
    {
        Start,
        InGame,
        Result,
        End,
    };



    /// <summary>現在の状態</summary>
    private GameState _CurrentState = GameState.Start;


    //時間関係
    private float ElapsedTime;         //経過時間
    public float ELAPSEDTIME
    {
        get { return ElapsedTime; }
    }
    [SerializeField, TooltipAttribute("制限時間")]
    private float LimitTime;    //制限(限界)時間
    public float LIMITTIME
    {
        get { return LimitTime; }
    }

    private const int CountDownNum = 3; //スタート時のカウントダウン数
    public int COUNTDOWN
    {
        get { return CountDownNum; }
    }

    public override void Awake()
    {
        base.Awake();
        if (GameObject.Find("ゲームマネージャー（確定）"))
            Destroy(this.gameObject);
        else
            name = "ゲームマネージャー（確定）";
    }
    /// <summary>
    /// 開始関数
    /// </summary>
    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        fade = gameObject.GetComponentInChildren<FadeInOut>();
    
    }

    //シーンチェンジ後のスタート位置を決定
    void setStartPosition()
    {
        Transform startpos = GameObject.Find(nextSceneName).transform;
        Transform obj = VRTKMANAGER.transform.Find("SDKSetups/SteamVR/[CameraRig]");
        obj.position = startpos.position;
        obj.rotation = Quaternion.Euler(new Vector3(0, startpos.rotation.y,0));
            ;

    }

    //移動可能か移動不可能シーンか調べて処理
    void setMovable(Scene nextScene)
    {

        //移動不可能シーンの時
        if (nextScene.name == "魔法攻撃")
        {
            LeftController.GetComponentInChildren<VRTK_TouchpadControl>().enabled = false;
            RightController.GetComponentInChildren<VRTK_TouchpadControl>().enabled = false;
        }
        //移動可能シーンの時
        else
        {
            LeftController.GetComponentInChildren<VRTK_TouchpadControl>().enabled = true;
            RightController.GetComponentInChildren<VRTK_TouchpadControl>().enabled = true;
        }
    }
    //シーンチェンジ時に呼ばれる
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        setStartPosition();
        setMovable(nextScene);
    }

    private void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("魔法攻撃");
        if (Input.GetKeyDown(KeyCode.H))
            SceneManager.LoadScene("1月26日統合");
        if(fade !=null)
        if (fade.CONDITION == FadeInOut.Condition.SCENECHANGE) 
        {
            fade.CONDITION = FadeInOut.Condition.FADEIN;
            SceneManager.LoadScene(nextSceneName);          
        }

    }
    /// <summary>
    /// 更新関数
    /// </summary>
    private void FixedUpdate()
    {
        ElapsedTime += Time.deltaTime;

        switch (_CurrentState)
        {
            case GameState.Start:
                updateStart();
                break;

            case GameState.InGame:
                updateInGame();
                break;

            case GameState.End:
                updateEnd();
                break;
        }
    }

    /// <summary>
    /// ゲーム開始前の更新処理
    /// </summary>
    private void updateStart()
    {
        //Debug.Log("カウントダウン:" + (int)-(ElapsedTime - 1));
        if (ElapsedTime >= 0)
        {
            _CurrentState = GameState.InGame;
            //Debug.Log("ゲームスタート");
        }

    }

    /// <summary>
    /// インゲーム中の更新処理
    /// </summary>
    private void updateInGame()
    {
        //  Debug.Log(ELAPSEDTIME);
        //時間切れになったら終了
        if (ELAPSEDTIME > LIMITTIME)
        {
            _CurrentState = GameState.End;
            //Debug.Log("時間切れ");
        }

    }

    /// <summary>
    /// ゲーム終了時の更新処理
    /// </summary>
    private void updateEnd()
    {
        //Debug.Log("終了です");

    }


    bool IsGameEnd() { return _CurrentState == GameState.End; }

    public void SceneChengeManager(string NextSecneName, string PosName)
    {

        nextSceneName = PosName;
        fade.CONDITION = FadeInOut.Condition.FADEOUT;
        SceneManager.LoadScene(NextSecneName);
        //fade.CONDITION = FadeInOut.Condition.FADEIN;
    }
}

