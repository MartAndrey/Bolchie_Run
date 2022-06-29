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
    public float runningSpeed = 4f;

    //Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    //Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    //Update is called once per frame
    void Update()
    {

    }

    //It is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (rb.velocity.x < runningSpeed)
        {
            rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
        }
    }

    //Method that is responsible for resetting the position of the camera
    public void ResetPosition()
    {
        this.transform.position = startPosition;
    }
}

