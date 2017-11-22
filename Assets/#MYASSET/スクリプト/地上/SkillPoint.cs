using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.GetComponent<SkillSystem>() != null)
        {
            gameobject.enable;
        }
    }
}
