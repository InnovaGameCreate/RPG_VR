using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Skill_Slash : SkillSystem
{
    //スキルサブクラス
    //スキルはタッチパッド

	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    protected override void AwakeSkillUP()//実際に発動するスキル内容の関数(上側) オーバーライド
    {
        Debug.Log(Trajectory.Count);
        Trajectory[0].transform.parent.transform.parent = eye;//目前に固定
        if(Trajectory.Count == 0)
        {
            _weapon.EasyPulseFunc(160.0f);
            Debug.Log("おめめ");
        }
    }

    protected override void AwakeSkillDOWN()//実際に発動するスキル内容の関数(下側) オーバーライド
    {

    }

    protected override void AwakeSkillPASSIVE()//実際に発動するスキル内容の関数(常時) オーバーライド
    {

    }
}
