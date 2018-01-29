using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK.Examples;

public class SceneChangerVR : RPGItemObject
{
    //シーン変更用

    [SerializeField]
    private string SceneName;//移動先シーン名

    private enum UseType{
        Grabb,
        Trigger,
        TouchPad,
        Contact
    };

    [SerializeField]
    private UseType _Type;

    private void SceneChangerFunc()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "ChengeTrigger" )
        {
            Debug.Log("hgdeioghigihglihlifdlikfdlih");
            if (_Type == UseType.Contact)
            {
                Debug.Log("ふれた");
            }
            else if (_Type == UseType.Grabb && Griped)
            {
                Debug.Log("にぎった");
            }
            else if (_Type == UseType.Trigger && triggerd)
            {
                Debug.Log("ひいた");
            }
            else if (_Type == UseType.TouchPad && Touched)
            {
                Debug.Log("たっち");
            }
        }
    }

}
