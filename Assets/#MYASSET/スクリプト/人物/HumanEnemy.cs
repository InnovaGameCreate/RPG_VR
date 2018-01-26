using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEnemy : HumanBase
{
    [SerializeField, TooltipAttribute("ドロップアイテム")]
    public GameObject[] dropitem;
    private const float breakForce = 150f;      //HP減少に必要な剣を振る速さ.

    protected bool deaded = false;          //死んだかどうか
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Status.Parameter.HP);
        if (Status.Parameter.HP <= 0 || Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Dead");
            Destroy(GetComponent<BehaviorTree>());
            StartCoroutine("Remove");
            return;
        }
    }

    // モデル消滅
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(60);
        Destroy(this.gameObject);
        dropItem();
    }

    //ドロップアイテム生成
    void dropItem()
    {
        //var canvasObject = new GameObject("Canvas");
        //var canvas = canvasObject.AddComponent<Canvas>();
        //canvasObject.AddComponent<GraphicRaycaster>();
        //canvas.renderMode = RenderMode.WorldSpace;
        //canvasObject.transform.position = transform.position;


        ////乱数用
        //int rand = (int)Random.Range(0, dropitem.Length);
        //if (rand == dropitem.Length)
        //    rand = 0;

        //// プレハブからインスタンスを生成
        //GameObject obj = Instantiate(dropitem[rand], Vector3.zero, Quaternion.identity);
        //obj.GetComponent<ItemBase>().droppeditem = true;
        //// 作成したオブジェクトを子として登録
        //obj.transform.parent = canvas.transform;

        //obj.AddComponent<BoxCollider>();
        //obj.GetComponent<BoxCollider>().size = new Vector3(100, 100, 20);
        //obj.AddComponent<origingravity>();
        //canvas.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
    }

    //ダメージを受けた際のアニメーションは攻撃を受けた側(つまり人物クラス　　　ダメージ計算を呼ぶのはダメージを与えた側(つまり武器クラスから
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weapon>())
        {

            var collisionForce = GetCollisionForce(collision);
            if (collisionForce > 0)
            {
                animator.SetTrigger("Damaged");
            }
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
