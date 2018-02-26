using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculate
{
    //HumanBase parent;   //HumanBaseインスタンス　    
    private bool is_magic;//trueなら魔法攻撃
    int _attackPower;     //実際の与ダメ
    List<Buff> _sendBuff = new List<Buff>();//与バフリスト
    List<Buff> _receiveBuff = new List<Buff>(); //受バフリスト
    Status _status;//ステータス


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

    //コンストラクタ
    public DamageCalculate(Status status,int BasisPower, bool is_MAGIC,List<Buff> sendBuff, List<Buff> receiveBuff)
    {
        is_magic = is_MAGIC;
        _status = status;
        
        if (_sendBuff != null)
        {
            //Debug.Log("buffffff");
            _sendBuff = sendBuff;
        }
        if (receiveBuff != null)
        {
            //Debug.Log("receive");
            _receiveBuff = receiveBuff;
        }
        CalculateAttackPower(BasisPower);//ダメージ計算

    }



    //実ダメージ計算
    private void CalculateAttackPower(int BasisPower)
    {
        Buff all_receiveBuff = new Buff();
        Parameters all_parameters = new Parameters();
        if (/*_receiveBuff != null || */_receiveBuff.Count > 0)
        {
            //Debug.Log(_receiveBuff.Count);
            all_receiveBuff = _receiveBuff[0];
            foreach (Buff s in _receiveBuff)
            {
                //Debug.Log("buff?");
                if (s == _receiveBuff[0])
                    continue;
                all_receiveBuff = s + all_receiveBuff;
            }
            all_parameters = (_status.Parameter + all_receiveBuff) * all_receiveBuff;
        }
        else
            all_parameters = _status.Parameter;
        //Debug.Log(BasisPower);
        _attackPower = is_magic ? all_parameters.MAGICATK : all_parameters.ATK;
        _attackPower = _attackPower * (BasisPower / 100);//威力計算
        Debug.Log("ALLATK"+_attackPower);

    }

}
