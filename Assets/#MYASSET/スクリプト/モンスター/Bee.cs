using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : HumanEnemy {
    bool once;
	// Use this for initialization

	// Update is called once per frame
	protected override void Update () {
        base.Update();
        //死んだら落下するように固定を解除
        if (deaded && !once)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            once = true;
        }
	}
}
