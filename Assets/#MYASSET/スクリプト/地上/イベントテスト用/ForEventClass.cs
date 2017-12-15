using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class SwordEventClassAAA : UnityEvent<int, int, int>
{
}

public class ForEventClass : MonoBehaviour {

    public SwordEventClassAAA OnNasubi;

    // Use this for initialization
    void Start () {
		OnNasubi.AddListener(Messsafge);//イベントテスト
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown && OnNasubi != null)
        {
            OnNasubi.Invoke(5, 6, 7);
        }
    }

    public void Masseage()
    {
        Debug.Log("nasubi");
    }

    public void Messsafge(int a, int b, int c)
    {
        Debug.Log("aaaa:" + a + "qwe" + b * c);
    }
}
