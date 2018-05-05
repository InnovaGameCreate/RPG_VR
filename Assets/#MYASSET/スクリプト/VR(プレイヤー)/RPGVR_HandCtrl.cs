using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RPGVR_HandCtrl : MonoBehaviour
{
    public enum Hands
    {
        Right,
        Left
    }

    public Hands hand;
    private Transform pointerFinger;//人差し指
    private Transform gripFingers;// 中指・薬指・小指（１つのオブジェクトになっている）
    public MagicTrigger magicTrigger;

    public StateModel[] stateModels;
    [SerializeField]
    private VRTK_InteractGrab _grabb;
    [SerializeField]
    private VRTK_ControllerEvents ctrl;
    private Animator handAnimator;
    public SwordMotion swordMotion;


    public int currentState = 100;
    int lastState = -1;

    public bool isTrigger;
    public bool action = false;
    public bool hold = false;
    public bool isMagic = false;
    public bool isMenuButtonDown = false;//メニューボタンを押した瞬間
    public bool isMenuButton = false;//メニューボタンを押すたびに状態が反転
    private GameObject test;
    private void Start()
    {
        
        handAnimator = GetComponent<Animator>();

        // --- 個別に定義した処理をコントローラーの操作に紐づける --------------------
        //GetComponent<VRTK_InteractGrab>().GrabButtonPressed += DoGrabOn;
        //GetComponent<VRTK_InteractGrab>().GrabButtonReleased += DoGrabOff;
        _grabb.GrabButtonPressed += DoGrabOn;
        _grabb.GrabButtonReleased += DoGrabOff;





        //メニューボタンを押した場合のイベント
        //StartMenuでもなければButtonOneでもなく下記のButtonTwoです
        ctrl.ButtonTwoPressed += MenuPressed;
        ctrl.ButtonTwoReleased += MenuReleased;

        ctrl.TriggerPressed += TriggerPressed;
        ctrl.TriggerReleased += TriggerReleased;

        

        //GetComponent<VRTK_InteractUse>().UseButtonPressed += DoUseOn;
        //GetComponent<VRTK_InteractUse>().UseButtonReleased += DoUseOff;


    }

    // ----トリガーを押したときの処理
    private void TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        isTrigger = true;
        Debug.Log("トリガーダウン");
    }

    //　---トリガーを離したときの処理
    private void TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        isTrigger = false;
        Debug.Log("トリガーアップ");

    }


    private void MenuPressed(object sender, ControllerInteractionEventArgs e)
    {
        isMenuButtonDown = true;
        isMenuButton = !isMenuButton;//押すたびに状態が反転する
        Debug.Log("メニューボタンダウン");


    }
    private void MenuReleased(object sender, ControllerInteractionEventArgs e)
    {
        isMenuButtonDown = false;
        Debug.Log("メニューボタンアップ");

    }

    // --- Grabボタンを押したときの処理（中指・薬指・小指を曲げる） ------------------------
    private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
    {

        //Debug.Log("swww");
        //targetGripRotation = maxRotation;
        hold = true;
    }

    // --- Grabボタンを離したときの処理（曲げていた中指・薬指・小指をもとに戻す） ----------
    private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
    {
        // targetGripRotation = originalGripRotation;
        if (hand == Hands.Right)
        {
            if (!swordMotion.IsEquip)//装備中でないなら
                hold = false;

        }
        
        if(hand == Hands.Left)
        {
            hold = false;
        }
      
    }

    

    // --- Useボタンを押したときの処理（人差し指を曲げる） ------------------------
    //private void DoUseOn(object sender, ControllerInteractionEventArgs e)
    //{
    //    //targetPointerRotation = maxRotation;
    //    action = true;
    //}

    //// --- Useボタンを離したときの処理（曲げていた人差し指をもとに戻す） ----------
    //private void DoUseOff(object sender, ControllerInteractionEventArgs e)
    //{
    //    //targetPointerRotation = originalPointerRotation;
    //    action = false;
    //}

    private void Update()
    {
        if (hand == Hands.Right)
        {
            handAnimator.SetBool("Equip", swordMotion.IsEquip);
            if (swordMotion.IsEquip)//装備中
                hold = true;
        }

        if (hand == Hands.Left)
        {

            isMagic = magicTrigger.magicpattern[1].GetComponent<MagicPattern>()._magic;
            if (isMagic)
                handAnimator.SetTrigger("Magic");


        }
        
        handAnimator.SetBool("Action", action);
        handAnimator.SetBool("Hold", hold);
    }
    
}

