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
        //受バフのみ受け付ける
        //parameterクラスはインスペクター内で上にあるのがプラス
        Parameters[] inspector_parameters = GetComponents<Parameters>();
        paraSingle = inspector_parameters[0];
        paraMagnification = inspector_parameters[1];
    }

    // Update is called once per frame
    void Update()
    {

    }


    public static Buff operator +(Buff a, Buff b)//通常足し算
    {
        //Buff re = null;
        //if (b != null)
        //{
            Buff re = new Buff
            {
                //Debug.Log(a.paraSingle +"+"+ b.paraSingle);
                paraSingle = a.paraSingle + b.paraSingle,
                paraMagnification = a.paraMagnification * b.paraMagnification
            };
        //}
        //else
        //{
        //    re = new Buff
        //    {
                
        //        paraSingle = a.paraSingle ,
        //        paraMagnification = a.paraMagnification
        //    };
        //}
        
        return re;
    }

    

}
