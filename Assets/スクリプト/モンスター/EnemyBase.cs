using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    //敵スーパークラス
    [SerializeField, TooltipAttribute("体力")]
    public int hp = 10;              //体力
    [SerializeField, TooltipAttribute("攻撃力")]
    public int atk;             //攻撃力
    [SerializeField, TooltipAttribute("攻撃速度 指定秒ごとに毎回攻撃")]
    public int atkspeed;    //攻撃速度      
    [SerializeField, TooltipAttribute("防御力")]
    public int def;         //防御力
    [SerializeField, TooltipAttribute("プレイヤー未発見時の自動移動距離(正方形の1辺の長さ)")]
    public int freemoverad = 2;    //自動移動距離限界
    [SerializeField, TooltipAttribute("プレイヤー未発見時の自動移動待機時間")]
    public int freemovewaittime = 3;
    [SerializeField, TooltipAttribute("プレイヤー発見時の追跡距離限界")]
    public int chaserad = 5;    //追跡距離限界

    protected Specialstatus status = Specialstatus.NONE;  //ステータス
    protected GameObject player;          //プレイヤー
    protected bool findplayer;              //プレイヤーを見つけたかどうか
    protected Vector3 []freemovepos=new Vector3[4];     //4角形に自由に動く位置
    protected float freeposdistance;            //自動移動目的地までの距離
    protected float playerdistance;            //追跡移動時プレイヤーまでの距離
    public Animator animator;            //アニメーターインスタンス
    protected float atkspeedcount;            //攻撃速度用カウンター
    protected int moveno = 0;     //自動移動目的地の番号
    protected const int stopdistance = 2;     //攻撃離れてる距離  speedよりも大きい数字
    protected const float enemyneardistance = 0.8f;  //敵との最小距離　これより近いと敵が離れていく
    protected bool arrive;          //自動移動時　目的地に到達したかどうか
    protected float breakForce = 150f;      //HP減少に必要な剣を振る速さ

    protected bool deaded = false;          //死んだかどうか

    protected enum Specialstatus{
        SPEEDUP,
        SPPEDDOWN,
        ATKUP,
        ATKDOWN,
        ATKSPEEDUP,
        ATKSPEEDDOWN,
        DEFUP,
        DEFDOWN,
        NONE
    }


    // Use this for initialization
    virtual protected void Start () {
		

        freemovepos[0] = new Vector3(transform.position.x + freemoverad, transform.position.y, transform.position.z + freemoverad);
        freemovepos[1] = new Vector3(transform.position.x - freemoverad, transform.position.y, transform.position.z + freemoverad);
        freemovepos[2] = new Vector3(transform.position.x - freemoverad, transform.position.y, transform.position.z - freemoverad);
        freemovepos[3] = new Vector3(transform.position.x + freemoverad, transform.position.y, transform.position.z - freemoverad);

        animator = GetComponent<Animator>();

        StartCoroutine("freeMove");

    
    }

    // Update is called once per frame
    virtual protected void Update () {
        
        if (hp <= 0)
        {
            dead();
            return;
        }
	}

    protected abstract IEnumerator freeMove();       //自由移動
    protected abstract IEnumerator chaseMove();      //追尾移動

    virtual protected void freeAnimation()           //自由移動時のアニメーション
    {
        if (arrive)
            animator.SetTrigger("Stay");//待機アニメーション
        else
            animator.SetTrigger("Move");//移動アニメーション
    }

    virtual protected void chaseAnimation()     //追跡移動時のアニメーション
    {
        if (playerdistance <= stopdistance)
            animator.SetTrigger("Attack"); //攻撃アニメーション
        else
            animator.SetTrigger("Move"); ;//移動アニメーション
    }

    virtual protected IEnumerator normalAttack()     //通常攻撃
    {
        while (findplayer)
        {
            if (playerdistance <= stopdistance)
                player.GetComponent<PlayerStatus>().damaged(atk);
            yield return new WaitForSeconds(atkspeed);
        }
    }
    protected abstract void specialAtack();   //特殊攻撃

    virtual protected void damaged()     //通常攻撃
    {
      
    }
    virtual protected void dead()//死亡
    {
        animator.SetTrigger("Dead");
        if (!deaded)
        {
            Destroy(this.gameObject, 5);
            deaded = true;
        }
    }
    virtual protected void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Player"))
        {
            if (player == null)
                player = other.gameObject;
            if (!findplayer)
            {
                findplayer = true;
               StartCoroutine("chaseMove");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionForce = GetCollisionForce(collision);
        Debug.Log(collision.gameObject.name);
        if (collisionForce > 0)
        {
            hp -= 1;
            animator.SetTrigger("Damage");
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
