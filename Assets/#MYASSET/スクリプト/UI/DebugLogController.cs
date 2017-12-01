using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogController : MonoBehaviour
{

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Scrollbar scrollbar;

    private void OnEnable()
    {
        Application.logMessageReceived += LogCallBackHandler;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= LogCallBackHandler;
    }

    // 取得したログの情報を処理する
    void LogCallBackHandler(string condition, string stackTrace, LogType type)
    {
        Text text = Instantiate(content, gameObject.transform).GetComponent<Text>();
        text.text = type + " " + condition;
        scrollbar.value = 0;//スクロールバーの位置を一番下にする
    }

    //呼び出しテスト
    private int num;
    IEnumerator RegisterTest()
    {
        while (true)
        {
            Debug.Log("test" + num++);
            Debug.LogWarning("warning");
            yield return new WaitForSeconds(5.0f);
        }
    }


    // Use this for initialization
    void Start()
    {
        //StartCoroutine(RegisterTest());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
