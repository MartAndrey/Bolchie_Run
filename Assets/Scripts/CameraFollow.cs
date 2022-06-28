using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Rigidbody2D rb;

    public float runningSpeed = 4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (rb.velocity.x < runningSpeed)
        {
            rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
        }
    }
}

