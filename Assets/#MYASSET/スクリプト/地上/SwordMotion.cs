using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMotion : MonoBehaviour
{
    public GameObject Sword;
    private SearchHand Wherehand;
    public GameObject gripEve;
    private bool isEquip;
    public bool IsEquip { get { return isEquip; } }
    private int coolTime;
    private Weapon_Sword swordSystem;
    private Transform initSwordTrans;//剣の初期位置

    // Use this for initialization
    void Start()
    {

        Wherehand = GetComponent<SearchHand>();
        swordSystem = GetComponent<Weapon_Sword>();
        //swordSystem.validDrop = test.GetHashCode();
        coolTime = 100;
        //Sword.SetActive(false);
        initSwordTrans = Sword.transform.parent;//ソードスナップポイントの方の座標がとりあえず基準



    }

    // Update is called once per frame
    void Update()
    {
        //print(swordSystem.validDrop);
        //print(coolTime);
        //肩に右手をもっていったとき(毎回クールタイムを挟むのはゴリ押し）
        //↑のやつはナシでソードが触れている状態で握ったら
        if (/*Wherehand._SearchShoulder*/ swordSystem.enabled == true && gripEve.GetComponent<WaistItem>().gripped && coolTime == 100)
        {
            //装備していない状態でグリップを押したとき
            if (!isEquip)
            {
                //肩以外では絶対離さない
                swordSystem.validDrop = VRTK.VRTK_InteractableObject.ValidDropTypes.NoDrop;

                //Sword.SetActive(true);
                isEquip = true;
                //GetComponent<VRTK.Examples.Sword>().Grabbed(gripEve.GetComponent<VRTK.VRTK_InteractGrab>());
                //Sword.transform.parent = transform;
                //Sword.transform.position = transform.position + new Vector3(-0.01f, -0.035f, -0.026f);
                //Sword.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                //                                            transform.rotation.eulerAngles.y + 180,
                //                                            transform.rotation.eulerAngles.z);
                coolTime = 0;
                return;

            }
        }

        //離すときだけは肩の範囲を採用少しだけクールタイムでゴリ押し
        if (Wherehand._SearchShoulder && gripEve.GetComponent<WaistItem>().gripped && coolTime == 100)
        {
            if (isEquip)
            {
                //Sword.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                //                                            transform.rotation.eulerAngles.y - 180,
                //                                            transform.rotation.eulerAngles.z);
                //肩で押したら離す
                swordSystem.validDrop = VRTK.VRTK_InteractableObject.ValidDropTypes.DropAnywhere;
                //一応無理やりスクリプトを無効化
                //swordSystem.enabled = false;
                //位置を元に戻す
                Sword.transform.parent = initSwordTrans;
                Sword.transform.position = initSwordTrans.position;
                Sword.transform.rotation = initSwordTrans.rotation;
                isEquip = false;
                coolTime = 0;
                //GetComponent<VRTK.Examples.Sword>().Ungrabbed(gripEve.GetComponent<VRTK.VRTK_InteractGrab>());
                //Sword.SetActive(false);
                return;
            }
        }


        if (!isEquip)
        {

            Sword.transform.position = initSwordTrans.position;
            Sword.transform.rotation = initSwordTrans.rotation;
        }

        if (coolTime != 100)
            coolTime++;//着脱出来るまでのクールタイム（要はゴリ押し）



        if (coolTime != 100 && !isEquip)
        {
            //Sword.SetActive(false);
            //クールタイム中は掴めない
            swordSystem.GetComponent<Weapon_Sword>().isGrabbable = false;

        }

        if (coolTime == 100)
        {
            //Sword.SetActive(true);
            swordSystem.GetComponent<Weapon_Sword>().isGrabbable = true;
        }

        //装備中なら
        if (isEquip)
        {
        }
        //勝手にfalseにされて吹っ飛ぶので
        Sword.GetComponent<BoxCollider>().isTrigger = true;


    }
}
