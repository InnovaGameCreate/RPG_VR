using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculate
{

    int _attackPower;     //実際の与ダメ
    List<Buff> _sendBuff;//与バフリスト
    List<Buff> _receiveBuff; //受バフリスト
    Status _status;//ステータス

    //攻撃力を取得
    public int AttackPower
    {
        get { return _attackPower; }
    }

    //与バフリストを取得
    public List<Buff> ReceiveBuff
    {
        get { return _receiveBuff; }
    }

    //コンストラクタ
    public DamageCalculate(Status status, List<Buff> sendBuff, List<Buff> receiveBuff)
    {
        _status = status;
        _sendBuff = sendBuff;
        _receiveBuff = receiveBuff;
        CalculateAttackPower();//ダメージ計算
    }

    void CalculateAttackPower()
    {
        //TODO   atksum =  (status + 受バフリスト)でごにょごにょ 
        _attackPower = 100;
    }
}
