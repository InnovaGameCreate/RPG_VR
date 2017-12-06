using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;


public class _Sword : MonoBehaviour {

    public GameObject isShoulder;
    public GameObject head;
   // public GameObject hand;
    public GameObject rightHand;
    public GameObject Sword;
    // private VRTK.Examples.Sword flag;

    public GameObject gripEve;
    private int once = 0;
    private bool isGrab;
    private SearchHand Wherehand;
    private bool grabbingSw = false;
	// Use this for initialization
	void Start () {
       // hand = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/GroundAttackpointR");
        //print(hand);
        Wherehand = rightHand.GetComponent<SearchHand>();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

    }

    // Update is called once per frame
    void Update ()
    {

        print(once);

        if (GetComponent<VRTK.Examples.Sword>().enabled == true && Wherehand._SearchShoulder == true/* && gripEve.GetComponent<WaistItem>().gripped*/)
        {
            GetComponent<VRTK.Examples.Sword>().enabled = false;
            Destroy(GetComponent<FixedJoint>());

        }
        else if (once == 1 && GetComponent<VRTK.Examples.Sword>().enabled == false && Wherehand._SearchShoulder == true && gripEve.GetComponent<WaistItem>().gripped)
        {

            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<VRTK.Examples.Sword>().enabled = true;
            once = 0;
            gameObject.AddComponent<FixedJoint>();
            this.GetComponent<FixedJoint>().connectedBody = rightHand.GetComponent<Rigidbody>();


        }
        else if (GetComponent<VRTK.Examples.Sword>().enabled == false )
        {
            this.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.2f, head.transform.position.z - 0.4f);
            this.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            once = 1;
        }
       

        isShoulder.transform.position = new Vector3(head.transform.position.x, head.transform.position.y + 0.4f, head.transform.position.z);

        
    }
}
