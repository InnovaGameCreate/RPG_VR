using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour {
    [SerializeField, TooltipAttribute("体力")]
    public int hp = 100;        //体力
    [SerializeField, TooltipAttribute("攻撃力")]
    public int atk = 10;        //攻撃力
    [SerializeField, TooltipAttribute("ドロップアイテム")]
    public GameObject []dropitem;
    private const float breakForce = 150f;      //HP減少に必要な剣を振る速さ.
    private Animator animator;            //アニメーターインスタンス
    protected bool deaded = false;          //死んだかどうか
  
    void Start()
    {
  
        animator = GetComponent<Animator>();
    
    }
    private void Update()
    {
        if (hp <= 0)
        {
            animator.SetTrigger("Dead");
            return;
        }
    }

    //ドロップアイテム生成
    void dropItem()
    {
        var canvasObject = new GameObject("Canvas");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvasObject.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvasObject.transform.position = transform.position;


        //乱数用
        int rand = (int)Random.Range(0,dropitem.Length);
        if (rand == dropitem.Length)
            rand = 0;
 
        // プレハブからインスタンスを生成
        GameObject obj = Instantiate(dropitem[rand], Vector3.zero, Quaternion.identity);
        obj.GetComponent<ItemBase>().droppeditem = true;
        // 作成したオブジェクトを子として登録
        obj.transform.parent = canvas.transform;

        obj.AddComponent<BoxCollider>();
        obj.GetComponent<BoxCollider>().size = new Vector3(100, 100, 20);
        obj.AddComponent<origingravity>();
        canvas.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
    }
     void dead()//死亡
    {
     
        if (!deaded)
        {
            Destroy(this.gameObject);
            deaded = true;
            dropItem();
           
        }
    }

    void attack()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        var collisionForce = GetCollisionForce(collision);
        Debug.Log(collision.gameObject.name);
        if (collisionForce > 0)
        {
            GetComponent<Animator>().SetTrigger("Damaged");
        }
    }

    private float GetCollisionForce(Collision collision)
    {
        if ((collision.collider.name.Contains("Sword") && collision.collider.GetComponent<VRTK.Examples.Sword>().CollisionForce() > breakForce))
        {
            return collision.collider.GetComponent<VRTK.Examples.Sword>().CollisionForce() * 1.2f;

        }

        if (collision.collider.name.Contains("Arrow"))
        {
            return 500f;
        }

        return 0f;
    }
}
