using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float forceJump = 2f;

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
        } 
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);            
        }  

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= friction;
        }
        else if(myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            myRigidbody.velocity = Vector2.up * forceJump;
        } 
    }
}
