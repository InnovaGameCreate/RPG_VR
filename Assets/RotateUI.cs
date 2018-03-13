using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour {
    private RectTransform myRectTfm;

    // Use this for initialization
    void Start () {
        myRectTfm = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        // 自身の向きをカメラに向ける
        myRectTfm.LookAt(Camera.main.transform);

    }
}
