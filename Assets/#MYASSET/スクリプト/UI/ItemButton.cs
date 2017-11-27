using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour {

    //[System.NonSerialized]
    public GameObject item_prefab;

    private bool isSelected;

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
