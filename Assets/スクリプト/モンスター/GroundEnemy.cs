using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : EnemyBase
{
    //敵のスピードはagentコンポーネントの方で設定

    private NavMeshAgent agent;                     //ナビメッシュ
    private bool look;
    // Use this for initialization
    protected override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        base.Start();
        agent.SetDestination(freemovepos[moveno]);    //ルート決定
        agent.stoppingDistance=stopdistance;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (findplayer&& player != null)
        {
            playerdistance = Vector3.Distance(transform.position, player.transform.position);


            Vector3 playerdir = player.transform.position - transform.position;
            playerdir = playerdir.normalized;
            //プレイヤが敵に近すぎた時距離をとる
            if (playerdistance <= enemyneardistance)
            {
                transform.position = transform.position - playerdir * Time.deltaTime * agent.speed /2;
            }
            //攻撃中プレイヤーを向くように
            if (playerdistance <= stopdistance)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(playerdir.x, 0, playerdir.z));
            }

            //追跡距離限界
            if (playerdistance > chaserad)
            {
                findplayer = false;
                StartCoroutine("freeMove");
                agent.SetDestination(freemovepos[moveno]);    //ルート決定
            }
            chaseAnimation();


        }
        else
        {
            freeposdistance = Vector2.Distance(new Vector2(freemovepos[moveno].x, freemovepos[moveno].z), new Vector2(transform.position.x, transform.position.z));
            freeAnimation();
        }
    }

    protected override IEnumerator freeMove()
    {
        while (!findplayer)
        {
                
            if( freeposdistance <= agent.speed)
            {
                arrive = true;
                yield return new WaitForSeconds(freemovewaittime);
                moveno++;
                moveno %= freemovepos.Length;
                agent.SetDestination(freemovepos[moveno]);    //ルート決定
                arrive = false;     
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override IEnumerator chaseMove()
    {
        StartCoroutine("normalAttack");
        while (findplayer)
        {          
           agent.SetDestination(player.transform.position);    //ルート決定
 
           
           yield return new WaitForSeconds(0.2f);
        }
    }


    
    protected override void specialAtack()
    {
    }

    protected override void dead()
    {
        base.dead();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
