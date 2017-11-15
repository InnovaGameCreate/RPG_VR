using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_time : MonoBehaviour
{

    public GameObject obj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0.1f;
        //Instantiate<GameObject>(obj);
    }
}
