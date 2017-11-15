using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

public class RPGTouchButton : MonoBehaviour
{
    private Button test;
    private bool isSelected=false;
    public GameObject controller;


    // Use this for initialization
    void Start()
    {
        test = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Enable時
    private void OnEnable()
    {
        if (controller.GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの登録
        controller.GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressedHandler;
        controller.GetComponent<VRTK_ControllerEvents>().TriggerReleased += TriggerReleasedHandler;
    }

    //Disable時
    private void OnDisable()
    {
        if (controller.GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの解除 
        controller.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressedHandler;
        controller.GetComponent<VRTK_ControllerEvents>().TriggerReleased -= TriggerReleasedHandler;
    }

    //コントローラがボタンから離れた時
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "[VRTK][AUTOGEN][Controller][CollidersContainer]")
        {
            Debug.Log("exit");
            isSelected = false;
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerExitHandler);
        }
    }

    //コントローラがボタンに触れた時
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.name == "[VRTK][AUTOGEN][Controller][CollidersContainer]")
        {
            
        }
    }

    //あ
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "[VRTK][AUTOGEN][Controller][CollidersContainer]")
        {
            Debug.Log("enter");
            isSelected = true;
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerEnterHandler);
        }
    }

    private void TriggerPressedHandler(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("trigger");
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerDownHandler);
    }


    private void TriggerReleasedHandler(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("trigger");
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }
}

