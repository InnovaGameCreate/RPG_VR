using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicParticleCollider : MonoBehaviour {
    [SerializeField, TooltipAttribute("攻撃力")]
    public int atk = 10;      //攻撃力
    void OnParticleCollision(GameObject objct)
    {      
        //処理内容
        //例）衝突したオブジェクトタグがenemyだった場合、オブジェクトを破壊する
            if (objct.gameObject.GetComponent<EnemyBase>() != null) {
               Destroy(this.gameObject);
                objct.gameObject.GetComponent<EnemyBase>().hp -= atk;
            }
    }
}
