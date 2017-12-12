using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public abstract class SkillSystem : MonoBehaviour
{
    /*
        スキルスーパークラス
            
     */

    public enum skillType
    {//スキルの種類
        Up,
        Under,
        Passive,
        Non
    }
    [SerializeField,TooltipAttribute("スキルタイプ設定")]
    public skillType SkillTYPE;
    public Transform eye;
    //[SerializeField]
    public GameObject HandL, HandR;
    //[SerializeField]
    public GameObject SkillZone1, SkillZone2;//スキル発動位置

    [SerializeField, Tooltip("出現パーティクルプレハブ設定")]
    protected GameObject _Particle;//手動設定
    [SerializeField, Tooltip("パーティクルの出現位置設定")]
    protected Transform MakeParticlePos;
    [SerializeField, Tooltip("スキル使用時周りを遅くすることができるようになるかも")]
    protected bool CanSlowy;//使用時遅くなるかどうか
    [SerializeField]
    private float Atk;//スキルの攻撃力
    [SerializeField, Tooltip("スキル発動までのタメ(秒)")]
    protected float _Time;//発動までのため時間
    [SerializeField, Tooltip("スキルのクールタイム(秒) 未実装")]
    private float Interval;//スキルのCT
    [SerializeField, Tooltip("消費パラメータ量 未実装")]
    private float Consumption;//消費パラメータ量　ステータス完成次第触ること　いい単語わからんかった

    public GameObject NodePrefabs;//手動設定(プレハブ)
    protected GameObject Node_Ins;//インスタンス
    public List<GameObject> Trajectory = new List<GameObject>();//スキルの起動表示のためのリスト
    

    protected SearchHand whereHand1, whereHand2;
    protected RPGItemObject _weapon;

    //スキル関係
    private bool SkillAwake;
    protected bool SkillCoolTimeFlag;
    private float timer;

    // Use this for initialization
    void Start()
    {
        whereHand1 = SkillZone1.GetComponent<SearchHand>();
        whereHand2 = SkillZone2.GetComponent<SearchHand>();

        _weapon = GetComponent<RPGItemObject>();

        SkillCoolTimeFlag = false;//ここで初期化
        //T_parent = Trajectory[0].transform.parent.gameObject;

        //eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (head)/Camera (eye)").transform;
        //eye = GameObject.Find("[VRTK_Scripts]/Headset").transform;
        //SkillZone2.transform.parent = eye;


    }

    private void Awake()
    {
        //eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (eye)").transform;
        HandL = GameObject.Find("[VRTK_Scripts]/LeftController");
        HandR = GameObject.Find("[VRTK_Scripts]/RightController");
        SkillZone1 = GameObject.Find("[VRTK_Scripts]/Headset/SkillZone1");
        SkillZone2 = GameObject.Find("[VRTK_Scripts]/SkillZone2");
        Time.timeScale = 1.0f;
        //スキル用フラグ初期化
        InitSkill();
        //NodePrefabs.transform.parent = eye;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        //下側スキル範囲
        SkillZone2.transform.position = new Vector3(eye.transform.position.x, 0.5f, eye.transform.position.z);//将来的にはCamera(eye)を参照に座標を決めたい

        if (SkillCoolTimeFlag)//クールタイム発生なら
        {
            timer += Time.deltaTime;
            if (Interval <= timer)
            {
                SkillCoolTimeFlag = false;
                timer = 0;
            }
            else
            {
                return;
            }
        }

        //if (_weapon.Touched)
        //{

        //    if (IsSkillAwakeing())
        //    {
        //        Node_Ins.SetActive(true);//軌道可視化
        //        _weapon.EasyPulseFunc(120.0f);
        //        AwakeSkill();
        //        if (CanSlowy && !SkillCoolTimeFlag)
        //            Time.timeScale = 0.1f;
        //        return;
        //    }

        //    if ((whereHand1._SearchR /*|| whereHand1._SearchL*/) && SkillTYPE == skillType.Up)//上側
        //    {
        //        //Debug.Log("vvvvv");
        //        _weapon.EasyPulseFunc(100.0f);
        //        StartCoroutine("StayHand", _Time);//テスト用要テスト
        //    }
        //    if ((whereHand2._SearchR /*|| whereHand1._SearchL*/) && SkillTYPE == skillType.Under)//下側
        //    {

        //        StartCoroutine("StayHand", _Time);
        //    }
        //    if (SkillTYPE == skillType.Passive)
        //    {

        //        StartCoroutine("StayHand", _Time);
        //    }
        //}
        //else
        //{
        //    Time.timeScale = 1.0f;
        //    Node_Ins.SetActive(false);//軌道可視化
        //}
    }

    protected abstract void AwakeSkill();//実際に発動するスキル内容の関数 オーバーライド用



    public IEnumerator StayHand(float SkillTime)//
    {
        if (/*running || */SkillAwake)//稼働中なら２つ目以降は破棄(現在解除中)
            yield break;

        //running = true;

        if (whereHand1._SearchR || whereHand2._SearchR)
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
            InitSkill();
            yield break;
        }

    }

    public bool IsSkillAwakeing()
    {
        return SkillAwake;
    }

    protected void InitSkill()
    {
        Node_Ins = Instantiate(NodePrefabs, eye);
        foreach (Transform Node_Ins in Node_Ins.transform)
        {
            Trajectory.Add(Node_Ins.gameObject);
        }
        Node_Ins.SetActive(false);

        timer = 0;
        Time.timeScale = 1.0f;
        SkillAwake = false;

    }

}

