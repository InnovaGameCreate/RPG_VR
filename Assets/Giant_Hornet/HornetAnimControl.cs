using UnityEngine;
using System.Collections;

public class HornetAnimControl : MonoBehaviour
{
    private Animator anim;  // Animator
    private float rotSpeed = 150; // Hornet rotation speed
    private float walkSpeed = 2;  // Hornet walk speed
    private bool fly = false; // Ground/Air bool

    // Use this for initialization
    void Start ()
	{
	    anim = GetComponent<Animator>(); // Get animator
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(KeyCode.W))   // Hornet walk forward animation + Vector3 translate
        {
            gameObject.transform.Translate(-Vector3.forward * Time.deltaTime * walkSpeed);
	        anim.SetFloat("forward", 1);
	    }

        if (Input.GetKeyUp(KeyCode.W))   // Stop walk forward animation when keyUp
        {
            anim.SetFloat("forward", 0);
        }


        if (Input.GetKey(KeyCode.S))   // Walk back animation + Vector3 translate
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            anim.SetFloat("back", 1);
        }

        if (Input.GetKeyUp(KeyCode.S)) //Stop walk back animation when keyUp
        {
            anim.SetFloat("back", 0);
        }

        if (Input.GetKey(KeyCode.A)) // Rotate our hornet left
            transform.Rotate(Vector3.up, -rotSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))  // Rotate our hornet right
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);


        


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) // Sprint animation + Vector3 translate
        {
            gameObject.transform.Translate(-Vector3.forward * Time.deltaTime * walkSpeed);
            anim.SetFloat("sprint", 1);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.LeftShift))  // Stop sprint animation
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            anim.SetFloat("sprint", 0);
        }


        if (Input.GetKeyDown(KeyCode.F) && fly == false)  // Enable "eating" animation
        {
            anim.SetTrigger("eating");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && fly == false)  // Enable "bite" animation
        {
            anim.SetTrigger("bite");
        }

        if (Input.GetKeyDown(KeyCode.Q) && fly == false)  // Enable "dead" animation
        {
            anim.SetTrigger("dead");
        }

        if (Input.GetKeyDown(KeyCode.E) && fly == false) // Enable "ground_hit" animation
        {
            anim.SetTrigger("ground_hit");
        }


        if (Input.GetKey(KeyCode.Space)) // Hold key for enable "Fly" animation
        {
            anim.SetBool("fly_up/down", true);
            fly = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))  // disable "Fly" animation
        {
            anim.SetBool("fly_up/down", false);
            fly = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && fly == true)  // Enable "fly_sting" animation only when you fly
        {
            anim.SetTrigger("fly_sting");
        }

        if (Input.GetKeyDown(KeyCode.Q) && fly == true)   // Enable "fly_dead" animation only when you fly
        {
            anim.SetTrigger("fly_dead");
        }

        if (Input.GetKeyDown(KeyCode.E) && fly == true)  // Enable "fly_hit" animation only when you fly
        {
            anim.SetTrigger("fly_hit");
        }




    }
}
