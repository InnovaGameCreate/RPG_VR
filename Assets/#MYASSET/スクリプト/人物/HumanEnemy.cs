using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanEnemy : HumanBase
{
    [SerializeField, TooltipAttribute("ドロップアイテム")]
    public GameObject[] dropitem;
    [SerializeField, TooltipAttribute("取得経験値量")]
    private int Experience;
    private const float breakForce = 150f;      //HP減少に必要な剣を振る速さ.

    protected bool deaded = false;          //死んだかどうか


    private BehaviorTree tree;

    [SerializeField, TooltipAttribute("HPバー")]
    private Slider hpSlider;      //体力バー

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        tree = GetComponent<BehaviorTree>();
    }

    void calculateVar(Slider target, int now, float max)
    {
        //体力バー計算
        float var = now / max;
        if (var > 1)
        {
            // 最大を超えたら0に戻す
            var = 0;
        }
        if (hpSlider != null)
            // HPゲージに値を設定
            target.value = var;
    }


    // Update is called once per frame
    void Update()
    {
        //死んだ時
        if (!deaded&&(Status.Parameter.HP <= 0 || Input.GetKeyDown(KeyCode.K)))
        {
            deaded = true;
            animator.SetTrigger("Dead");
            Destroy(GetComponent<BehaviorTree>());
            StartCoroutine("Remove");
            //敵の場合親の名前を引数に渡す
            GameManager.Instance.QMANAGER.CheckQuestAchievement(transform.parent.name);
            Destroy(hpSlider.transform.parent.gameObject);
            return;
        }
        else
        {
            //Debug.Log(Status.Parameter.ATK);
            //tree.SetVariable("FreeSpeed", (int)(Status.Parameter.SPEED) );//intに丸めました
            //tree.SetVariableValue("ChaseSpeed", (int)(Status.Parameter.SPEED));
        }
        calculateVar(hpSlider, Status.Parameter.HP, (float)Status.Parameter.MAXHP);
        Status.Parameter.HP = Status.Parameter.HP - (1);
    }

    // モデル消滅
    IEnumerator Remove()
    {
        dropItem();
        yield return new WaitForSeconds(60);
        Destroy(this.gameObject);
     
    }

    //ドロップアイテム生成
    void dropItem()
    {
        //var canvasObject = new GameObject("Canvas");
        //var canvas = canvasObject.AddComponent<Canvas>();
        //canvasObject.AddComponent<GraphicRaycaster>();
        //canvas.renderMode = RenderMode.WorldSpace;
        //canvasObject.transform.position = transform.position;


        //乱数用
        int rand = (int)Random.Range(0, dropitem.Length);
        if (rand == dropitem.Length)
            rand = 0;

        // プレハブからインスタンスを生成
        //GameObject obj = Instantiate(dropitem[rand], transform.position, dropitem[rand].transform.rotation);
        //obj.GetComponent<ItemBase>().droppeditem = true;
        //obj.GetComponent<Rigidbody>().GetComponent<Rigidbody>().angularVelocity = Vector3.up * Mathf.PI;

        //経験値の為、プレイヤークラス取得
        GameManager.Instance.PLAYER.EXP += Experience;

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
        if (collision.gameObject.GetComponent<Weapon_Sword>())
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
        if ((collision.collider.name.Contains("Sword") && collision.collider.GetComponent<Weapon_Sword>().CollisionForce() > breakForce))
        {
            return collision.collider.GetComponent<Weapon_Sword>().CollisionForce() * 1.2f;

        }

        if (collision.collider.name.Contains("Arrow"))
        {
            return 500f;
        }

        return 0f;
    }

}
