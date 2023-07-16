using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References to components
    public Rigidbody2D rb;

    //Movement variables
    [Range(0,50)]
    public float maxMoveVelocity;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    [Range(1, 50)]
    [SerializeField]
    private int framesToMax = 3;
    private float velocityBeforeSpeed;
    private float velocityPerFrame;

    //Breaking variables
    [Range (1, 50)]   [SerializeField]
    private int framesToStop = 3;
    public bool isBreaking = false;
    private float velocityBeforeBreak;
    private float breakPerFrame;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Decide values of variables related to moving left and start moving left. Actually moving left is under "void FixedUpdate"
        if (Input.GetKeyDown("a") && !isMovingLeft)
        {
            isBreaking = false; //Stop breaking and stop moving right
            isMovingRight = false;

            velocityBeforeSpeed = rb.velocity.x; //
            velocityPerFrame = (-maxMoveVelocity - velocityBeforeSpeed) / framesToMax; //adderar maxMoveVelocity i och med att hastigheten åt vänster är negativ, -- = +

            isMovingLeft = true;
            Debug.Log("velocityBeforeSpeed = " + velocityBeforeSpeed + " and velocityPerFrame = " + velocityPerFrame);

        }
        if (Input.GetKeyUp("a")) //Stop moving left when releasing button
        {
            isMovingLeft = false;
        }

        //Decide values of variables related to moving right and start moving right. Actually moving right is under "void FixedUpdate"
        if (Input.GetKeyDown("d") && !isMovingRight)
        {
            isBreaking = false;
            isMovingLeft = false;

            velocityBeforeSpeed = rb.velocity.x;
            velocityPerFrame = (maxMoveVelocity - velocityBeforeSpeed) / framesToMax;

            isMovingRight = true;
            Debug.Log("velocityBeforeSpeed = " +  velocityBeforeSpeed + " and velocityPerFrame = " + velocityPerFrame);
        }
        if (Input.GetKeyUp("d")) //Stop moving right when releasing button
        {
            isMovingRight = false;
        }

        //Begin breaking and deciding values of related variables
        if(!isMovingLeft && !isMovingRight && rb.velocity.x != 0 && !isBreaking)
        {
            isBreaking = true;
            //Räkna ut hastigheten innan bromsandet börjat
            velocityBeforeBreak = rb.velocity.x;
            breakPerFrame = velocityBeforeBreak / framesToStop;
            Debug.Log("breakPerFrame = " + breakPerFrame);
        }

    }

    void FixedUpdate () 
    {
        //Beteende om man bromsar
        if (isBreaking) 
        {
            float unclampedBreak = rb.velocity.x - breakPerFrame;

            if(velocityBeforeBreak>=0)
            {
                rb.velocity = new Vector2(Mathf.Clamp(unclampedBreak, 0, velocityBeforeBreak), rb.velocity.y);
            }
            else if (velocityBeforeBreak<0)
            {
                rb.velocity = new Vector2(Mathf.Clamp(unclampedBreak, velocityBeforeBreak, 0), rb.velocity.y);
            }
                        
            if(rb.velocity.x == 0)
            {
                isBreaking = !isBreaking;
            }
        }

        //Beteende om man går vänster
        if (isMovingLeft)
        {
            float unclampedAcceleration = rb.velocity.x + velocityPerFrame; //Subtract to move left

            rb.velocity = new Vector2(Mathf.Clamp(unclampedAcceleration, -maxMoveVelocity, velocityBeforeSpeed), rb.velocity.y);
        }

        //Beteende man går höger
        if (isMovingRight)
        {
            float unclampedAcceleration = rb.velocity.x + velocityPerFrame; //Add to move right

            rb.velocity = new Vector2(Mathf.Clamp(unclampedAcceleration, velocityBeforeSpeed,maxMoveVelocity ), rb.velocity.y);
        }
    }
}
