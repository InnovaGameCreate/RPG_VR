using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicParticleCollider : MonoBehaviour {
    void OnParticleCollision(GameObject objct)
    {

        Debug.Log("aaaa");
        //処理内容
        //例）衝突したオブジェクトタグがenemyだった場合、オブジェクトを破壊する
        if (objct.gameObject.tag == "enemy")
        {
            Object.Destroy(gameObject);
        }
    }
}
