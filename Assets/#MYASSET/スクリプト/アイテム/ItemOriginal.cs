using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOriginal : MonoBehaviour
{
    public const int id_max = 100;//idの最大値

    [SerializeField]
    private GameObject[] orig_item = new GameObject[id_max];//アイテムオブジェクトのオリジナル
    [System.NonSerialized]
    public GameObject[] gen_item = new GameObject[id_max];//生成したアイテム

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < id_max; i++)
        {
            if (orig_item[i] != null)
            {
                gen_item[i] = Instantiate(orig_item[i], gameObject.transform);
                gen_item[i].GetComponent<ItemBase>().ID = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
