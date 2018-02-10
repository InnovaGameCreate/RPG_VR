using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIItemController : MonoBehaviour
{
    //アイテムの種類分(noneを除く)のビューポート(この下にcontentを生成)
    private GameObject[] viewports = new GameObject[Enum.GetValues(typeof(ItemBase.ItemType)).Length - 1];
    //contents(この下にボタンを追加していく)
    private GameObject[] contents = new GameObject[Enum.GetValues(typeof(ItemBase.ItemType)).Length - 1];
    //contentのプレファブ
    [SerializeField]
    private GameObject content_prefab;
    //アイテムボタンのプレファブ
    [SerializeField]
    private GameObject item_button_prefab;
    //バック
    private BackPack backpack;
    //更新時間管理用
    private float updateTime = 0;

    // Use this for initialization
    void Start()
    {
        //backpack取ってくる(片方で取ってこれない場合がある)
        backpack = GameManager.Instance.HEAD.GetComponentInChildren<BackPack>();
        if (backpack == null)
            backpack = GameManager.Instance.VRTKMANAGER.GetComponentInChildren<BackPack>();
        //各viewportの下にcontentを生成していく
        for (int i = 0; i < ItemBase.ItemTypeTotalNum; i++)
        {
            //viewport取得(contentの親)
            viewports[i] = GameObject.Find("ItemCanvas/TabPanel/Tab" + (i + 1) + "/Viewport/");
            //viewportの下にcontentを生成
            contents[i] = Instantiate(content_prefab, viewports[i].transform);
            //新しいcontentをスクロールバーに登録
            GameObject.Find("ItemCanvas/TabPanel/Tab" + (i + 1)).GetComponent<ScrollRect>().content = contents[i].GetComponent<RectTransform>();
        }
        ResetCanvas();
    }

    //キャンバスをリセットする
    private void ResetCanvas()
    {
        for (int i = 0; i < ItemBase.ItemTypeTotalNum; i++)
        {
            //contentを削除
            Destroy(contents[i]);
            //viewportの下にcontentを生成
            contents[i] = Instantiate(content_prefab, viewports[i].transform);
            //新しいcontentをスクロールバーに登録
            GameObject.Find("ItemCanvas/TabPanel/Tab" + (i + 1)).GetComponent<ScrollRect>().content = contents[i].GetComponent<RectTransform>();
        }
    }

    //アイテムをキャンバスに登録する
    private void UpdateAllItem()
    {
        ResetCanvas();
        int n = 0;
        foreach (ItemBase i in backpack.has_item) // 先頭から最後まで順番に表示
        {
            Debug.Log("UI:" + n);
            GameObject obj = Instantiate(item_button_prefab, contents[0].transform);
            obj.GetComponentInChildren<Text>().text = i.FlavorText;
            obj.GetComponent<ItemButton>().number = n;
            obj.GetComponent<Button>().onClick.AddListener(() => {
                backpack.UseItem(0);//要修正
            });
            obj.GetComponent<Button>().onClick.AddListener(UpdateAllItem);
            n++;
        }
    }

    private void PrintHello()
    {
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        updateTime += Time.deltaTime;
        if (updateTime > 1.0f)
        {
            ResetCanvas();
            UpdateAllItem();
            updateTime = 0.0f;
        }
    }
}
