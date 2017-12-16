using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : MonoBehaviour {
    //人物スーパークラス
    Status humanstatus;         //ステータス
    bool is_fly;         //飛んでるか   
    BackPack bag;       //持ち物クラス
     //TODO   受バフリスト・与バフリスト　格納用　宣言



    private void Start()
    {
        //nullなら非戦闘要員
        humanstatus = GetComponent<Status>();
    }
}
