using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Text text;

    // Use this for initialization
    void Start()
    {
        //text
        text = transform.Find("Panel/TitleText").GetComponent<Text>();
        //チュートリアル開始
        StartCoroutine("StateUpdate");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "aaaaaaaaaa";
    }

    //IEnumerator StateUpdate()
    //{
    //    //1

    //    yield return new WaitUntil(Move);

    //    //Application.LoadLevel("maincamera");//LoadSceneが何故か使えないので旧形式で
    //}
}
