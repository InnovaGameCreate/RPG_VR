using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class flyPatrol : Conditional {
    [SerializeField]
    private float _speed = 0;           //スピード
    [SerializeField]
    private SharedGameObjectList _targetGameObject;     //パトロール場所のリスト
    [SerializeField]
    private float ArriveDistance;           //到着距離
    [SerializeField]
    private float WaypointPauseDuration;      //到着時の待機時間
    private int checknum;           //パトロールで調べてる番号
    private float waypointReachedTime;      
    private bool reach;             //ついたかどうか
    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - _targetGameObject.Value[checknum].transform.position) < ArriveDistance)
        {
            if (!reach)
            {
                waypointReachedTime = Time.time;
                reach = true;
            }
            else if (waypointReachedTime + WaypointPauseDuration <= Time.time)
            {
                checknum++;
                checknum %= _targetGameObject.Value.Count;
                reach = false;
            }
            return TaskStatus.Success;
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetGameObject.Value[checknum].transform.position, _speed * Time.deltaTime);
        transform.LookAt(_targetGameObject.Value[checknum].transform);
        return TaskStatus.Running;
    }

    // Reset the public variables
    public override void OnReset()
    {
        base.OnReset();
    }
}
