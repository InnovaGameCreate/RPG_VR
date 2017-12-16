﻿using System.Collections;
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

    private void Start()
    {
        //nullなら非戦闘要員
        humanstatus = GetComponent<Status>();
    }

    //攻撃を受けたとき
    public void ReceiveAttack(DamageCalculate d)
    {
        receiveBuff.AddRange(d.SendBuff);//与バフを受け取る
    }

    //受バフの処理
    IEnumerator ApplyReceiveBuff()
    {
        while (true)
        {
            foreach (Buff b in receiveBuff)
            {
                //ステータス変更処理

                //有効時間を減らす
                b.AvailableSeconds--;
            }
            //有効時間が切れた要素の削除
            receiveBuff.RemoveAll(i => i.AvailableSeconds <= 0);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
