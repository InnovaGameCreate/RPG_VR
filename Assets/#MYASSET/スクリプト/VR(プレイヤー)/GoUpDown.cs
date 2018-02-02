using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GoUpDown : MonoBehaviour
{
    //上昇下降関連
    [Tooltip("上昇or下降速度")]
    public float risingspeed = 1;
    [SerializeField]
    public bool flymode;  //飛行状態かどうか
    [SerializeField]
    public bool undertofly;                    //地面から空中へ強制移動中か

    private float[] axisx = new float[2];       //左右コントローラの触れたタッチパッドのx位置
    private VRTK_ControllerEvents[] events = new VRTK_ControllerEvents[2];      //左右コントローラコンポーネント
    private int checkcontroler = -1;            //タッチパッドに触れた状態でタッチパッドが押されたコントローラ
    protected bool touchpadTouched;             //タッチパッドを押したかどうか
   


    //飛行モードアクション関連
    private const float flystartrange_y = 0.05f;                   //必要な羽ばたき幅（0.1秒間における一方向への)
    private const int samplenum = 30;                            //キャリブレーション数
    private const float startflyupspeed = 1;              //はじめに上昇する際の速度
    private List<float> precontrollerposL = new List<float>();   //過去の左コントローラの位置
    private List<float> precontrollerposR = new List<float>();   //過去の右コントローラの位置

    private Transform[] controllerpos = new Transform[2];        //左右コントローラの位置
    private Checkmode[] checkmode = { Checkmode.NONE, Checkmode.NONE };   //コントローラーの移動チェックモード
    private const int needcheckcount = 3;                     //必要な一方向のみの連続移動カウント数
    private int[] checkcount = new int[2];                         //一方向のみの連続移動カウント数
    private int[] updownsetcount = new int[2];                     //上下に連続移動した合計数 現状最適値6以上
    private bool firstcheckpos;                     //controllerposが全部埋まったかどうか

    private enum Checkmode
    {
        UP,
        DOWN,
        NONE
    }
    private void Start()
    {
        StartCoroutine("checkFlyStart");

    }

    //リスト最前線から上下遷移モード把握
    private void setFirstMoveMode()
    {
        //左コントローラー　
        if (Mathf.Abs(precontrollerposL[0] - precontrollerposL[1]) > flystartrange_y)
        {
            if (precontrollerposL[0] - precontrollerposL[1] > flystartrange_y)
                checkmode[0] = Checkmode.DOWN;
            else if (precontrollerposL[0] - precontrollerposL[1] < flystartrange_y)
                checkmode[0] = Checkmode.UP;
        }
        else
            checkmode[0] = Checkmode.NONE;

        //右コントローラー　
        if (Mathf.Abs(precontrollerposR[0] - precontrollerposR[1]) > flystartrange_y)
        {
            if (precontrollerposR[0] - precontrollerposR[1] > flystartrange_y)
                checkmode[1] = Checkmode.DOWN;
            else if (precontrollerposR[0] - precontrollerposR[1] < flystartrange_y)
                checkmode[1] = Checkmode.UP;
        }
        else
            checkmode[1] = Checkmode.NONE;

    }

    //0,1秒間における コントローラy軸移動が一方向へ何回連続してるか調べる。以後上下方向反転の繰り返し
    private void checkUpOrDownSum()
    {
        for (int i = 0; i < updownsetcount.Length; i++)
        {
            updownsetcount[i] = 0;
        }
        //左コントローラ
        for (int i = 0; i < precontrollerposL.Count - 1; i++)
        {
        
            switch (checkmode[0])
            {
                case Checkmode.UP:
                    if (precontrollerposL[i] - precontrollerposL[i + 1] < flystartrange_y)
                        checkcount[0]++;
                    break;
                case Checkmode.DOWN:
                    if (precontrollerposL[i] - precontrollerposL[i + 1] > flystartrange_y)
                        checkcount[0]++;
                    break;
                default:
                    break;
            }
            if (checkcount[0] > needcheckcount)
            {
                checkcount[0] = 0;
                switch (checkmode[0])
                {
                    case Checkmode.UP:
                        checkmode[0] = Checkmode.DOWN;
                        break;
                    case Checkmode.DOWN:
                        checkmode[0] = Checkmode.UP;
                        break;
                    default:
                        break;
                }
                updownsetcount[0]++;
            }
        }
        //右コントローラ
        for (int i = 0; i < precontrollerposR.Count - 1; i++)
        {
          
            switch (checkmode[1])
            {
                case Checkmode.UP:
                    if (precontrollerposR[i] - precontrollerposR[i + 1] < flystartrange_y)
                        checkcount[1]++;
                    break;
                case Checkmode.DOWN:
                    if (precontrollerposR[i] - precontrollerposR[i + 1] > flystartrange_y)
                        checkcount[1]++;
                    break;
                default:
                    break;
            }
            if (checkcount[1] > needcheckcount)
            {
                checkcount[1] = 0;
                switch (checkmode[1])
                {
                    case Checkmode.UP:
                        checkmode[1] = Checkmode.DOWN;
                        break;
                    case Checkmode.DOWN:
                        checkmode[1] = Checkmode.UP;
                        break;
                    default:
                        break;
                }
                updownsetcount[1]++;
            }
        }
        
    }
    //コントローラのy軸における上下遷移を調べる
    private void changeSimple()
    {
        //初期値格納終了
        if (!firstcheckpos && precontrollerposL.Count > samplenum && precontrollerposR.Count > samplenum)
            firstcheckpos = true;

        if (firstcheckpos)
        {
            setFirstMoveMode();
            checkUpOrDownSum();

            if (updownsetcount[0] > 4)
                Debug.Log(updownsetcount[0]);
            if (updownsetcount[1] > 4)
                Debug.Log(updownsetcount[1]);
            if (updownsetcount[0] > 4 && updownsetcount[1] > 4)
                Debug.Log("FLYSTART!!!");
        }

    }

    //強制的に一定値上昇する
    private void startFlyMode()
    {
        if (undertofly)
            transform.Translate(0, startflyupspeed * Time.deltaTime, 0);

    }
    //飛行事前動作を始めたかどうかを調べる
    IEnumerator checkFlyStart()
    {
        while (true)
        {
            if (!firstcheckpos)
            {
                precontrollerposL.Add(controllerpos[0].position.y);
                precontrollerposR.Add(controllerpos[1].position.y);
            }

            else
            {
                precontrollerposL.Add(controllerpos[0].position.y);
                precontrollerposL.RemoveAt(0);
                precontrollerposR.Add(controllerpos[1].position.y);
                precontrollerposR.RemoveAt(1);
            }

            if (!undertofly && updownsetcount[0] > 4 && updownsetcount[1] > 4)
            {
                undertofly = true;
                GetComponent<Rigidbody>().useGravity = false;
            }
          

            changeSimple();
            yield return new WaitForSeconds(0.1f);  //毎ループここで0.1秒停止する
        }
    }
    //上昇下降
    void flyMove()
    {
    
        //押されたタッチパッドが左右のどちらか
        for (int i = 0; i < axisx.Length; i++)
        {
            if (axisx[i] > 0.2f || axisx[i] < -0.2f)
            {
                checkcontroler = i;
                break;
            }
            if (i == axisx.Length - 1)
                checkcontroler = -1;
        }

        //上昇または下降
        if (checkcontroler != -1)
        {
            if (axisx[checkcontroler] > 0)
                transform.Translate(0, risingspeed * Time.deltaTime, 0);
            else if (axisx[checkcontroler] < 0)
                transform.Translate(0, -risingspeed * Time.deltaTime, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (flymode)
            flyMove();
        else
            startFlyMode();
       
       
    }

    //AwakeのほうがStartより早い 
    protected virtual void Awake()
    {
        controllerpos[0] = GameManager.Instance.VRTKSCRIPTS.transform.Find("LeftController");
        controllerpos[1] = GameManager.Instance.VRTKSCRIPTS.transform.Find("RightController");
        events[0] = controllerpos[0].GetComponent<VRTK_ControllerEvents>();
        events[1] = controllerpos[1].GetComponent<VRTK_ControllerEvents>();
        /*
        events[0] = GameObject.Find("[VRTK_Scripts]/LeftController").GetComponent<VRTK_ControllerEvents>();
        events[1] = GameObject.Find("[VRTK_Scripts]/RightController").GetComponent<VRTK_ControllerEvents>();
        */
    }

    //イベントハンドラ割当
    void OnEnable()
    {
        for (int i = 0; i < events.Length; i++)
        {
            events[i].TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadClicked);
            events[i].TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadUnclicked);
            events[i].TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouched);
            events[i].TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadUntouched);
            events[i].TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < events.Length; i++)
        {
            events[i].TouchpadPressed -= new ControllerInteractionEventHandler(DoTouchpadClicked);
            events[i].TouchpadReleased -= new ControllerInteractionEventHandler(DoTouchpadUnclicked);
            events[i].TouchpadTouchStart -= new ControllerInteractionEventHandler(DoTouchpadTouched);
            events[i].TouchpadTouchEnd -= new ControllerInteractionEventHandler(DoTouchpadUntouched);
            events[i].TouchpadAxisChanged -= new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
        }

    }

    //振動
    protected virtual void AttemptHapticPulse(float strength)
    {
        if (events[0])
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(events[0].gameObject), strength);
        }
    }

    //タッチパッド押した
    protected virtual void DoTouchpadClicked(object sender, ControllerInteractionEventArgs e)
    {
        //for (int i = 0; i < events.Length; i++)
        //{
        //    if (sender == events[i])
        //        axisx[i] = e.touchpadAxis.x;
        //}
        if (sender == events[0])
                axisx[0] = e.touchpadAxis.x;
    }
    //タッチパッド離した
    protected virtual void DoTouchpadUnclicked(object sender, ControllerInteractionEventArgs e)
    {
        //for (int i = 0; i < events.Length; i++)
        //{
        //    if (sender == events[i])
        //        axisx[i] = 0;
        //}
        if (sender == events[0])
            axisx[0] = 0;
    }
    //タッチパッド触れた
    protected virtual void DoTouchpadTouched(object sender, ControllerInteractionEventArgs e)
    {
        touchpadTouched = true;

    }
    //タッチパッド触れなくなった
    protected virtual void DoTouchpadUntouched(object sender, ControllerInteractionEventArgs e)
    {
        touchpadTouched = false;
    }

    protected virtual void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        if (touchpadTouched)
        {

        }
    }

    protected virtual float CalculateAngle(ControllerInteractionEventArgs e)
    {
        return 360 - e.touchpadAngle;
    }
}
