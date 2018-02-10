using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
using VRTK.Examples;

public class SceneChangerVR : RPGItemObject
{
    //シーン変更用

    [SerializeField]
    private string SceneName;//移動先シーン名
    [SerializeField]
    private string NextPosName;//移動先の場所名
    private FadeInOut fade;
    private enum UseType
    {
        Grabb,
        Trigger,
        TouchPad,
        Contact
    };

    [SerializeField]
    private UseType _Type;

    private bool _contact;

    //private GameObject rightC, leftC;

    //private void Start()
    //{
    //    rightC = GameManager.Instance.RIGHTCONTROLLER;
    //    leftC = GameManager.Instance.LEFTCONTROLLER;

    //}

    protected override void Update()
    {
        switch (_Type)
        {
            case UseType.Contact:
                break;
            case UseType.Grabb:
                if (Griped)
                {
                    Debug.Log("どあのぶ");
                    SceneChangerFunc();
                }
                break;
            case UseType.TouchPad:
                if (Touched)
                {
                    //Debug.Log("どあのぶ");
                }
                break;
            case UseType.Trigger:
                if (triggerd)
                {
                    //Debug.Log("どあのぶ");
                }
                break;
        }
        
    }


    private void SceneChangerFunc()
    {

        if (fade != null)
        {
            fade.CONDITION = FadeInOut.Condition.FADEIN;
            GameManager.Instance.SceneChengeManager(SceneName, NextPosName);
            fade.CONDITION = FadeInOut.Condition.FADEOUT;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.GetComponentInChildren<FadeInOut>())
        {
            fade = coll.gameObject.GetComponentInChildren<FadeInOut>();
        }

        if (coll.gameObject == rightcontroller || coll.gameObject == leftcontroller)
        {
            _contact = true;
        }
        else
            _contact = false;
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject == rightcontroller || coll.gameObject == leftcontroller)
        {
            _contact = false;
        }
    }

}
