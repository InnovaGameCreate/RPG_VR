using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

[RequireComponent(typeof(SkillSystem))]
public class Weapon_Sword : RPGItemObject
{
    /*
        武器クラス
        実際に実装する方
     */

    //public float Atk;
    [SerializeField]
    SkillSystem Skill1;
    [SerializeField]
    SkillSystem Skill2;

    private List<Buff> _send = new List<Buff>();
    private List<Buff> _receive = new List<Buff>();

    [SerializeField]
    private HumanBase pplay;

    // Use this for initialization
    //protected override void Start () {
    //       base.Start();
    //}

    //Update is called once per frame
    void Update()
    {
        if (triggerd)
        {
            //Debug.Log("triggggg");
        }
        if (Griped)
        {
            
        }
        //if (Touched)
        //{
        //    GetComponent<SkillSystem>().enabled = true;
        //}
        //else
        //{
        //    GetComponent<SkillSystem>().enabled = false;
        //}

        //if (GetComponent<BoxCollider>() )
        //{
        //    Debug.Log("afgedghrdh");
        //}
    }

    


    /*トリガー*/
    protected override void TriggerPressedHandler2(object sender, ControllerInteractionEventArgs e)//トリガーを押したとき
    {
        triggerd = true;

        
    }

    protected override void TriggerReleasedHandler2(object sender, ControllerInteractionEventArgs e)//トリガーを離したとき
    {
        triggerd = false;


    }
    /*タッチパッド*/
    protected override void TouchPadPressedHandler2(object sender, ControllerInteractionEventArgs e)//タッチパッドを押したとき
    {
        Touched = true;

        //Debug.Log("tttt");
    }

    protected override void TouchPadReleasedHandler2(object sender, ControllerInteractionEventArgs e)//タッチパッドを離したとき
    {
        Touched = false;


    }

    /*グリップ*/
    protected override void GripPressedHandler2(object sender, ControllerInteractionEventArgs e)//グリップを押したとき
    {
        Griped = true;
        //Debug.Log("ggg");
        GetComponent<BoxCollider>().isTrigger = true;

    }

    protected override void GripReleasedHandler2(object sender, ControllerInteractionEventArgs e)//グリップを離したとき
    {
        Griped = false;
        
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        GetComponent<BoxCollider>().isTrigger = false;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "enemy" && this.enabled)
        {
            //Debug.Log("aaaafojsegj");
            //Status st = GetComponentInParent<HumanBase>().Status;
            Status st = GameManager.Instance.PLAYER.Status;
            // Status st = pplay.Status;

            if (!GetComponent<Buff>())
                Debug.Log("fbvgsureiuheirhghhhh");

            _send.Add(GetComponent<Buff>());
            List<Buff> _counter = null;
            //if (coll.gameObject.GetComponent<HumanBase>().CounterBuff != null)//テストプレイ時敵にHumanBaseがアタッチされてないためつくまでコメントアウト
            //{
            //    _counter = coll.gameObject.GetComponent<HumanBase>().CounterBuff;
            //}
            DamageCalculate dam = new DamageCalculate(st, 100, false, _send, _counter);
            coll.gameObject.GetComponent<HumanBase>().ReceiveAttack(dam);
        }

        if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
        {
            collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
            var hapticStrength = collisionForce / maxCollisionForce;
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.5f, 0.01f);
        }
        else
        {
            collisionForce = coll.relativeVelocity.magnitude * impactMagnifier;
        }
    }


    // 振動関連
    private float impactMagnifier = 240f;//120f;
    private float collisionForce = 0f;
    private float maxCollisionForce = 2000f;//4000f;
    private VRTK_ControllerReference controllerReference;

    public float CollisionForce()
    {
        return collisionForce;
    }

    public override void Grabbed(VRTK_InteractGrab grabbingObject)
    {
        base.Grabbed(grabbingObject);
        controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        controllerReference = null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controllerReference = null;
        interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }


}
