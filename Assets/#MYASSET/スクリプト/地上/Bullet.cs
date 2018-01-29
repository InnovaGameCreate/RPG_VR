using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    private Status bulletstatus = new Status();
    private List<Buff> _send = new List<Buff>();
    //private List<Buff> _receive = new List<Buff>();
    private int AtkPower;//威力
    private float Speed;//弾速
    private float BreakTime;//玉の消える時間

    public List<Buff> SENDBUFF
    {
        get { return _send; }
        set { _send = value; }
    }
    //public List<Buff> RECEIVEBUFF
    //{
    //    get { return _receive; }
    //    set { _receive = value; }
    //}
    public Status BulletStatus//コピーされた攻撃者のステータス格納
    {
        get { return bulletstatus; }
        set { bulletstatus = value; }
    }
    public float B_SPEED
    {
        get { return Speed; }
        set { Speed = value; }
    }
    public float B_BREAKTIME
    {
        get { return BreakTime; }
        set { BreakTime = value; }
    }
    public int B_POWER
    {
        get { return AtkPower; }
        set { AtkPower = value; }
    }

    private float timer;
    private Rigidbody rig;

    // Use this for initialization
    void Start () {
        timer = 0;

        //bulletstatus = new Status();
        rig = GetComponent<Rigidbody>();

        rig.AddForce(transform.forward * Speed);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= BreakTime)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "enemy")
        {
            //Debug.Log("HHHHIIIITTTTTTTTT!!!!!!!!!!!!");
            //Status st = GetComponentInParent<HumanBase>().Status;

            if (coll.gameObject.GetComponent<HumanBase>() == null)//まだアタッチされてないのでnull
            {
                //Debug.Log("HumanBase");
            }
            //if (coll.gameObject.GetComponent<HumanBase>().CounterBuff == null)//カウンターバフの値取ろうとするとエラー発生
            //{
            //    Debug.Log("Counter");
            //}
            //Debug.Log("MagATK" + bulletstatus.Parameter.MAGICATK);
            DamageCalculate dam = new DamageCalculate(bulletstatus, AtkPower, true, _send, null/*coll.gameObject.GetComponent<HumanBase>().CounterBuff*/);
        }
    }
}
