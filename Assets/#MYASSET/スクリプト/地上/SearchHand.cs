using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SearchHand : MonoBehaviour
{
    public bool _SearchUP, _SearchDown,_SearchItem,_SearchShoulder;
    private bool running;
    private bool SkillAwake;
    private float timer;

    private List<bool> SkillFlags = new List<bool>();

    // Use this for initialization
    void Start()
    {
        _SearchUP = _SearchDown = false;
        _SearchItem = _SearchShoulder = false;

        running = false;
        SkillAwake = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (collider.gameObject.name == "Shoulder")
        {
            _SearchShoulder = true;
            Debug.Log("抜剣可");

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
        if (collider.gameObject.name == "Shoulder")
        {
            _SearchShoulder = false;
           // Debug.Log("おてて");
        }

      
}
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "WaistItem")
        {
            _SearchItem = false;
            //Debug.Log("おてて");
        }
        if (collider.gameObject.name == "Shoulder")
        {
            _SearchShoulder = false;
            // Debug.Log("おてて");
        }
    }

    public bool IsRangeItem()
    {
        return _SearchItem;
    }

    //public IEnumerator StayHand_UP(float SkillTime)//スキル発動までのタメ
    //{
    //    if(running || SkillAwake)//稼働中なら２つ目以降は破棄
    //        yield break;

    //    if (_SearchUP)
    //    {
    //        if (SkillTime <= timer)//タメ時間経過なら
    //        {

    //            SkillAwake = true;
    //        }
    //        Debug.Log("Stay");
    //        timer++;
    //        yield return new WaitForSeconds(1.0f);//まだならカウントを位置増やしもう一度
    //    }
    //    else//範囲外に出たなら初期化
    //    {
    //        running = false;
    //        SkillAwake = false;
    //        timer = 0;
    //        yield break;
    //    }
    //}

    //public bool IsSkillAwakeing()
    //{
    //    return SkillAwake;
    //}
}
