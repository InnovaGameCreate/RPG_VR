using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogController : MonoBehaviour
{
    [SerializeField]
    private GameObject textPrefab;
    [SerializeField]
    private Scrollbar scrollbar;


    // 取得したログの情報を処理する
    public void RegisterLog(string str){
        Text text = Instantiate(textPrefab, gameObject.transform).GetComponent<Text>();
        text.text = str;
        scrollbar.value = 0;//スクロールバーの位置を一番下にする
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
