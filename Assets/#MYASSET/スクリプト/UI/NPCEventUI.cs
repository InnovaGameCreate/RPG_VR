using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCEventUI : MonoBehaviour {

   public enum EeventMarkState
    {
        eExclamationText,
        eQuestionText,
        eNone,
    };

        
    [SerializeField]
    private Transform playerTransform;//プレイヤーの位置
    [SerializeField]
    private Canvas eventUI;//！とか？とか
    public EeventMarkState markState = EeventMarkState.eNone; //最初は何も表示させない

    // Use this foMr initialization
    void Start ()
    {

       
	}
	
	// Update is called once per frame
	void Update () {
        eventUI.transform.LookAt(playerTransform);//プレイヤーのほうへ向く



        //マークの状態によって！か？か無しかを切り替える
        if(markState == EeventMarkState.eExclamationText)
            eventUI.transform.Find("!Text").GetComponent<Text>().text = "!";
        if (markState == EeventMarkState.eQuestionText)
            eventUI.transform.Find("!Text").GetComponent<Text>().text = "?";
        if (markState == EeventMarkState.eNone)
            eventUI.transform.Find("!Text").GetComponent<Text>().text = "";



    }
}
