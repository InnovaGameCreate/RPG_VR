using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;using UnityEngine;


public class flyPursue :Conditional{
    [SerializeField]
    private float _speed = 0;       //スピード
    [SerializeField]
    private SharedGameObject _targetGameObject;     //目的地
    [SerializeField]
    private float ArriveDistance;           //到着距離

    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - _targetGameObject.Value.transform.position) < ArriveDistance)
        {
            return TaskStatus.Success;
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetGameObject.Value.transform.position, _speed * Time.deltaTime);
        transform.LookAt(_targetGameObject.Value.transform);
        return TaskStatus.Running;
    }
}
