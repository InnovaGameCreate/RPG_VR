using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    
    private HumanPlayer savedPlayer;        //プレイヤーのインスタンス保存
    public HumanPlayer SAVEDPLAYER
    {
        get { return savedPlayer; }
        set { savedPlayer = value; }
    }

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

    /// <summary>
    /// 開始関数
    /// </summary>
    private void Start()
    {
        _CurrentState = GameState.Start;
        SAVEDPLAYER = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/プレイヤークラス").GetComponent<HumanPlayer>();
    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("魔法攻撃");
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
}

