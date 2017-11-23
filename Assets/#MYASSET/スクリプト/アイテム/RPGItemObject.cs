namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RPGItemObject : VRTK_InteractableObject
    {
        /*
         *  アイテムスーパークラス
         *  
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
        public RPGItemObject triggerdother;


        // Use this for initialization
        void Start()
        {
            //base.Start();//継承元
            if (gameObject.name == "RightController")
                triggerdother = GameObject.Find("[VRTK_Scripts]/LeftController").GetComponent<RPGItemObject>();
            else
                triggerdother = GameObject.Find("[VRTK_Scripts]/RightController").GetComponent<RPGItemObject>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (GetComponent<VRTK_ControllerEvents>() == null)
                return;
            // イベントハンドラの登録
            //トリガー
            GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressedHandler;
            GetComponent<VRTK_ControllerEvents>().TriggerReleased += TriggerReleasedHandler;
            //タッチパッド
            GetComponent<VRTK_ControllerEvents>().TouchpadPressed += TouchPadPressedHandler;
            GetComponent<VRTK_ControllerEvents>().TouchpadReleased += TouchPadReleasedHandler;
            //グリップ
            GetComponent<VRTK_ControllerEvents>().GripPressed += GripPressedHandler;
            GetComponent<VRTK_ControllerEvents>().GripReleased += GripReleasedHandler;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (GetComponent<VRTK_ControllerEvents>() == null)
                return;
            // イベントハンドラの解除 
            //トリガー
            GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressedHandler;
            GetComponent<VRTK_ControllerEvents>().TriggerReleased -= TriggerReleasedHandler;
            //タッチパッド
            GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= TouchPadPressedHandler;
            GetComponent<VRTK_ControllerEvents>().TouchpadReleased -= TouchPadReleasedHandler;
            //グリップ
            GetComponent<VRTK_ControllerEvents>().GripPressed -= GripPressedHandler;
            GetComponent<VRTK_ControllerEvents>().GripReleased -= GripReleasedHandler;
        }

        protected override void Awake()
        {
            base.Awake();
            rightcontroller = GameObject.Find("/[VRTK_SDKManager] /SDKSetups/SteamVR/[CameraRig]/Controller (right)");
            leftcontroller = GameObject.Find("/[VRTK_SDKManager] /SDKSetups/SteamVR/[CameraRig]/Controller (left)");
        }

        // Update is called once per frame
        void Update()
        {

        }

        // イベントハンドラ
        /*トリガー*/
        protected virtual void TriggerPressedHandler(object sender, ControllerInteractionEventArgs e)//トリガーを押したとき
        {
            //triggerd = true;

            
        }

        protected virtual void TriggerReleasedHandler(object sender, ControllerInteractionEventArgs e)//トリガーを離したとき
        {
            //triggerd = false;
            

        }
        /*タッチパッド*/
        protected virtual void TouchPadPressedHandler(object sender, ControllerInteractionEventArgs e)//タッチパッドを押したとき
        {
            //Touched = true;


        }

        protected virtual void TouchPadReleasedHandler(object sender, ControllerInteractionEventArgs e)//タッチパッドを離したとき
        {
            //Touched = false;

            
        }

        /*グリップ*/
        protected virtual void GripPressedHandler(object sender, ControllerInteractionEventArgs e)//グリップを押したとき
        {
            //Griped = true;


        }

        protected virtual void GripReleasedHandler(object sender, ControllerInteractionEventArgs e)//グリップを離したとき
        {
            //Griped = false;


        }
    }
}
