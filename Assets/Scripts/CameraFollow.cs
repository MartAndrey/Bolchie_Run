using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Singleton
    public static CameraFollow sharedInstance;

    //Variable that stores the initial position   
    Vector3 startPosition;

    //Component responsible for responding to and handling physics
    Rigidbody2D rb;

    //Speed ​​with which the camera moves
    public float runningSpeed = 3f;

    float _increaseSpeed = 0.5f;

    float _distanceToSpeedUpMax = 10;

    float _distanceToSpeedUp = 0;

    //Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _distanceToSpeedUp += Time.deltaTime;
    }
    //Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    //It is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (rb.velocity.x < runningSpeed)
        {
            rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
        }

        if (_distanceToSpeedUp >= _distanceToSpeedUpMax)
        {
            IncreaseSpeed();

            _distanceToSpeedUp = 0;
        }
    }

    //Method that is responsible for resetting the position of the camera
    public void ResetCamera()
    {
        this.transform.position = startPosition;

        runningSpeed = 3;

        _distanceToSpeedUp = 0;
    }

    void IncreaseSpeed()
    {
        runningSpeed += _increaseSpeed;
    }
}

