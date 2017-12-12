using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class SkillPoint : MonoBehaviour {
    //剣が触れたらノードを消すクラス
    [SerializeField]
    private Material TargetMaterial;
    [SerializeField]
    private SkillSystem[] _sys  = new SkillSystem[3];
    private SkillSystem target;
    public SkillSystem.skillType SkillTYPE;

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
            _sys = coll.gameObject.GetComponents<SkillSystem>();
            for(int i = 0; i < _sys.Length; i++)
            {
                if (SkillTYPE == _sys[i].SkillTYPE)
                    target = _sys[i];
            }

            if (target.Trajectory[0] == gameObject)
            {
                if (target.Trajectory.Count > 1)
                {
                    target.Trajectory[1].GetComponent<Renderer>().material = TargetMaterial;
                }
                target.Trajectory.RemoveAt(0);//0番から順に消す
                this.gameObject.SetActive(false);
            }
            
            
        }
        
    }
}
