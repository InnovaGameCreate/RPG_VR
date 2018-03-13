using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RespawnManager : MonoBehaviour {


    //各戦闘ステージにあらかじめ配置しておく
    //シーンにもともと設置している分だけ復活

    GameObject []tagobjs;
    respawn_info []monsters;

    struct respawn_info
    {
       public GameObject linkmonster;
       public Vector3 inipos;
       public int monsterT;
       public bool notRevive;
    }
    [SerializeField, TooltipAttribute("リスポーンさせるプレハブ設定")]
    GameObject[] prefab_Monster;

  
    // Use this for initialization
    void Start () {
        tagobjs = GameObject.FindGameObjectsWithTag("enemy(parent)");
        monsters = new respawn_info[tagobjs.Length];

        for (int i = 0; i < tagobjs.Length; i++)
        {
            monsters[i].linkmonster = tagobjs[i];
            monsters[i].notRevive = tagobjs[i].GetComponentInChildren<HumanEnemy>().NOTREVIVE;
            monsters[i].inipos = tagobjs[i].transform.localPosition;

            for (int j = 0; j < prefab_Monster.Length; j++)
            {
                var myRegExp = new Regex(prefab_Monster[j].name);
                if (myRegExp.IsMatch(tagobjs[i].name))
                    monsters[i].monsterT= j;
            }
        }
      
    }

    // Update is called once per frame
    void Update () {
        for(int i=0;i < monsters.Length;i++)
        {
            if (monsters[i].linkmonster == null && !monsters[i].notRevive)
                monsters[i].linkmonster = Instantiate(prefab_Monster[monsters[i].monsterT], monsters[i].inipos, Quaternion.identity);
        }

    }
}
