using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

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
    private float Time;//発動までのため時間
    [SerializeField]
    private float Interval;//スキルのCT

    //public GameObject NodeList;//スキル用のノードの親
    public List<GameObject> Trajectory = new List<GameObject>();//スキルの起動表示のためのリスト

    private SearchHand whereHand1, whereHand2;
    //private VRTK_ControllerReference controllerReference;

    // Use this for initialization
    void Start()
    {
        whereHand1 = SkillZone1.GetComponent<SearchHand>();
        whereHand2 = SkillZone2.GetComponent<SearchHand>();
        eye = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (head)").transform;


    }

    private void Awake()
    {
        
        HandL = GameObject.Find("[VRTK_Scripts]/LeftController");
        HandR = GameObject.Find("[VRTK_Scripts]/RightController");
        SkillZone1 = GameObject.Find("[VRTK_Scripts]/Headset/SkillZone1");
        SkillZone2 = GameObject.Find("[VRTK_Scripts]/SkillZone2");

        
    }

    // Update is called once per frame
    void Update()
    {

        //下側スキル範囲
        SkillZone2.transform.position = new Vector3(eye.transform.position.x, 0.6f, eye.transform.position.z);

        if (whereHand1._SearchUP && SkillTYPE == skillType.Up)//上側
        {
            AwakeSkillUP();
        }
        if (whereHand2._SearchDown && SkillTYPE == skillType.Under)//下側
        {
            AwakeSkillDOWN();
        }
        if (SkillTYPE == skillType.Passive)
        {
            AwakeSkillPASSIVE();
        }
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

    public void PuleseFunc()
    {
      //  SteamVR_TrackedObject trackedObject = (rightorleft == 1 ? rightcontroller : leftcontroller).GetComponent<SteamVR_TrackedObject>();
       // var device = SteamVR_Controller.Input((int)trackedObject.index);
      //  device.TriggerHapticPulse(2000);        //コントローラの振動
    }
}

