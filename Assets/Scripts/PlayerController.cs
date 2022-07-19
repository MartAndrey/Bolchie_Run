using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Singleton PlayerController
    public static PlayerController sharedInstance;

    //Variables that are set equal to the parameters of the animator control
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_IS_FALLING = "isFalling";
    const string STATE_IS_MOVING = "isMoving";

    [SerializeField] int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 100, MAX_HEATH = 200, MIN_HEALTH = 10,
                     INITIAL_MANA = 15, MAX_MANA = 30, MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.2f;

    //Variable to save the initial position of the player
    Vector3 startPosition;

    // Force with which the player jumps
    public float jumpForce = 50f;

    //Player speed
    public float speed = 10f;

    //Lightning distance to detect if we are touching the ground
    public float rayDistance = 3.5f;

    //Soil defining layer
    public LayerMask groundMask;

    //Component responsible for responding to and handling physics
    public Rigidbody2D rigidBody;

    //Get the player animator
    Animator animator;

    //The one in charge of rendering the sprite.
    SpriteRenderer spriteRender;

    // Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
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

        startPosition = this.transform.position; //Save current player position

        RestartHealthMana();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        animator.SetBool(STATE_IS_FALLING, IsFalling());
        animator.SetBool(STATE_IS_MOVING, IsMoving());

        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }

        if (Input.GetButtonDown("Super Jump"))
        {
            Jump(true);
        }


        //--------------------------------------------------------------------------------------------//
        Debug.DrawRay(this.transform.position, Vector2.down * rayDistance, Color.white);
    }

    //It is called every fixed frame-rate frame
    void FixedUpdate()
    {
        Move();
    }

    #region Restore Player Position
    //The method is responsible for resetting the player's position
    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);

        Invoke("RestartPosition", 0.15f);

        RestartHealthMana();
    }

    void RestartHealthMana()
    {
        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;

        this.rigidBody.velocity = Vector2.zero;
    }
    #endregion Restore Player Position

    #region Player Jump

    //Player Jump method
    void Jump(bool superJump)
    {
        float jumpForceFactor = jumpForce;

        if (superJump && manaPoints >= SUPERJUMP_COST && IsTouchingTheGround())
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }

        if (IsTouchingTheGround() && GameManager.sharedInstance.currentGameState == GameState.InGame) //If the player is touching the ground and "GameState" is "InGame", the player will be able to jump
        {
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }
    #endregion Player Jump

    #region  Player Move
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
    #endregion Player Move

    #region Plyer Dead
    public void Dead()
    {
        animator.SetBool(STATE_ALIVE, false);

        GameManager.sharedInstance.Invoke("GameOver", 1.2f);

        float traveledDistance = GetTravelledDistance();
        float previousHighDistance = PlayerPrefs.GetFloat("High Score", 0f);

        if (traveledDistance > previousHighDistance)
        {
            PlayerPrefs.SetFloat("High Score", traveledDistance);
        }
    }
    #endregion Plyer Dead

    #region Validation methods
    //Method to detect if the player is on the ground or not
    public bool IsTouchingTheGround()
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
    #endregion Validation methods

    #region Collectables
    public void CollectHealth(int points)
    {
        this.healthPoints += points;

        if (this.healthPoints >= MAX_HEATH)
        {
            this.healthPoints = MAX_HEATH;
        }

        if (this.healthPoints <= 0)
        {
            Dead();
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;

        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
    #endregion Collectables
}