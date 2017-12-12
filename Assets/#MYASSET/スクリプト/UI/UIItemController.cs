using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    // Use this for initialization
    void Start()
    {
        //各viewportの下にcontentを生成していく
        for (int i = 0; i < ItemBase.ItemTypeTotalNum; i++)
        {
            viewports[i] = GameObject.Find("ItemCanvas/TabPanel/Tab" + (i + 1) + "/Viewport/");
            contents[i] = Instantiate(content_prefab, viewports[i].transform);
        }
        ResetCanvas();
    }

    //アイテムキャンバスの更新
    void UpdateCanvas()
    {

    }

    //キャンバスをリセットする
    private void ResetCanvas()
    {
        for (int i = 0; i < ItemBase.ItemTypeTotalNum; i++)
        {
            Destroy(contents[i]);
            contents[i] = Instantiate(content_prefab, viewports[i].transform);
        }
    }

    //アイテムをキャンバスに登録する
    private void RegisterItem()
    {
        for(int i = 0; i < ItemOriginal.id_max; i++)
        {
            //if (BackPack.) ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(item_button_prefab, contents[0].transform);
    }
}
