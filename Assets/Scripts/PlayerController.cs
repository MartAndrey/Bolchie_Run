using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables que son igualadas a los parametros del control de animador 
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    // Force with which the player jumps
    public float jumpForce = 50f;

    //Lightning distance to detect if we are touching the ground
    public float rayDistance = 1.8f;

    //Soil defining layer
    public LayerMask groundMask;

    // Component responsible for responding to and handling physics
    Rigidbody2D rigidBody;
    
    //Get the player animator
    Animator animator;

    // Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());


        //--------------------------------------------------------------------------------------------//
        Debug.DrawRay(this.transform.position, Vector2.down * rayDistance, Color.white);
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
        if (Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
