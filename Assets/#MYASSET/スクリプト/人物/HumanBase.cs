using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBase : MonoBehaviour {
    //人物スーパークラス
    Status humanstatus;         //ステータス
    int hp;     //体力残量
    int mp;     //魔力残量
    int lv = 1; //レベル
    int exp;    //現在の経験値
    const int expmax = 100;        //経験値最大量
    bool is_fly;         //飛んでるか   
    BackPack bag;       //持ち物クラス
     //TODO   与バフクラス・受バフクラス格納用リスト宣言

    public int HP
    {
        get { return hp; }
    }
    public int MP
    {
        get { return mp; }
    }


}
