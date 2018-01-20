using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SearchHand : MonoBehaviour
{
    public bool _SearchR, _SearchL, _SearchItem,_SearchShoulder;
    //private bool running;
    //private bool SkillAwake;
    //private float timer;

    private List<bool> SkillFlags = new List<bool>();

    // Use this for initialization
    void Start()
    {
        _SearchR = _SearchL = false;
        _SearchItem = _SearchShoulder = false;

        //running = false;
        //SkillAwake = false;
        //timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        //右側
        if (collider.gameObject.name == "GroundAttackpointR")
        {
            _SearchR = true;
            
        }
        if (collider.gameObject.name == "GroundAttackpointL")
        {
            _SearchL = true;
            

        }
        if (collider.gameObject.name == "WaistItem")
        {
            _SearchItem = true;
           Debug.Log("腰アイテム");

        }
        if (collider.gameObject.name == "Shoulder")
        {
            _SearchShoulder = true;


        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "GroundAttackpointR")
        {
            _SearchR = false;
            
        }
        if (collider.gameObject.name == "GroundAttackpointL")
        {
            _SearchL = false;
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
    //public void OnTriggerExit(Collider collider)
    //{
    //    if (collider.gameObject.name == "WaistItem")
    //    {
    //        _SearchItem = false;
    //        //Debug.Log("おてて");
    //    }
    //    if (collider.gameObject.name == "Shoulder")
    //    {
    //        _SearchShoulder = false;
    //        // Debug.Log("おてて");
    //    }
    //}

    public bool IsRangeItem()
    {
        return _SearchItem;
    }

    //スキルスーパークラスで移植できてしまったので凍結
    //public IEnumerator StayHand_UP(float SkillTime)//次イノベで確認すべき
    //{
    //    if (running /*|| SkillAwake*/)//稼働中なら２つ目以降は破棄(現在解除中)
    //        yield break;

    //    running = true;

    //    if (_SearchUP)
    //    {
    //        timer += 1.0f;
    //        Debug.Log(timer + ":SKILL!!!!!!");
    //        yield return new WaitForSeconds(1.0f);//まだならカウントを位置増やしもう一度
    //    }
    //    else
    //    {
    //        timer = 0;
    //        running = false;
    //        SkillAwake = false;
    //        yield break;
    //    }

    //}



    //public bool IsSkillAwakeing()
    //{
    //    return SkillAwake;
    //}
}
