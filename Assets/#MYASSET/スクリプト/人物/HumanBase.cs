using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : MonoBehaviour
{
    //人物スーパークラス
    Status humanstatus;         //ステータス
    bool is_fly;         //飛んでるか   
    BackPack bag;       //持ち物クラス
<<<<<<< HEAD
     //TODO   受バフリスト・与バフリスト　格納用　宣言
=======

    //与バフクラス・受バフクラス格納用リスト宣言
    List<Buff> sendBuff = new List<Buff>();
    List<Buff> receiveBuff = new List<Buff>();

    //TODO   ダメージ計算クラス

>>>>>>> 03edea8ffc0aac187f9a34016c3b51606c5a226d



    private void Start()
    {
        //nullなら非戦闘要員
        humanstatus = GetComponent<Status>();
    }
}
