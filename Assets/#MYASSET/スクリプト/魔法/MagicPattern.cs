using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Buff))]
public class MagicPattern : MonoBehaviour
{
    private GameObject[][] grandson = new GameObject[2][];
    private Transform[] leftright = new Transform[3];     //左右別パターンの親格納用
    private int[] nowpoint = new int[2];      //現在の発動フラグ接触数 
    private Transform eye;
    private bool magic;
    public float maginfinishtime = 1;   //魔法が終わるまでの時間
    public bool startmagic;    //魔法が再生されたかどうか
    public enum USEHAND
    {
        Left,
        Right,
        LeftAndRight,
        None
    }


    public GameObject HumanObj;//開発が落ち着いたらpublicをやめる
    private List<Buff> _sendBuff = new List<Buff>();
    private List<Buff> _receiveBuff = new List<Buff>();

    [SerializeField, TooltipAttribute("魔法の威力(百分率)")]
    private int Bullet_Atk;//魔法の威力
    [SerializeField]
    private float Bullet_FlySpeed;
    [SerializeField]
    private float Bullet_BreakTime;
    [SerializeField]
    private GameObject Bullet;//発射する当たり判定

    private USEHAND usehand;

    public bool _magic
    {
        get
        {
            return magic;
        }
    }

    // Use this for initialization
    void Start()
    {
        eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (eye)").transform;
        HumanObj = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/プレイヤークラス").gameObject;

        int no = 0;   //添字数え上げ用     
        foreach (Transform child in transform)
        {
            leftright[no] = child;
            no++;
        }

        if (leftright[0].childCount > 0 && leftright[1].childCount > 0)
            usehand = USEHAND.LeftAndRight;
        else if (leftright[0].childCount > 0)
            usehand = USEHAND.Left;
        else
            usehand = USEHAND.Right;
        no = 0;
        if (usehand == USEHAND.Left || usehand == USEHAND.LeftAndRight)//左の魔法
            if (leftright[0].childCount != 0)
            {
                this.grandson[0] = new GameObject[leftright[0].childCount];
                foreach (Transform child in leftright[0])
                {
                    this.grandson[0][no] = child.gameObject;
                    if (no != 0)
                        this.grandson[0][no].GetComponent<MeshRenderer>().enabled = false;
                    no++;
                }
            }
        no = 0;
        if (usehand == USEHAND.Right || usehand == USEHAND.LeftAndRight)//右の魔法
            if (leftright[1].childCount != 0)
            {
                this.grandson[1] = new GameObject[leftright[1].childCount];
                foreach (Transform child in leftright[1])
                {
                    this.grandson[1][no] = child.gameObject;
                    if (no != 0)
                        this.grandson[1][no].GetComponent<MeshRenderer>().enabled = false;
                    no++;
                }
            }

        _sendBuff.Add(GetComponent<Buff>());
    }

    // Update is called once per frame
    void Update()
    {

        if (usehand == USEHAND.Left || usehand == USEHAND.LeftAndRight)//左の魔法
            if (leftright[0].childCount - 1 > nowpoint[0] && !grandson[0][nowpoint[0]].GetComponent<MeshRenderer>().enabled)
            {
                grandson[0][nowpoint[0] + 1].GetComponent<MeshRenderer>().enabled = true;
                nowpoint[0]++;
            }
        if (usehand == USEHAND.Right || usehand == USEHAND.LeftAndRight)//右の魔法
            if (leftright[1].childCount - 1 > nowpoint[1] && !grandson[1][nowpoint[1]].GetComponent<MeshRenderer>().enabled)
            {
                grandson[1][nowpoint[1] + 1].GetComponent<MeshRenderer>().enabled = true;
                nowpoint[1]++;
            }

        transform.position = eye.position + eye.forward * 0.5f;
        transform.rotation = eye.rotation;

        switch (usehand)
        {
            case USEHAND.Left:
                if (!magic && leftright[0].childCount - 1 == nowpoint[0] && !grandson[0][nowpoint[0]].GetComponent<MeshRenderer>().enabled)
                {
                    StartCoroutine("Magic");
                    magic = true;
                    
                }
                break;
            case USEHAND.Right:
                if (!magic && leftright[1].childCount - 1 == nowpoint[1] && !grandson[1][nowpoint[1]].GetComponent<MeshRenderer>().enabled)
                {
                    StartCoroutine("Magic");
                    magic = true;
                    
                }
                break;
            case USEHAND.LeftAndRight:
                if (!magic && leftright[0].childCount - 1 == nowpoint[0] && !grandson[0][nowpoint[0]].GetComponent<MeshRenderer>().enabled)
                    if (leftright[1].childCount - 1 == nowpoint[1] && !grandson[1][nowpoint[1]].GetComponent<MeshRenderer>().enabled)
                    {
                        StartCoroutine("Magic");
                        magic = true;
                    }
                break;
            default:
                break;
        }
        //魔法飛ばしてる間も更新してる気がする
        
    }

    // 魔法コルーチン  
    IEnumerator Magic()
    {
        startmagic = true;
        leftright[2].GetComponent<ParticleSystem>().Play();

        GameObject MBullet = Instantiate(Bullet, transform.position, transform.rotation);
        Bullet _bullet = MBullet.GetComponent<Bullet>();
        _bullet.BulletStatus = HumanObj.GetComponent<HumanBase>().Status;//ステのコピー //この時点では値が存在

        //Debug.Log("MagATK" + _bullet.BulletStatus.Parameter.MAGICATK);
        //Debug.Log("HuATK" + HumanObj.GetComponent<HumanBase>().Status.Parameter.MAGICATK);
        _bullet.B_SPEED = Bullet_FlySpeed;
        _bullet.B_BREAKTIME = Bullet_BreakTime;
        _bullet.SENDBUFF = _sendBuff;
        _bullet.B_POWER = Bullet_Atk;
        yield return new WaitForSeconds(maginfinishtime);
        leftright[2].GetComponent<ParticleSystem>().Stop();
        //Destroy(this.gameObject);
    }
}
