using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SearchHand : MonoBehaviour
{
    public bool _SearchUP, _SearchDown,_SearchItem;
    // Use this for initialization
    void Start()
    {
        _SearchUP = _SearchDown = false;
        _SearchItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("mmmmmmmmaammmmmmmmmmmmmmaaaaaaaaaaaa");
    }

    public void OnTriggerStay(Collider collider)
    {
        //Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name == "SkillZone1")
        {
            _SearchUP = true;
            //Debug.Log("おてて");
        }
        if (collider.gameObject.name == "SkillZone2")
        {
            _SearchDown = true;
            //Debug.Log("おてて");

        }
        if (collider.gameObject.name == "WaistItem")
        {
            _SearchItem = true;
           Debug.Log("腰アイテム");

        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "SkillZone1")
        {
            _SearchUP = false;
        }
        if (collider.gameObject.name == "SkillZone2")
        {
            _SearchDown = false;
            //Debug.Log("おてて");
        }
        if (collider.gameObject.name == "WaistItem")
        {
            _SearchItem = false;
            //Debug.Log("おてて");
        }
    }

    public bool IsRangeItem()
    {
        return _SearchItem;
    }

}
