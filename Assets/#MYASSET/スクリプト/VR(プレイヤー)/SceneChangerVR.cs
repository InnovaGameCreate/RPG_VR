using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
using VRTK.Examples;

public class SceneChangerVR : MonoBehaviour
{
    //シーン変更用

    [SerializeField]
    private string SceneName;//移動先シーン名

    private enum UseType
    {
        Grabb,
        Trigger,
        TouchPad,
        Contact
    };

    [SerializeField]
    private UseType _Type;

    private GameObject rightC, leftC;

    private void Start()
    {
        rightC = GameManager.Instance.RIGHTCONTROLLER;
        leftC = GameManager.Instance.LEFTCONTROLLER;

    }

    private void OnEnable()
    {
        if (rightC.GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの登録
        else
        {
            rightC.GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressedHandler;
            rightC.GetComponent<VRTK_ControllerEvents>().TriggerReleased += TriggerReleasedHandler;
        }

        if (leftC.GetComponent<VRTK_ControllerEvents>() == null)
            return;
        else
        {
            leftC.GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressedHandler;
            leftC.GetComponent<VRTK_ControllerEvents>().TriggerReleased += TriggerReleasedHandler;
        }

    }
    private void OnDisable()
    {
        if (rightC.GetComponent<VRTK_ControllerEvents>() == null)
            return;
        else
        {
            rightC.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressedHandler;
            rightC.GetComponent<VRTK_ControllerEvents>().TriggerReleased -= TriggerReleasedHandler;
        }

        if (leftC.GetComponent<VRTK_ControllerEvents>() == null)
            return;
        else
        {
            leftC.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressedHandler;
            leftC.GetComponent<VRTK_ControllerEvents>().TriggerReleased -= TriggerReleasedHandler;
        }
    }

    private void SceneChangerFunc()
    {
        SceneManager.LoadScene(SceneName);
    }

    //private void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.gameObject.GetComponent<VRTK_InteractableObject>() != null)
    //    {
    //        VRTK_InteractableObject grabbobj = coll.gameObject.GetComponent<VRTK_InteractableObject>();
    //        Debug.Log("hgdeioghigihglihlifdlikfdlih");
    //        if (_Type == UseType.Contact)
    //        {
    //            Debug.Log("ふれた");
    //        }
    //        else if (_Type == UseType.Grabb && grabbobj.Grabbed)
    //        {
    //            Debug.Log("にぎった");
    //        }
    //        else if (_Type == UseType.Trigger && triggerd)
    //        {
    //            Debug.Log("ひいた");
    //        }
    //        else if (_Type == UseType.TouchPad && Touched)
    //        {
    //            Debug.Log("たっち");
    //        }
    //    }
    //}

    private void TriggerPressedHandler(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("ひいた");
    }

    private void TriggerReleasedHandler(object sender, ControllerInteractionEventArgs e)
    {
    }
}
