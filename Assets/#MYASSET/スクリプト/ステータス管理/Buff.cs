using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    Parameters ParaSingle;          //プラス
    Parameters ParaMagnification;   //倍率


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Buff operator +(Buff a, Buff b)
    {
        Buff re = new Buff();
        re.ParaSingle = a.ParaSingle + b.ParaSingle;
        re.ParaMagnification = a.ParaMagnification * b.ParaMagnification;
        return re;
    }
}
