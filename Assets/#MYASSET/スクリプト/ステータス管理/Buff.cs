using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    //プラス
    Parameters paraSingle;
    public Parameters ParaSingle
    {
        get { return paraSingle; }
    }
    //倍率
    Parameters paraMagnification;
    public Parameters ParaMagnification
    {
        get { return paraMagnification; }
    }
    //有効時間
    int availableSeconds;
    public int AvailableSeconds
    {
        get { return availableSeconds; }
        set { availableSeconds = value; }
    }


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
        re.paraSingle = a.paraSingle + b.paraSingle;
        re.paraMagnification = a.paraMagnification * b.paraMagnification;
        return re;
    }


}
