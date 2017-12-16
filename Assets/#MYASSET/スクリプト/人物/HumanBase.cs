using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanBase : MonoBehaviour
{
    //人物スーパークラス
    Status humanstatus;         //ステータス
    bool is_fly;         //飛んでるか   
    BackPack bag;       //持ち物クラス

    //受バフリスト・与バフリスト　格納用　宣言
    List<Buff> sendBuff = new List<Buff>();
    List<Buff> receiveBuff = new List<Buff>();

    //TODO   ダメージ計算クラス

    public delegate void Damaged(DamageCalculate dm);
    public event Damaged damageEvent;           //ダメージイベント

    private void Start()
    {
        //nullなら非戦闘要員
        humanstatus = GetComponent<Status>();
        damageEvent += damaged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") || other.CompareTag("FlyAttack"))
            damageEvent(other.GetComponent<DamageCalculate>());
    }

    //ダメージを受けた時の処理　 自分のステータス・バフリストを考慮して計算
    void damaged(DamageCalculate dm)
    {

    }
}
