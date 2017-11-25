using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class Weapon_Sword : RPGItemObject
{
    /*
        武器クラス
        実際に実装する方
     */

    public float Atk;


    // Use this for initialization
    //protected override void Start () {
    //       base.Start();
    //}

    //Update is called once per frame
    void Update()
    {
        if (triggerd)
        {
            Debug.Log("triggggg");
        }
    }

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //}

    //protected override void OnDisable()
    //{
    //    base.OnDisable();
    //}

    //protected override void Awake()
    //{
    //    base.Awake();
    //}

    /*トリガー*/
    protected override void TriggerPressedHandler2(object sender, ControllerInteractionEventArgs e)//トリガーを押したとき
    {
        triggerd = true;

        
    }

    protected override void TriggerReleasedHandler2(object sender, ControllerInteractionEventArgs e)//トリガーを離したとき
    {
        triggerd = false;


    }
    /*タッチパッド*/
    protected override void TouchPadPressedHandler2(object sender, ControllerInteractionEventArgs e)//タッチパッドを押したとき
    {
        Touched = true;

        Debug.Log("tttt");
    }

    protected override void TouchPadReleasedHandler2(object sender, ControllerInteractionEventArgs e)//タッチパッドを離したとき
    {
        Touched = false;


    }

    /*グリップ*/
    protected override void GripPressedHandler2(object sender, ControllerInteractionEventArgs e)//グリップを押したとき
    {
        Griped = true;
        Debug.Log("ggg");

    }

    protected override void GripReleasedHandler2(object sender, ControllerInteractionEventArgs e)//グリップを離したとき
    {
        Griped = false;

        
    }

    public override void StartUsing(GameObject currentUsingObject)
    {
        Debug.Log("aaaaaaaaaaaaaaaa");
    }
}
