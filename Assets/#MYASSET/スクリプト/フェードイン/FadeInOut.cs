using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
    Image img;
    float time;
    const float finishTime = 3.0f;      //暗転が終わるまでの時間
    public enum Condition
    {
        FADEIN,
        FADEOUT,
        SCENECHANGE,
        STAY
    };
    private Condition cod;      //状態

    public Condition CONDITION 
    {
        get { return cod; }
        set { cod = cod != Condition.SCENECHANGE ? value : cod; }
    }

    // Use this for initialization
    void Start () {
        img=GetComponent<Image>();
        img.color = new Color(0,0,0,1);
        cod = Condition.FADEOUT;

    }
	
	// Update is called once per frame
	void Update () {
        if (cod != Condition.STAY && cod != Condition.SCENECHANGE)
        {
            if (img.color.a >= 0 && img.color.a <= 1)
            {
                if (cod == Condition.FADEOUT)
                    time += Time.deltaTime;
                else
                    time -= Time.deltaTime;
            }
            else
            {
                if (cod == Condition.FADEOUT)
                {
                    time = finishTime;
                    cod = Condition.STAY;
                }
                else
                {
                    cod = Condition.SCENECHANGE;
                }
            }
          
            img.color = new Color(0, 0, 0, 1 - time / finishTime);
        }
    }
 

}
