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

    protected override void Update()
    {
        base.Update();

        if (_weapon.Touched)
        {

            if (IsSkillAwakeing())
            {
                Node_Ins.SetActive(true);//軌道可視化
                _weapon.EasyPulseFunc(120.0f);
                AwakeSkill();
                if (CanSlowy && !SkillCoolTimeFlag)
                    Time.timeScale = 0.1f;
                return;
            }

            if ((whereHand1._SearchR) && SkillTYPE == skillType.Up)//上側
            {
                _weapon.EasyPulseFunc(100.0f);
                StartCoroutine("StayHand", _Time);//テスト用要テスト
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            Node_Ins.SetActive(false);//軌道可視化
        }
    }

    protected override void AwakeSkill()//実際に発動するスキル内容の関数(上側) オーバーライド
    {
        try
        {
            
            Node_Ins.transform.parent = eye;//目前に固定
            if (Trajectory.Count == 0 || flags)
            {
                _weapon.EasyPulseFunc(140.0f);
                
                if (!flags2)
                {
                    Vector3 pos = new Vector3(eye.transform.position.x+ 5.0f, eye.transform.position.y, eye.transform.position.z);
                    //今見ている方向に対して前にしたい
                    flags2 = true;
                    aaaa = Instantiate(_Particle, pos, eye.transform.rotation);
                    aaaa.transform.parent = null;

                    InitFlags();
                    InitSkill();
                    SkillCoolTimeFlag = true;
                    Time.timeScale = 1.0f;
                }
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            //flags = true;
            //Debug.Log("スキルがなんかおかしいです");
        }
    }

    //protected override void AwakeSkillDOWN()//実際に発動するスキル内容の関数(下側) オーバーライド
    //{

    //}

    //protected override void AwakeSkillPASSIVE()//実際に発動するスキル内容の関数(常時) オーバーライド
    //{

    //}

    void InitFlags()
    {
        flags = flags2 = false;
    }
}
