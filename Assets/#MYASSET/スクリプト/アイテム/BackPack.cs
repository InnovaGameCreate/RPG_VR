using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackPack : MonoBehaviour
{


    struct Item
    {
        public ItemBase item_base;
        public int num;
    }

    private const int MAXHASNUM = 99;      //アイテム最大所持数
    private Item[] has_item = new Item[ItemOriginal.id_max];//所持しているアイテムの情報


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //アイテムを獲得する
    public void AcquireItem(int id)
    {
        has_item[id].num++;
    }
}
