using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //TODO アイテム・装備使用管理系
  //  [SerializeField, TooltipAttribute("アイテム管理UIコンポーネント指定")]
  //  private UILogController itemUIController;

    [SerializeField, TooltipAttribute("LOG管理コンポーネント指定")]
    private UILogController logController;

    public UILogController LOGCONTROLLER
    {
        get { return logController; }
    }
}
