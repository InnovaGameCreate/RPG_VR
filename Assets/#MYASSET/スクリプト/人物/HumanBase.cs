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
    }
    public List<Buff> ReceiveBuff
    {
        get { return receiveBuff; }
    }
    public List<Buff> CounterBuff
    {
        get { return counterBuff; }
        set { counterBuff = value; }
    }

    //TODO   ダメージ計算クラス

    protected virtual void Start()
    {
        StartCoroutine("ApplyReceiveBuff");
        humanstatus = new Status();
        //アタッチしていないと以下向こうとなるので　一時的に除去
        if(GetComponentInChildren<DisplayParameters>()!=null)//子
            humanstatus.Parameter = humanstatus.Parameter + GetComponentInChildren<DisplayParameters>();//パラメータ代入
        //if (GetComponent<DisplayParameters>() != null)//直
        //    humanstatus.Parameter = humanstatus.Parameter + GetComponent<DisplayParameters>();//パラメータ代入

        animator = GetComponent<Animator>();
      
    }

    //攻撃を受けたとき
    public void ReceiveAttack(DamageCalculate d)
    {
        if(d.SendBuff != null)
            receiveBuff.AddRange(d.SendBuff);//与バフを受け取る
        if (Status.Parameter == null)
            Debug.Log("sfgreyreh");
        if (d == null)
            Debug.Log("dsgdhfhhhhhhh");
        Status.Parameter.HP = Status.Parameter.HP - (d.AttackPower);
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
