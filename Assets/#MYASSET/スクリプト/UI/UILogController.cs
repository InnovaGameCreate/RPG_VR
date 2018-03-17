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

    private const int maxlogs = 5;     //最大ログ履歴数
    // 取得したログの情報を処理する
    public void RegisterLog(string str, Color color)
    {
        Text text = Instantiate(textPrefab, gameObject.transform).GetComponent<Text>();
        text.text = str;
        text.color = color;


        scrollbar.value = 0;//スクロールバーの位置を一番下にする

        int ObjCount = this.transform.childCount;
        if (ObjCount > maxlogs)
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
                break;
            }


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
