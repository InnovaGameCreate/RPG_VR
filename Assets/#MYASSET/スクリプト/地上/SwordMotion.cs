using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMotion : MonoBehaviour {
    public GameObject Sword;
    private SearchHand Wherehand;
    public GameObject gripEve;
    private bool isEquip;
    private int coolTime;

    // Use this for initialization
    void Start () {

        Wherehand = GetComponent<SearchHand>();
        coolTime = 100;
        Sword.SetActive(false);

    }

    // Update is called once per frame
    void Update ()
    {
        //肩に右手をもっていったとき(毎回クールタイムを挟むのはゴリ押し）
        if (Wherehand._SearchShoulder && gripEve.GetComponent<WaistItem>().gripped && coolTime == 100)
        {
            //装備していない状態でグリップを押したとき
            if (!isEquip)
            {
                Sword.SetActive(true);
                isEquip = true;
                Sword.transform.parent = transform;
                Sword.transform.position = transform.position + new Vector3(-0.01f, -0.035f, -0.026f);
                Sword.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 6.421f,
                                                            transform.rotation.eulerAngles.y + 0,
                                                            transform.rotation.eulerAngles.z + 0);
                coolTime = 0;
                return;

            }

            
            if(isEquip)
            {
                isEquip = false;
                coolTime = 0;
                Sword.SetActive(false);
                return;
            }
        }


        if(coolTime != 100)
            coolTime++;//着脱出来るまでのクールタイム（要はゴリ押し）
        //装備中なら
        if (isEquip)
        {
        }

        
    }
}
