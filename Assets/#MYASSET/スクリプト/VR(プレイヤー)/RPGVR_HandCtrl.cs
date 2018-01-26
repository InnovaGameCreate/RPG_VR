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

    public Hands hand = Hands.Right;

    private Transform pointerFinger;//人差し指
    private Transform gripFingers;// 中指・薬指・小指（１つのオブジェクトになっている）


    public StateModel[] stateModels;
    [SerializeField]
    private VRTK_InteractGrab _grabb;
    private Animator handAnimator;

    public int currentState = 100;
    int lastState = -1;

    public bool action = false;
    public bool hold = false;

    private void Start()
    {
        handAnimator = GetComponent<Animator>();

        // --- 個別に定義した処理をコントローラーの操作に紐づける --------------------
        //GetComponent<VRTK_InteractGrab>().GrabButtonPressed += DoGrabOn;
        //GetComponent<VRTK_InteractGrab>().GrabButtonReleased += DoGrabOff;
        _grabb.GrabButtonPressed += DoGrabOn;
        _grabb.GrabButtonReleased += DoGrabOff;
        //GetComponent<VRTK_InteractUse>().UseButtonPressed += DoUseOn;
        //GetComponent<VRTK_InteractUse>().UseButtonReleased += DoUseOff;


    }
    // --- Grabボタンを押したときの処理（中指・薬指・小指を曲げる） ------------------------
    private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
    {
        //Debug.Log("swww");
        //targetGripRotation = maxRotation;
        hold = !hold;
    }

    // --- Grabボタンを離したときの処理（曲げていた中指・薬指・小指をもとに戻す） ----------
    private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
    {
        //targetGripRotation = originalGripRotation;
        //hold = !hold;
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
        handAnimator.SetBool("Action", action);
        handAnimator.SetBool("Hold", hold);
    }
}

