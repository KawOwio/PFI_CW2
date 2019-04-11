using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float topSpeed = 15.0f;      //Speed
    Rigidbody myRigidbody;


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Get move direction
        float move = -Input.GetAxis("Horizontal");

        //Add velocity
        myRigidbody.velocity = new Vector3(0, 0, move * topSpeed * Time.deltaTime);
    }

    //Change to collision with enemies
    //Checks for collision with the ground
    private void OnCollisionEnter2D(Collision2D groundBlock)
    {
        if (groundBlock.gameObject.tag == "Ground")
        {
            //death
        }
    }
}
