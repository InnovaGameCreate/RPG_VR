using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class origingravity : MonoBehaviour
{

    public Vector3 localGravity = -Vector3.up*1;

    private Rigidbody rb;
    private float  time;        //一定時間後にアイテム回収可能
    private bool catch_ok;  //回収可能かどうか
    // Use this for initialization
    void Start()
    {

     

        gameObject.AddComponent<Rigidbody>();

        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.useGravity = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = Vector3.up * Mathf.PI;
     

    }

    void FixedUpdate()
    {
        setLocalGravity();
    }

    void setLocalGravity()
    {
        rb.AddForce(localGravity, ForceMode.Acceleration);
    }


}