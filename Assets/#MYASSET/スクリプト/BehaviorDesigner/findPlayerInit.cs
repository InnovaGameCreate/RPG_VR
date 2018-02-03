using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class findPlayerInit : Conditional
{
    [SerializeField]
    private SharedGameObject player;     //追跡すべきプレイヤー

    // 実行直後に呼ばれる。データは持ち越しのため、必ず初期化などを書く。
    public override void OnStart()
    {
        GameObject manager;
        base.OnAwake();
        if (GameManager.Instance.name == "ゲームマネージャー（確定）")
            player.Value = GameManager.Instance.HEAD;
    }

}
