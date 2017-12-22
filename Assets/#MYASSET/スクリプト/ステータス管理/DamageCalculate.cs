using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculate : MonoBehaviour
{
    HumanBase parent;   //HumanBaseインスタンス　    
    bool is_magic;      //魔法攻撃か　剣か
    int _attackPower;     //実際の与ダメ
    List<Buff> _sendBuff;//与バフリスト
    List<Buff> _receiveBuff; //受バフリスト
    Status _status;//ステータス

    private void Start()
    {
        is_magic = gameObject.CompareTag("Brade") ? false : true;
        parent = GetComponentInParent<HumanBase>();
    }
    //攻撃力を取得
    public int AttackPower
    {
        get { return _attackPower; }
    }

    //与バフリストを取得
    public List<Buff> SendBuff
    {
        get { return _sendBuff; }
    }

    //　[コピー]  剣：当たった時に         魔法：発射したときに
    public void copyFromHumanBase()
    {
        _status = parent.Status;
        _sendBuff = parent.SendBuff;
        _receiveBuff = parent.ReceiveBuff;

        _receiveBuff.Add(GetComponent<Buff>());
    }
    ////コンストラクタ
    //public DamageCalculate(Status status, List<Buff> sendBuff, List<Buff> receiveBuff)
    //{
    //    _status = status;
    //    _sendBuff = sendBuff;
    //    _receiveBuff = receiveBuff;
    //    CalculateAttackPower();//ダメージ計算
    //}


    private void OnCollisionEnter(Collision collision)
    {
        //剣のときコピー
        if (!is_magic)
            copyFromHumanBase();

        CalculateAttackPower();
        collision.gameObject.GetComponent<HumanBase>().ReceiveAttack(this);
    }

    //実ダメージ計算
    private void CalculateAttackPower()
    {
        Buff all_receiveBuff = null;
        foreach (Buff s in _receiveBuff)
            all_receiveBuff += s;
        
        Parameters all_parameters = null;
        all_parameters = (_status.Parameter + all_receiveBuff.ParaSingle) * all_receiveBuff.ParaMagnification;


        _attackPower = is_magic ? all_parameters.MAGICATK : all_parameters.ATK;
    }

}
