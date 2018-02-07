using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField, Tooltip("スキル使用時周りを遅くすることができるようになる")]
    protected bool CanSlowy;//使用時遅くなるかどうか
    [SerializeField]
    private int Atk;//スキルの攻撃力
    [SerializeField, Tooltip("スキル発動までのタメ(秒)")]
    protected float _Time;//発動までのため時間
    [SerializeField, Tooltip("スキルのクールタイム(秒)")]
    private float Interval;//スキルのCT
    [SerializeField, Tooltip("攻撃用プレハブ")]
    private GameObject B_prefabs;//
    [SerializeField]
    private float Speed;//弾速
    [SerializeField]
    private float BreakTime;//玉の消える時間

    public GameObject NodePrefabs;//手動設定(プレハブ)
    protected GameObject Node_Ins;//インスタンス
    public List<GameObject> Trajectory = new List<GameObject>();//スキルの起動表示のためのリスト

    //public GameObject HumanObj;//プレイヤークラス

    protected SearchHand whereHand1, whereHand2;
    protected RPGItemObject _weapon;

    //スキル関係
    private bool SkillAwake;
    protected bool SkillCoolTimeFlag;
    private float timer,timer2;
    protected ParticleSystem _pati;
    //

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

        //HumanObj = GameManager.Instance.VRTKMANAGER.transform.Find("SDKSetups/SteamVR/[CameraRig]/プレイヤークラス").gameObject;
    }

    private void Awake()
    {
        
        HandL = GameManager.Instance.LEFTCONTROLLER;
        HandR = GameManager.Instance.RIGHTCONTROLLER;
        SkillZone1 = GameManager.Instance.VRTKSCRIPTS.transform.Find("Headset/SkillZone1").gameObject;
        SkillZone2 = GameManager.Instance.VRTKSCRIPTS.transform.Find("SkillZone2").gameObject;
        Time.timeScale = 1.0f;
        //スキル用フラグ初期化
        InitSkill();
        //NodePrefabs.transform.parent = eye;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (eye == null)
            eye = GameManager.Instance.VRTKMANAGER.transform.Find("SDKSetups/SteamVR/[CameraRig]/Camera (eye)").transform;

        //下側スキル範囲
        SkillZone2.transform.position = new Vector3(eye.transform.position.x, eye.transform.position.y - 1.0f, eye.transform.position.z);//将来的にはCamera(eye)を参照に座標を決めたい

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

        if(_pati != null)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 5.0f)
            {
                _pati.Stop();
                _pati = null;
                
            }
        }

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

    public void MakeBullet()
    {
        GameObject Bullet = Instantiate(B_prefabs, transform.position, transform.rotation);
        Bullet _bullet = Bullet.GetComponent<Bullet>();
        _bullet.BulletStatus = GameManager.Instance.PLAYER.Status;//ステのコピー
        _bullet.B_SPEED = Speed;
        _bullet.B_BREAKTIME = BreakTime;
        _bullet.SENDBUFF = /*_sendBuff*/null;
        _bullet.B_POWER = Atk;
        _bullet.B_ISMAGIC = false;
        //Debug.Log("QWE");
    }
}

