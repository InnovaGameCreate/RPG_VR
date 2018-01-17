using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Status bulletstatus;
    private List<Buff> _send = new List<Buff>();
    private List<Buff> _receive = new List<Buff>();

    // Use this for initialization
    void Start () {
        bulletstatus = new Status();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "enemy")
        {
            //Debug.Log("aaaafojsegj");
            //Status st = GetComponentInParent<HumanBase>().Status;

            if (!GetComponent<Buff>())
                Debug.Log("fbvgsureiuheirhghhhh");

            _send.Add(GetComponent<Buff>());
            DamageCalculate dam = new DamageCalculate(bulletstatus, _send, _send);
        }
    }
}
