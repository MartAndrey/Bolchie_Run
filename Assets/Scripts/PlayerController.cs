using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Force with which the player jumps
    public float jumpForce = 6f;

    // Component responsible for responding to and handling physics
    Rigidbody2D rigidBody;

    //Soil defining layer
    public LayerMask groundMask;

    // Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            Jump();
        }
    }

    // Jump method
    void Jump()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  
        }
    } 

    //Method to detect if the player is on the ground or not
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 2f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
