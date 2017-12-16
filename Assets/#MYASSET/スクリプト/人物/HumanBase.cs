using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : MonoBehaviour
{
    //人物スーパークラス
    Status humanstatus;         //ステータス
    int lv = 1; //レベル
    int exp;    //現在の経験値
    const int expmax = 100;        //経験値最大量
    bool is_fly;         //飛んでるか   
    BackPack bag;       //持ち物クラス

    //与バフクラス・受バフクラス格納用リスト宣言
    List<Buff> sendBuff = new List<Buff>();
    List<Buff> receiveBuff = new List<Buff>();

    //TODO   ダメージ計算クラス




    private void Start()
    {
        //nullなら非戦闘要員
        humanstatus = GetComponent<Status>();
    }
}
