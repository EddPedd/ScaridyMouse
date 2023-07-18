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
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    [Range(1, 50)]
    [SerializeField]
    private int framesToMax = 3;
    private float velocityBeforeSpeed;
    private float velocityPerFrame;

    //Breaking variables
    [Range (1, 50)]   [SerializeField]
    private int framesToStop = 3;
    private bool isBreaking = false;
    private float velocityBeforeBreak;
    private float breakPerFrame;

    //Skip variables
    private bool canSkip = false;
    private bool isSkipping = false;
    private float skipTime;
    [SerializeField]
    [Range(0, 2)]
    private float skipDuration = 0;
    [SerializeField]
    [Range(0, 10000)]
    private float skipForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //"T" for test
        if (Input.GetKeyDown("t"))
        {
            canSkip = true;
        }

        //Skip if Space and canSkip
        if (Input.GetKeyDown(KeyCode.Space) && canSkip)
        {
            //Make sure the mouse isn't moving or breaking or anything else during jump
            canSkip = false;
            isMovingLeft = false;
            isMovingRight = false;
            isBreaking = false;

            Skip(); //Add force, play animations/Sounds mm

            //Count the time of the skip
            skipTime += Time.deltaTime;

            //Cancel the skip if it has gone on for the wished duration
            if (skipTime >= skipDuration)
            {
                //Cancel skip
                isSkipping = false;
            }
        }


        //Decide values of variables related to moving left and start moving left. Actually moving left is under "void FixedUpdate"
        if (Input.GetKeyDown("a") && !isMovingLeft && !isSkipping)
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
        if (Input.GetKeyDown("d") && !isMovingRight && !isSkipping)
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

        //Begin breaking if not moving left, right, player ins't standing still and player isn´t skipping
        if(!isMovingLeft && !isMovingRight && rb.velocity.x != 0 && !isBreaking && !isSkipping)
        {
            isBreaking = true;
            //Calculate the velocity before breaking
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

    private void Skip()
    {
        Debug.Log("Skipped");
        Vector3 skipDirection = transform.forward;
        rb.AddForce(skipDirection * skipForce, ForceMode2D.Impulse);
    }
}
