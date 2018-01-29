using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Skill_SwordUpper : SkillSystem
{
    private bool flags = false;
    private bool flags2 = false;

    private GameObject aaaa;
    //スキルサブクラス
    //スキルはタッチパッド

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

            if ((whereHand2._SearchR) && SkillTYPE == skillType.Under)//上側
            {
                _weapon.EasyPulseFunc(100.0f);
                //Debug.Log("uuuuuuu");
                StartCoroutine("StayHand", _Time);//テスト用要テスト
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            Node_Ins.SetActive(false);//軌道可視化
        }
    }

    protected override void AwakeSkill()//実際に発動するスキル内容の関数 オーバーライド
    {
        try
        {
            //Debug.Log(Trajectory.Count);
            Node_Ins.transform.parent = eye;//目前に固定
            Node_Ins.transform.localPosition = new Vector3(0.0f, 0.03f, 1.2f);
            Node_Ins.transform.localRotation = Quaternion.Euler(0, 90, 0);
            if (Trajectory.Count == 0 || flags)
            {
                _weapon.EasyPulseFunc(140.0f);
                
                if (!flags2)
                {
                    Vector3 pos = new Vector3(eye.transform.position.x+ 5.0f, eye.transform.position.y, eye.transform.position.z);
                    //今見ている方向に対して前にしたい
                    flags2 = true;
                    aaaa = Instantiate(_Particle, eye.position + eye.forward * 1.0f, eye.rotation);
                    _pati = aaaa.GetComponent<ParticleSystem>();
                    MakeBullet();
                    aaaa.transform.parent = null;

                    InitFlags();
                    InitSkill();
                    SkillCoolTimeFlag = true;
                }
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            //flags = true;
            //Debug.Log("スキルがなんかおかしいです");
        }
    }

    void InitFlags()
    {
        flags = flags2 = false;
    }
}
