using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPoint : MonoBehaviour {
    public enum USEHAND
    {
        Left,
        Right,
        None
    }

    private USEHAND usehand;
    private void Start()
    {
        if (transform.parent.name == "Right")
            usehand = USEHAND.Right;
        else
            usehand = USEHAND.Left;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.CompareTag("MagicHand"))
        {
            if((other.gameObject.name== "MagicAttackpointL"&& usehand==USEHAND.Left)|| (other.gameObject.name == "MagicAttackpointR" && usehand == USEHAND.Right))
                GetComponent<MeshRenderer>().enabled = false;
        }

           
    }
}
