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
        


        // Use this for initialization
        void Start()
        {

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
    }
}
