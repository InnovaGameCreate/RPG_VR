using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MagicTrigger : MonoBehaviour
{
    public GameObject[] magicpattern = new GameObject[3];
    private Transform eye;
    private GameObject magicpatternobj;         //生成した魔法陣形
    public bool triggerd;          //トリガーを引いたかどうか
    private bool multitriggerd;
    private MagicTrigger triggerdother;          //逆のコントローラのトリガーを引いたかどうか
    private void Start()
    {
        if (gameObject.name == "RightController")
            triggerdother = GameObject.Find("[VRTK_Scripts]/LeftController").GetComponent<MagicTrigger>();
        else
            triggerdother = GameObject.Find("[VRTK_Scripts]/RightController").GetComponent<MagicTrigger>();
    }
    private void OnEnable()
    {
        if (GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの登録
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressedHandler;
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += TriggerReleasedHandler;
    }
    private void OnDisable()
    {
        if (GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの解除 
        GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressedHandler;
        GetComponent<VRTK_ControllerEvents>().TriggerReleased -= TriggerReleasedHandler;
    }
    private void Update()
    {
        if (triggerd && triggerdother.triggerd && magicpatternobj!=null && !multitriggerd)
            Destroy(magicpatternobj);

        if(!triggerd && !triggerdother.triggerd)
            multitriggerd = false;
    }
    // イベントハンドラ
    private void TriggerPressedHandler(object sender, ControllerInteractionEventArgs e)
    {
        triggerd = true;

        if (eye == null)
            eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (eye)").transform;

        //リョウコントローラ魔法
        if (triggerdother.triggerd)
        {
            magicpatternobj = Instantiate(magicpattern[2], eye.position + eye.forward * 0.5f, eye.rotation);
            multitriggerd = true;
        }

        //右コントローラ魔法
        else
        {
            if (gameObject.name == "RightController")
                magicpatternobj = Instantiate(magicpattern[1], eye.position + eye.forward * 0.5f, eye.rotation);
            //左コントローラ魔法
            else
                magicpatternobj = Instantiate(magicpattern[0], eye.position + eye.forward * 0.5f, eye.rotation);
        }
    }

    private void TriggerReleasedHandler(object sender, ControllerInteractionEventArgs e)
    {
        triggerd = false;
        if (magicpattern != null && !magicpatternobj.GetComponent<MagicPattern>().startmagic)
            Destroy(magicpatternobj);

    }
}
