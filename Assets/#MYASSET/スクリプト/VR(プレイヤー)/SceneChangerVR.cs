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
        SceneManager.LoadScene(SceneName);
    }

    private void OnTriggerEnter(Collider coll)
    {
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
