using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanBase : MonoBehaviour
{
    //人物スーパークラス
    bool is_fighter;    //戦闘要員かどうか
    protected Status humanstatus;         //ステータス
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
    protected List<Buff> sendBuff = new List<Buff>();//殴った時に送るバフ
    //protected List<Buff> receiveBuff = new List<Buff>();//殴られた時に受け取ったバフの格納
    protected List<Buff> counterBuff = new List<Buff>();//殴られた時に相手に送るバフ

    public Buff[] AwakeBuff = new Buff[10];//実際に計算するバフ
                                    //とりあえず10枠
                                    //None
                                    //Equip_Fix     Equipは直接操作すること
                                    //HP
                                    //DeHP
                                    //ATK
                                    //DeATK
                                    //DEF
                                    //DeDEF
                                    //間違えないように
    public List<Buff> SendBuff
    {
        get { return sendBuff; }
        set { sendBuff = value; }
    }
    //public List<Buff> ReceiveBuff
    //{
    //    get { return receiveBuff; }
    //    set { receiveBuff = value; }
    //}
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
            //receiveBuff.AddRange(d.SendBuff);//与バフを受け取る
            BuffUpdate(d.SendBuff);
        }

        Status.Parameter.HP = Status.Parameter.HP - (d.AttackPower);
    }

    //mp消費
    public void useMagic(int usemp)
    {
        Status.Parameter.MP = Status.Parameter.MP - usemp;

    }

    //受バフの処理
    protected virtual IEnumerator ApplyReceiveBuff()
    {
        //bool Permanence = false;
        while (true)
        {
            //foreach (Buff b in receiveBuff)//旧版
            //{
            //    //ステータス変更処理
            //    humanstatus.Parameter = humanstatus.Parameter + b;
            //    humanstatus.Parameter = humanstatus.Parameter * b;

            //    //有効時間を減らす
            //    b.AvailableSeconds--;
            //    Permanence = b.PARMANENT;
            //}
            //if (Permanence == false) {
            //    //有効時間が切れた要素の削除
            //    receiveBuff.RemoveAll(i => i.AvailableSeconds <= 0);
            //}

            for(int i = 0; i < AwakeBuff.Length; i++)//配列用
            {
                if (AwakeBuff[i] == null)
                    continue;
                //ステータス変更処理
                //humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[i];
                //humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[i];

                //毎回確認するのはバフの時間とHP,MPのみに
                humanstatus.Parameter.HP = humanstatus.Parameter.HP + AwakeBuff[i].HP;
                humanstatus.Parameter.MP = humanstatus.Parameter.MP + AwakeBuff[i].MP;


                //有効時間を減らす
                AwakeBuff[i].AvailableSeconds--;
                //Permanence = AwakeBuff[i].PARMANENT;
                if (AwakeBuff[i].PARMANENT == false && AwakeBuff[i].AvailableSeconds <= 0)//有効時間が切れた要素の削除
                {
                    humanstatus.Parameter = humanstatus.Parameter - AwakeBuff[i];
                    humanstatus.Parameter = humanstatus.Parameter / AwakeBuff[i];
                    AwakeBuff[i] = null;
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    void BuffUpdate(List<Buff> BUFFLIST)
    {
        //少し複雑なのでバグ出るかも
        //Debug.Log("aaaaa");
        foreach (Buff b in BUFFLIST)
        {
            if (b.GetType() == typeof(Buff_ATK))
            {
                if (AwakeBuff[(int)Buff.BuffType.ATK] == null || AwakeBuff[(int)Buff.BuffType.ATK].ATK < b.ATK)
                {
                    AwakeBuff[(int)Buff.BuffType.ATK] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.ATK];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.ATK];
                }

                else if (AwakeBuff[(int)Buff.BuffType.ATK].ATK == b.ATK && AwakeBuff[(int)Buff.BuffType.ATK].AvailableSeconds < b.AvailableSeconds)//時間の長い方
                {
                    AwakeBuff[(int)Buff.BuffType.ATK] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.ATK];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.ATK];
                }
            }
            else if (b.GetType() == typeof(Buff_D_ATK))
            {
                //Debug.Log("bbb");
                if (AwakeBuff[(int)Buff.BuffType.DeATK] == null || AwakeBuff[(int)Buff.BuffType.DeATK].ATK > b.ATK)
                {
                    AwakeBuff[(int)Buff.BuffType.DeATK] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DeATK];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DeATK];
                }
                else if (AwakeBuff[(int)Buff.BuffType.DeATK].ATK == b.ATK && AwakeBuff[(int)Buff.BuffType.DeATK].AvailableSeconds < b.AvailableSeconds)//時間の長い方
                {
                    AwakeBuff[(int)Buff.BuffType.DeATK] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DeATK];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DeATK];
                }
            }
            else if (b.GetType() == typeof(Buff_DEF))
            {
                if (AwakeBuff[(int)Buff.BuffType.DEF] == null || AwakeBuff[(int)Buff.BuffType.DEF].DEF < b.DEF)
                {
                    AwakeBuff[(int)Buff.BuffType.DEF] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DEF];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DEF];
                }

                else if (AwakeBuff[(int)Buff.BuffType.DEF].DEF == b.DEF && AwakeBuff[(int)Buff.BuffType.DEF].AvailableSeconds < b.AvailableSeconds)//時間の長い方
                {
                    AwakeBuff[(int)Buff.BuffType.DEF] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DEF];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DEF];
                }
            }
            else if (b.GetType() == typeof(Buff_D_DEF))
            {
                if (AwakeBuff[(int)Buff.BuffType.DeDEF] == null || AwakeBuff[(int)Buff.BuffType.DeDEF].DEF > b.DEF)
                {
                    AwakeBuff[(int)Buff.BuffType.DeDEF] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DeDEF];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DeDEF];
                }

                else if (AwakeBuff[(int)Buff.BuffType.DeDEF].DEF == b.DEF && AwakeBuff[(int)Buff.BuffType.DeDEF].AvailableSeconds < b.AvailableSeconds)//時間の長い方
                {
                    AwakeBuff[(int)Buff.BuffType.DeDEF] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DeDEF];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DeDEF];
                }

            }
            else if (b.GetType() == typeof(Buff_HP))
            {
                if (AwakeBuff[(int)Buff.BuffType.HP] == null || AwakeBuff[(int)Buff.BuffType.HP].HP < b.HP)
                {
                    AwakeBuff[(int)Buff.BuffType.HP] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.HP];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.HP];
                }


                else if (AwakeBuff[(int)Buff.BuffType.HP].DEF == b.DEF && AwakeBuff[(int)Buff.BuffType.HP].AvailableSeconds < b.AvailableSeconds)//時間の長い方
                {
                    AwakeBuff[(int)Buff.BuffType.HP] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.HP];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.HP];
                }
            }
            else if (b.GetType() == typeof(Buff_D_HP))
            {
                if (AwakeBuff[(int)Buff.BuffType.DeHP] == null || AwakeBuff[(int)Buff.BuffType.DeHP].HP > b.HP)
                {
                    AwakeBuff[(int)Buff.BuffType.DeHP] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DeHP];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DeHP];
                }

                else if (AwakeBuff[(int)Buff.BuffType.DeHP].DEF == b.DEF && AwakeBuff[(int)Buff.BuffType.DeHP].AvailableSeconds < b.AvailableSeconds)//時間の長い方
                {
                    AwakeBuff[(int)Buff.BuffType.DeHP] = b;
                    humanstatus.Parameter = humanstatus.Parameter + AwakeBuff[(int)Buff.BuffType.DeHP];
                    humanstatus.Parameter = humanstatus.Parameter * AwakeBuff[(int)Buff.BuffType.DeHP];
                }
            }
            //else if (b.GetType() == typeof(Buff_EquipFix))
            //{
            //    if(AwakeBuff[(int)Buff.BuffType.Equip_Fix] == null)
            //        AwakeBuff[(int)Buff.BuffType.Equip_Fix] = b;
            //    else
            //        AwakeBuff[(int)Buff.BuffType.Equip_Fix] += b;
            //}
            else
            {
                //Debug.Log("why");
            }
        }
    }
}
