﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class SkillSystem : MonoBehaviour
{
    /*
        スキルスーパークラス
            
     */

    private enum skillType
    {//スキルの種類
        Up,
        Under,
        Passive,
        Non
    }
    [SerializeField]
    private skillType SkillTYPE;
    public Transform eye;
    //[SerializeField]
    public GameObject HandL, HandR;
    //[SerializeField]
    public GameObject SkillZone1, SkillZone2;//スキル発動位置

    [SerializeField]
    private bool CanSlowy;//使用時遅くなるかどうか
    [SerializeField]
    private float Atk;//スキルの攻撃力
    [SerializeField]
    protected float _Time;//発動までのため時間
    [SerializeField]
    private float Interval;//スキルのCT
    [SerializeField]
    private float Consumption;//消費パラメータ量　ステータス完成次第触ること　いい単語わからんかった

    //public GameObject NodeList;//スキル用のノードの親
    public List<GameObject> Trajectory = new List<GameObject>();//スキルの起動表示のためのリスト

    private SearchHand whereHand1, whereHand2;
    protected RPGItemObject _weapon;

    //スキル移植用
    private bool running;
    private bool SkillAwake;
    private float timer;

    // Use this for initialization
    void Start()
    {
        whereHand1 = SkillZone1.GetComponent<SearchHand>();
        whereHand2 = SkillZone2.GetComponent<SearchHand>();

        _weapon = GetComponent<RPGItemObject>();

        //eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (head)/Camera (eye)").transform;
        //eye = GameObject.Find("[VRTK_Scripts]/Headset").transform;
        //SkillZone2.transform.parent = eye;

        //スキル用フラグ初期化
        running = false;
        SkillAwake = false;
        timer = 0;
    }

    private void Awake()
    {
        //eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (eye)").transform;
        HandL = GameObject.Find("[VRTK_Scripts]/LeftController");
        HandR = GameObject.Find("[VRTK_Scripts]/RightController");
        SkillZone1 = GameObject.Find("[VRTK_Scripts]/Headset/SkillZone1");
        SkillZone2 = GameObject.Find("[VRTK_Scripts]/SkillZone2");

        //if (Trajectory.Count == 0)
        //{
        //    Debug.Log("スキルの軌道ノードを確認してください");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        //下側スキル範囲
        SkillZone2.transform.position = new Vector3(eye.transform.position.x, 0.6f, eye.transform.position.z);//将来的にはCamera(eye)を参照に座標を決めたい



        //if (GetComponent<RPGItemObject>() != null) {
        //Debug.Log(whereHand1._SearchUP);
        if (_weapon.Touched)
        {
            if (IsSkillAwakeing())
            {
                Trajectory[0].transform.parent.gameObject.SetActive(true);//軌道可視化
                _weapon.EasyPulseFunc(120.0f);
                AwakeSkillUP();
                return;
            }

            if ((whereHand1._SearchR || whereHand1._SearchL) && SkillTYPE == skillType.Up)//上側
            {
                //Debug.Log("vvvvv");
                _weapon.EasyPulseFunc(100.0f);
                StartCoroutine("StayHand_UP", _Time);//テスト用要テスト
            }
            if ((whereHand1._SearchR || whereHand1._SearchL) && SkillTYPE == skillType.Under)//下側
            {
                AwakeSkillDOWN();
            }
            if (SkillTYPE == skillType.Passive)
            {
                AwakeSkillPASSIVE();
            }
        }
        else
        {
            Trajectory[0].transform.parent.gameObject.SetActive(false);//軌道可視化
        }
        //}

        
    }

    protected virtual void AwakeSkillUP()//実際に発動するスキル内容の関数(上側) オーバーライド用
    {
        
    }

    protected virtual void AwakeSkillDOWN()//実際に発動するスキル内容の関数(下側) オーバーライド用
    {

    }

    protected virtual void AwakeSkillPASSIVE()//実際に発動するスキル内容の関数(常時) オーバーライド用
    {

    }


    public IEnumerator StayHand_UP(float SkillTime)//次イノベで確認すべき
    {
        if (/*running || */SkillAwake)//稼働中なら２つ目以降は破棄(現在解除中)
            yield break;

        running = true;

        if (whereHand1._SearchR || whereHand1._SearchL)
        {
            timer += Time.deltaTime;
            //Debug.Log((int)timer + ":SKILL!!!!!!");
            if (timer >= SkillTime)//カウント完了なら
            {
                
                SkillAwake = true;
                yield break;
            }
            yield return new WaitForSeconds(1.0f);//まだならカウントを位置増やしもう一度
        }
        else
        {
            timer = 0;
            running = false;
            SkillAwake = false;
            yield break;
        }

    }

    public bool IsSkillAwakeing()
    {
        return SkillAwake;
    }
}

