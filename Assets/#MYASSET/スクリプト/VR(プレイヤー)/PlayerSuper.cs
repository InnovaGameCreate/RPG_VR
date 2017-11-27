using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuper : MonoBehaviour {

    /*
		プレイヤースーパークラスっぽいの 
			サブクラス候補
				持ち物管理
				装備品
				ステータス
                UI?
	*/


    [SerializeField]
    private GameObject SkillZone1,SkillZone2;//スキルクラスのを後程移植すること
    [SerializeField]
    private GameObject HipItemZone;//腰アイテム位置
    [SerializeField]
    private GameObject SwordRepwan;//抜剣位置

    /*
     * 位置参照はここで一括のほうがいいんじゃない？
    public GameObject HandR, HandL;//手
    public GameObject Eye;//頭の位置
    */

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
