using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SkillSystem : MonoBehaviour
{

    private enum skillType
    {//スキルの種類
        Up,
        Under,
        Passive,
        Non
    }
    [SerializeField]
    private GameObject HandL, HandR;
    [SerializeField]
    private GameObject SkillZone1, SkillZone2;//スキル発動位置

    [SerializeField]
    private bool CanSlowy;//使用時遅くなるかどうか
    [SerializeField]
    private float Atk;//スキルの攻撃力
    [SerializeField]
    private float Tame;//発動までのため時間
    [SerializeField]
    private float Interval;//スキルのCT

    private SearchHand whereHand1, whereHand2;
    //private VRTK_ControllerReference controllerReference;

    // Use this for initialization
    void Start()
    {
        whereHand1 = SkillZone1.GetComponent<SearchHand>();
        whereHand2 = SkillZone2.GetComponent<SearchHand>();
     
    }

    // Update is called once per frame
    void Update()
    {

        //下側スキル範囲
        SkillZone2.transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
        
    }

    public void PuleseFunc()
    {
      //  SteamVR_TrackedObject trackedObject = (rightorleft == 1 ? rightcontroller : leftcontroller).GetComponent<SteamVR_TrackedObject>();
       // var device = SteamVR_Controller.Input((int)trackedObject.index);
      //  device.TriggerHapticPulse(2000);        //コントローラの振動
    }
}

