using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//クエストのデバッグ用で作りました（いつか消します）
public class DebugScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<HumanNPC>().sendQuest();
	}
	
	// Update is called once per frame
	void Update () {
        //左クリックでゴブリン倒した判定
        if (Input.GetMouseButtonDown(0))
        {
            //これを使えばいいのでは！？
            GameManager.Instance.QMANAGER.CheckQuestAchievement("ゴブリン");
        }
	}
}
