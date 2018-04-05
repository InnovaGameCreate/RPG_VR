using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            triggerdother = GameManager.Instance.LEFTCONTROLLER.GetComponent<MagicTrigger>();
        else
            triggerdother = GameManager.Instance.RIGHTCONTROLLER.GetComponent<MagicTrigger>();
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
            eye = GameManager.Instance.VRTKMANAGER.transform.Find("SDKSetups/SteamVR/[CameraRig]/Camera (eye)");

        //両コントローラ魔法
        if (triggerdother.triggerd)
        {
            if (magicpattern[2].GetComponent<MagicPattern>().USEMP < GameManager.Instance.PLAYER.Status.Parameter.MP)
            {
                magicpatternobj = Instantiate(magicpattern[2], eye.position + eye.forward * 0.5f, eye.rotation);
                multitriggerd = true;
            }
        }

        //右コントローラ魔法   左コントローラ魔法
        else
        {

            if (gameObject.name == "RightController")
            {
                if (magicpattern[1].GetComponent<MagicPattern>().USEMP < GameManager.Instance.PLAYER.Status.Parameter.MP)
                    magicpatternobj = Instantiate(magicpattern[1], eye.position + eye.forward * 0.5f, eye.rotation);
            }
            else
            {
                if (magicpattern[1].GetComponent<MagicPattern>().USEMP < GameManager.Instance.PLAYER.Status.Parameter.MP)
                    magicpatternobj = Instantiate(magicpattern[0], eye.position + eye.forward * 0.5f, eye.rotation);
            }
        }
    }

    private void TriggerReleasedHandler(object sender, ControllerInteractionEventArgs e)
    {
        triggerd = false;
        if (magicpattern != null && !magicpatternobj.GetComponent<MagicPattern>().startmagic)
            Destroy(magicpatternobj);

    }

    //魔法が使える状態かどうか判定
    public bool IsMagicTrigger()
    {
        //チュートリアルかフィールドにいれば使える
        return SceneManager.GetActiveScene().name == "チュートリアル試し" 
            || SceneManager.GetActiveScene().name == "フィールド";

    }
}
