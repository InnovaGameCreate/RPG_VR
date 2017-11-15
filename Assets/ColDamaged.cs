using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColDamaged : MonoBehaviour {
    Animator animator;
    private float breakForce = 150f;      //HP減少に必要な剣を振る速さ
    private void Start()
    {
        animator = transform.parent.GetComponent<EnemyBase>().animator;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var collisionForce = GetCollisionForce(collision);
        Debug.Log(collision.gameObject.name);
        if (collisionForce > 0)
        {
         
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
