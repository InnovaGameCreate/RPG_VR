using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour {
    public enum ItemType
    {
        Healing,    //回復アイテム
        StatusUp,    //ステータスアップアイテム
        Equipment,  //装備アイテム
        Important,   //鍵等の進行系アイテム
        Monster,    //モンスター素材
        None
    };
    private const int maxhasnum = 99;      //アイテム最大所持数
    public GameObject[][] item = new GameObject [(int)ItemType.None][];

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
