using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToUnder : MonoBehaviour
{
    private GoUpDown goupdown;          //goupdownのインスタンス
    private bool flytoundernow;         //強制着地中華
    private float time;                 //余分に上昇するように
    private const float settime = 2;
    private bool check;
    // Use this for initialization
    void Start()
    {
        goupdown = transform.parent.GetComponent<GoUpDown>();

    }

    private void Update()
    {
        if (check)
        {
            time += Time.deltaTime;
            if (time > settime)
            {
                time = 0;
                check = false;
                if (goupdown.undertofly)
                {
                    goupdown.undertofly = false;
                    goupdown.flymode = true;

                }
            }
             
        }
      

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
            if (goupdown.flymode)
            {
                goupdown.flymode = false;
                transform.parent.GetComponent<Rigidbody>().useGravity = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
            if (goupdown.undertofly && time == 0 && !check)
                check = true;
           
    }
}
