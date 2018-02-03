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
    public Transform HeadPos;
    public GameObject Head;
    private SearchHand rangeWaistItem;
    private bool exist;
    private GameObject test;
    private bool oneTimeGrab;

    private void Awake()
    {
       // rangeWaistItem = waistItem.GetComponent<SearchHand>();

    }
    void Start()
    {
       
        
        rangeWaistItem = Head.GetComponent<SearchHand>();
       // print(Head.GetComponent <SearchHand>()._SearchItem);
        exist = true;
        waistItem = GameObject.Find("WaistItem");
        //rangeWaistItem = waistItem.GetComponent<SearchHand>();

        if (gameObject.name == "RightController")
            grippedOther = GameManager.Instance.LEFTCONTROLLER.GetComponent<WaistItem>();
        else
            grippedOther = GameManager.Instance.RIGHTCONTROLLER.GetComponent<WaistItem>();
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
       
        //Instantiate(waistItem,itemTrans);
        // waistmultiItem = GameObject.Find("WaistItem");

        //print(rangeWaistItem._SearchItem);
        //if (rangeWaistItem._SearchItem)
        //{//アイテムの範囲内であれば
        //    Destroy(waistItem);
        //    print("アイテム使用");
        //}
       // exist = false;
        //if(/*コントローラーの位置がアイテムの範囲内ならば*/1)
        //{
        //    //Destroy()
        //}
    }

    private void GripReleasedHandler(object sender, ControllerInteractionEventArgs e)
    {
      //  Instantiate(waistmultiItem);
        gripped = false;

    }

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        // waistItem.transform.position= GameObject.Find("[VRTK_Scripts]/SkillZone2").transform.position;
        //  itemTrans = waistItem.transform;
        // print(exist);
        if (/*grippedOther.gripped || */gripped)
        {
            exist = false;
        }
        ////if(exist)
        ////waistItem.transform.position = new Vector3(0.15f + HeadPos.transform.position.x, 1.0f, HeadPos.transform.position.z);

        if (gripped)
        {

        }

        //waistItem = GameObject.
    }
}
