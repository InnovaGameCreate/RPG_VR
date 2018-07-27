using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NPCAnimation : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animatorRun;
    

    void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
        animatorRun = GetComponent<Animator>();
       
    }


    void Update()
    {
        
            animatorRun.SetFloat("Speed", agent.velocity.sqrMagnitude);
        
        
    }
}
