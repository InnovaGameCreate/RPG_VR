using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WaistItem : MonoBehaviour
{

    public bool gripped; //握ったかどうか
    private Transform waist; //腰の位置
    private GameObject waistItem; //腰にあるアイテム
    private GameObject waistmultiItem;
    private Transform itemTrans;
    public WaistItem grippedOther;          //逆のコントローラを握ったかどうか

    void Start()
    {
        waistItem = GameObject.Find("WaistItem");
        if (gameObject.name == "RightController")
            grippedOther = GameObject.Find("[VRTK_Scripts]/LeftController").GetComponent<WaistItem>();
        else
            grippedOther = GameObject.Find("[VRTK_Scripts]/RightController").GetComponent<WaistItem>();
    }
    private void OnEnable()
    {
        if (GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの登録
        GetComponent<VRTK_ControllerEvents>().GripPressed += GripPressedHandler;
        GetComponent<VRTK_ControllerEvents>().GripReleased += GripReleasedHandler;

    }

    private void OnDisable()
    {
        if (GetComponent<VRTK_ControllerEvents>() == null)
            return;
        // イベントハンドラの解除
        GetComponent<VRTK_ControllerEvents>().GripPressed -= GripPressedHandler;
        GetComponent<VRTK_ControllerEvents>().GripReleased -= GripReleasedHandler;

    }

    // イベントハンドラ
    private void GripPressedHandler(object sender, ControllerInteractionEventArgs e)
    {
        gripped = true;
        Instantiate(waistItem,itemTrans);
        waistmultiItem = GameObject.Find("WaistItem");
        Destroy(waistItem);
        //if(/*コントローラーの位置がアイテムの範囲内ならば*/1)
        //{
        //    //Destroy()
        //}
    }

    private void GripReleasedHandler(object sender, ControllerInteractionEventArgs e)
    {
        Instantiate(waistmultiItem);
        gripped = false;

    }

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        waistItem.transform.position= GameObject.Find("[VRTK_Scripts]/SkillZone2").transform.position;
        itemTrans = waistItem.transform;

        if (gripped)
        {

        }

        //waistItem = GameObject.
    }
}
