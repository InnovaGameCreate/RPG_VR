using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class findPlayerInit : Conditional
{
    [SerializeField]
    private SharedGameObject player;     //追跡すべきプレイヤー
    [SerializeField]
    private float ArriveDistance;           //到着距離
    [SerializeField]
    private float WaypointPauseDuration;      //到着時の待機時間
    private int checknum;           //パトロールで調べてる番号
    private float waypointReachedTime;
    private bool reach;             //ついたかどうか

    // 実行直後に呼ばれる。データは持ち越しのため、必ず初期化などを書く。
    public override void OnAwake()
    {
        base.OnAwake();
        player.Value = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Camera (head)");
        Debug.Log(player);
    }

}
