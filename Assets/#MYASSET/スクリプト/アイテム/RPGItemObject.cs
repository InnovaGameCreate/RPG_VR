namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RPGItemObject : VRTK_InteractableObject
    {
        protected GameObject ParticleObject;
        [SerializeField]
        private GameObject rightcontroller;       //右コントローラ
        [SerializeField]
        private GameObject leftcontroller;       //左コントローラ

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
