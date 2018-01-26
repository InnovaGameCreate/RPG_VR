using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculate
{
    HumanBase parent;   //HumanBaseインスタンス　    
    private bool is_magic;//trueなら魔法攻撃
    int _attackPower;     //実際の与ダメ
    List<Buff> _sendBuff;//与バフリスト
    List<Buff> _receiveBuff; //受バフリスト
    Status _status;//ステータス

    //private void Start()
    //{
    //    is_magic = GetComponent<Weapon>() != null ? false : true;
    //    parent = transform.root.GetComponent<HumanBase>();
    //    copyFromHumanBase();
    //}
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
        //Buff receive_send = GetComponent<Buff>();        //0番目が受バフ
        Buff receive_send = new Buff();
        _status = parent.Status;
        _sendBuff = parent.SendBuff;
        _receiveBuff = parent.ReceiveBuff;

        _receiveBuff.Add(receive_send);
        // _sendBuff.Add(receive_send[1]);

        //TODO [問題] parent.Status以降が参照できない　null


        CalculateAttackPower();

    }
    //コンストラクタ
    public DamageCalculate(Status status, bool is_MAGIC,List<Buff> sendBuff = null, List<Buff> receiveBuff = null)
    {
        is_magic = is_MAGIC;
        _status = status;
        if (_sendBuff != null)
            _sendBuff = sendBuff;
        if(receiveBuff != null)
            _receiveBuff = receiveBuff;
        CalculateAttackPower();//ダメージ計算
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    //剣のときコピー
    //    if (!is_magic)
    //        copyFromHumanBase();
    //    collision.gameObject.GetComponent<HumanBase>().ReceiveAttack(this);
    //}

    //実ダメージ計算
    private void CalculateAttackPower()
    {
        Buff all_receiveBuff = new Buff();
        Parameters all_parameters = new Parameters();
        
        if (_receiveBuff != null)
        {
            all_receiveBuff = _receiveBuff[0];
            foreach (Buff s in _receiveBuff)
            {
                if (s == _receiveBuff[0])
                    continue;
                all_receiveBuff = s + all_receiveBuff;
            }
            all_parameters = (_status.Parameter + all_receiveBuff) * all_receiveBuff;
        }
        else
            all_parameters = _status.Parameter;
        //Debug.Log("MATK" + all_receiveBuff.MAGICATK);
        _attackPower = is_magic ? all_parameters.MAGICATK : all_parameters.ATK;
        
        //Debug.Log("ALLATK"+_attackPower);

    }

}
