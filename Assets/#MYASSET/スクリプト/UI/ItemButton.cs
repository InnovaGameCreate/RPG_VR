using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour {

    //[System.NonSerialized]
    public GameObject item_prefab;

    public ItemBase item { get; set; }//アイテム
    private bool isSelected;//現在選択されているか

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnEvent()
    {
        Debug.Log("event");
    }

    public void IsSelected(bool b)
    {
        isSelected = b;
    }
}
