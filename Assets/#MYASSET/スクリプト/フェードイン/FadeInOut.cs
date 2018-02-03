using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
    float time;
    const float finishTime = 3.0f;      //暗転が終わるまでの時間
    [SerializeField, TooltipAttribute("フェードインアウト用イメージを選択")]
    private Image fadeInOutImage;         
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
        set { cod = value; }
    }

    // Use this for initialization
    void Start () {
        fadeInOutImage.color = new Color(0,0,0,1);
        cod = Condition.FADEOUT;

    }
	
	// Update is called once per frame
	void Update () {
        if (cod != Condition.STAY && cod != Condition.SCENECHANGE)
        {
            if (fadeInOutImage.color.a >= 0 && fadeInOutImage.color.a <= 1)
            {
                //明るくなる
                if (cod == Condition.FADEOUT)
                    time += Time.deltaTime;
                //暗くなる
                else if(cod == Condition.FADEIN)
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
                    time = 0;
                }
            }
          Debug.Log("a  "+ cod);
            fadeInOutImage.color = new Color(0, 0, 0, 1 - time / finishTime);
        }
    }
 

}
