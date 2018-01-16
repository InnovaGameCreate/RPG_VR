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

    //パラメータ
    [SerializeField]
    private DisplayParameters add;
    [SerializeField]
    private DisplayParameters multi;


    // Use this for initialization
    void Start()
    {
        //受バフのみ受け付ける
        //parameterクラスはインスペクター内で上にあるのがプラス
        //Parameters[] inspector_parameters = GetComponents<Parameters>();
        //paraSingle = inspector_parameters[0];
        //paraMagnification = inspector_parameters[1];

        paraSingle = new Parameters();
        paraMagnification = new Parameters();
        paraSingle = paraSingle + add;
        paraMagnification = paraMagnification + multi;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Buff operator +(Buff a, Buff b)
    {
        Buff re = new Buff();
        if (!b)
            Debug.Log("b");
        re.paraSingle = a.paraSingle + b.paraSingle;
        re.paraMagnification = a.paraMagnification * b.paraMagnification;
        return re;
    }


}
