using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Skill_Slash : SkillSystem
{
    private bool flags = false;
    private bool flags2 = false;

    private GameObject aaaa;
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
        try
        {
            Debug.Log(Trajectory.Count);
            Node_Ins.transform.parent = eye;//目前に固定
            if (Trajectory.Count == 0 || flags)
            {
                _weapon.EasyPulseFunc(160.0f);
                
                if (!flags2)
                {
                    Vector3 pos = new Vector3(eye.transform.position.x+ 5.0f, eye.transform.position.y, eye.transform.position.z);
                    //今見ている方向に対して前にしたい
                    flags2 = true;
                    aaaa = Instantiate(_Particle, pos,eye.transform.rotation);
                    aaaa.transform.parent = null;

                    InitFlags();
                    InitSkill();
                }
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            //flags = true;
            //Debug.Log("スキルがなんかおかしいです");
        }
    }

    protected override void AwakeSkillDOWN()//実際に発動するスキル内容の関数(下側) オーバーライド
    {

    }

    protected override void AwakeSkillPASSIVE()//実際に発動するスキル内容の関数(常時) オーバーライド
    {

    }

    void InitFlags()
    {
        flags = flags2 = false;
    }
}
