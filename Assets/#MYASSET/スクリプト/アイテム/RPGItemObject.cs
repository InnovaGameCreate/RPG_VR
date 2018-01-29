namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RPGItemObject : VRTK_InteractableObject
    {
        /*
         *  アイテムスーパークラス
         *  11/25現在右のみイベントを受け取れる状態
         */
        protected GameObject ParticleObject;//パーティクルオブジェクト
        [SerializeField]
        private GameObject rightcontroller;       //右コントローラ
        [SerializeField]
        private GameObject leftcontroller;       //左コントローラ

        public int ID;//アイテムID
        public bool CanStack;//アイテムがスタック可能かどうか
        public float ObjectBreakTime;//フィールドのアイテムが消える時間
        public string FlavorText;//説明文

        /*イベントトリガ*/
        public bool triggerd;
        public bool Touched;
        public bool Griped;
        //public RPGItemObject triggerdother;
        private VRTK_ControllerReference controllerReference;

        // Use this for initialization
        protected virtual void Start()
        {
            //leftcontroller = GameObject.Find("[VRTK_Scripts]/LeftController").gameObject;
            //rightcontroller = GameObject.Find("[VRTK_Scripts]/RightController").gameObject;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (rightcontroller.GetComponent<VRTK_ControllerEvents>() == null)
            {
                Debug.Log("dsfgvrgber");
                return;
            }
            // イベントハンドラの登録
            //トリガー
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressedHandler2;
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TriggerReleased += TriggerReleasedHandler2;
            //タッチパッド
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += TouchPadPressedHandler2;
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TouchpadReleased += TouchPadReleasedHandler2;
            //グリップ
            rightcontroller.GetComponent<VRTK_ControllerEvents>().GripPressed += GripPressedHandler2;
            rightcontroller.GetComponent<VRTK_ControllerEvents>().GripReleased += GripReleasedHandler2;
            //振動用
            controllerReference = null;
            interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (rightcontroller.GetComponent<VRTK_ControllerEvents>() == null)
                return;
            // イベントハンドラの解除 
            //トリガー
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressedHandler2;
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TriggerReleased -= TriggerReleasedHandler2;
            //タッチパッド
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= TouchPadPressedHandler2;
            rightcontroller.GetComponent<VRTK_ControllerEvents>().TouchpadReleased -= TouchPadReleasedHandler2;
            //グリップ
            rightcontroller.GetComponent<VRTK_ControllerEvents>().GripPressed -= GripPressedHandler2;
            rightcontroller.GetComponent<VRTK_ControllerEvents>().GripReleased -= GripReleasedHandler2;
        }

        protected override void Awake()
        {
            base.Awake();
            //rightcontroller = GameObject.Find("/[VRTK_SDKManager] /SDKSetups/SteamVR/[CameraRig]/Controller (right)");
            //leftcontroller = GameObject.Find("/[VRTK_SDKManager] /SDKSetups/SteamVR/[CameraRig]/Controller (left)");
            leftcontroller = GameObject.Find("[VRTK_Scripts]/LeftController").gameObject;
            rightcontroller = GameObject.Find("[VRTK_Scripts]/RightController").gameObject;
        }

        // Update is called once per frame
        //void Update()
        //{

        //}

        // イベントハンドラ
        /*トリガー*/
        protected virtual void TriggerPressedHandler2(object sender, ControllerInteractionEventArgs e)//トリガーを押したとき
        {
            triggerd = true;

            
        }

        protected virtual void TriggerReleasedHandler2(object sender, ControllerInteractionEventArgs e)//トリガーを離したとき
        {
            triggerd = false;
            

        }
        /*タッチパッド*/
        protected virtual void TouchPadPressedHandler2(object sender, ControllerInteractionEventArgs e)//タッチパッドを押したとき
        {
            Touched = true;


        }

        protected virtual void TouchPadReleasedHandler2(object sender, ControllerInteractionEventArgs e)//タッチパッドを離したとき
        {
            Touched = false;

            
        }

        /*グリップ*/
        protected virtual void GripPressedHandler2(object sender, ControllerInteractionEventArgs e)//グリップを押したとき
        {
            Griped = true;


        }

        protected virtual void GripReleasedHandler2(object sender, ControllerInteractionEventArgs e)//グリップを離したとき
        {
            Griped = false;


        }

        public void EasyPulseFunc(float farce)
        {
            //100でもまあまあ強い
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, farce);
        }

        /*振動用ハンドラ？*/
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
        /*ここまで*/
    }
}
