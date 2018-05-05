using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //TODO アイテム・装備使用管理系
  //  [SerializeField, TooltipAttribute("アイテム管理UIコンポーネント指定")]
  //  private UILogController itemUIController;

    [SerializeField, TooltipAttribute("LOG管理コンポーネント指定")]
    private UILogController logController;
    [SerializeField]
    private GameObject inventoryUI;
    private RPGVR_HandCtrl rightHand;//インベントリ表示フラグ
    

    private void Start()
    {
        rightHand = GameManager.Instance.RIGHTHAND.GetComponent<RPGVR_HandCtrl>();
    }

    private void Update()
    {
        InventoryDisplay();
    }

    public UILogController LOGCONTROLLER
    {
        get { return logController; }
    }

    //インベントリの表示処理
    public void InventoryDisplay()
    {
        //右手のメニューボタンを押すと呼ばれる（離すと条件外）
        if (rightHand.isMenuButtonDown)
        {
            //押すたびに表示、非表示を繰り返す
            inventoryUI.SetActive(rightHand.isMenuButton);
        }
    }


}
