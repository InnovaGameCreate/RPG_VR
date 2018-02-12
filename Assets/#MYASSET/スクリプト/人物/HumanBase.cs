using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanBase : MonoBehaviour
{
    //人物スーパークラス
    bool is_fighter;    //戦闘要員かどうか
    Status humanstatus;         //ステータス
                                //一時的ステータス向上時に対応させるため base....の変数を用意

    public Status Status
    {
        get { return humanstatus; }
        set { humanstatus = value; }
    }

    bool is_fly;         //飛んでるか   
    BackPack bag;       //持ち物クラス
    protected Animator animator;            //アニメーターインスタンス

    //受バフリスト・与バフリスト　格納用　宣言
    List<Buff> sendBuff = new List<Buff>();//殴った時に送るバフ
    List<Buff> receiveBuff = new List<Buff>();//殴られた時に受け取ったバフの格納
    List<Buff> counterBuff = new List<Buff>();//殴られた時に相手に送るバフ
    public List<Buff> SendBuff
    {
        get { return sendBuff; }
        set { sendBuff = value; }
    }
    public List<Buff> ReceiveBuff
    {
        get { return receiveBuff; }
        set { receiveBuff = value; }
    }
    public List<Buff> CounterBuff
    {
        get { return counterBuff; }
        set { counterBuff = value; }
    }

    //TODO   ダメージ計算クラス

    protected virtual void Start()
    {
        StartCoroutine("ApplyReceiveBuff");//バフ処理
        humanstatus = new Status();
        //アタッチしていないと以下向こうとなるので　一時的に除去
        if(GetComponentInChildren<DisplayParameters>()!=null)//子
            humanstatus.Parameter = humanstatus.Parameter + GetComponentInChildren<DisplayParameters>();//パラメータ代入


        animator = GetComponent<Animator>();
      
    }

    //攻撃を受けたとき
    public void ReceiveAttack(DamageCalculate d)
    {
        if (d.SendBuff != null)
        {
            receiveBuff.AddRange(d.SendBuff);//与バフを受け取る
            //Debug.Log("sfgreyreh");
        }

        Status.Parameter.HP = Status.Parameter.HP - (d.AttackPower);
    }

    //mp消費
    public void useMagic(int usemp)
    {
        Status.Parameter.MP = Status.Parameter.MP - usemp;

    }

    //受バフの処理
    IEnumerator ApplyReceiveBuff()
    {
        bool Permanence = false;
        while (true)
        {
            foreach (Buff b in receiveBuff)
            {
                //ステータス変更処理
                humanstatus.Parameter = humanstatus.Parameter + b;
                humanstatus.Parameter = humanstatus.Parameter * b;

                //有効時間を減らす
                b.AvailableSeconds--;
                Permanence = b.PARMANENT;
            }
            if (Permanence == false) {
                //有効時間が切れた要素の削除
                receiveBuff.RemoveAll(i => i.AvailableSeconds <= 0);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
