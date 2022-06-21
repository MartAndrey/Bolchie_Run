using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Singleton PlayerController
    public static PlayerController sharedInstancePC;

    //Variables that are set equal to the parameters of the animator control
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_IS_FALLING = "isFalling";
    const string STATE_IS_MOVING = "isMoving";

    // Force with which the player jumps
    public float jumpForce = 50f;

    //Player speed
    public float speed = 10f;

    //Lightning distance to detect if we are touching the ground
    public float rayDistance = 3.5f;

    //Soil defining layer
    public LayerMask groundMask;

    // Component responsible for responding to and handling physics
    Rigidbody2D rigidBody;

    //Get the player animator
    Animator animator;

    //The one in charge of rendering the sprite.
    SpriteRenderer spriteRender;

    // Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        if (sharedInstancePC == null)
        {
            sharedInstancePC = this;
        }

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_IS_FALLING, true);
        animator.SetBool(STATE_IS_MOVING, true);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        animator.SetBool(STATE_IS_FALLING, IsFalling());
        animator.SetBool(STATE_IS_MOVING, IsMoving());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


        //--------------------------------------------------------------------------------------------//
        Debug.DrawRay(this.transform.position, Vector2.down * rayDistance, Color.white);
    }

    //It is called every fixed frame-rate frame
    void FixedUpdate()
    {
        Move();
    }

    //Player Jump method
    void Jump()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //Player Movement Method
    void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(speed * horizontalAxis, rigidBody.velocity.y);

        if (horizontalAxis < 0)
        {
            spriteRender.flipX = true;
        }
        else if (horizontalAxis > 0)
        {
            spriteRender.flipX = false;
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

    //Method to detect that the player is falling or not 
    bool IsFalling() => rigidBody.velocity.y < 0;

    //Method to detect if the player is moving or not
    bool IsMoving() => rigidBody.velocity.x != 0;
}