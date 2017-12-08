using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SkillPoint : MonoBehaviour {
    //剣が触れたらノードを消すクラス
    [SerializeField]
    private Material TargetMaterial;

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
            SkillSystem _sys = coll.gameObject.GetComponent<SkillSystem>();
            if(_sys.Trajectory[0] == gameObject)
            {
                if (_sys.Trajectory.Count > 1)
                {
                    _sys.Trajectory[1].GetComponent<Renderer>().material = TargetMaterial;
                }
                _sys.Trajectory.RemoveAt(0);//0番から順に消す
                this.gameObject.SetActive(false);
            }
            
        }
        
    }
}
