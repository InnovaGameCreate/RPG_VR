using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyBase
{
    [SerializeField, TooltipAttribute("移動速度")]
    public float speed = 2;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (findplayer)
        {
            playerdistance = Vector3.Distance(transform.position, player.transform.position);
            transform.LookAt(player.transform.position);
            //追跡距離限界
            if (playerdistance > chaserad)
            {
                findplayer = false;
                StartCoroutine("freeMove");
                transform.LookAt(freemovepos[moveno]);
            }

            var aim = player.transform.position - transform.position;         //目的地までの差
            aim = aim.normalized;
            //プレイヤが敵に近すぎた時距離をとる
            if (playerdistance <= enemyneardistance)
            {
                transform.position = transform.position - aim * Time.deltaTime * speed / 2;
            }

            if (playerdistance > stopdistance)
            {
                transform.position = transform.position + aim.normalized * Time.deltaTime * speed;
            }

            chaseAnimation();
        }
        else
        {
            freeposdistance = Vector3.Distance(freemovepos[moveno], transform.position);
            freeAnimation();

            if (freeposdistance > speed)
            {
                var aim = freemovepos[moveno] - transform.position;         //目的地までの差
                transform.position = transform.position + aim.normalized * Time.deltaTime * speed;
            }
        }
    }

    protected override IEnumerator chaseMove()
    {
        StartCoroutine("normalAttack");
        yield return null;

    }

    protected override void dead()
    {
        base.dead();
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    protected override IEnumerator freeMove()
    {
        while (!findplayer)
        {
            if (freeposdistance <= speed)
            {
                arrive = true;
                yield return new WaitForSeconds(freemovewaittime);
                moveno++;
                moveno %= freemovepos.Length;
                transform.LookAt(freemovepos[moveno]);
                arrive = false;
            }

           yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void specialAtack()
    {

    }
}
