using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class BackPack : MonoBehaviour
{


    struct Item
    {
        public ItemBase item_base;
        public int num;
    }

    private const int MAXHASNUM = 99;      //アイテム最大所持数
    public List<ItemBase> has_item = new List<ItemBase>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    //アイテムを獲得する
    public void AcquireItem(ItemBase item)
    {
        has_item.Add(item);
    }

    //アイテムを使う
    public void UseItem(int i)
    {
        Debug.Log("Used:"+i);
        has_item[i].ItemUse();
        has_item.RemoveAt(i);
    }
}
