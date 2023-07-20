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
    private bool cancelingSkipping;
    private int skipFrames;
    private int cancelFrames;
    [SerializeField]
    [Range(0, 10000)]
    private float skipForce = 10;
    [SerializeField]
    [Range(0, 30)]
    private int skipDuration = 0;
    [SerializeField]
    [Range(1, 30)]
    private int cancelDuration;
    

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

        //Skip if Space, canSkip and moving in a direction
        if (Input.GetKeyDown(KeyCode.Space) && canSkip && (Input.GetKey("a") || Input.GetKey("d")))
        {
            //Make sure the mouse isn't moving or breaking or anything else during jump
            canSkip = false;
            isMovingLeft = false;
            isMovingRight = false;
            isBreaking = false;
            isSkipping = true; //make sure other movement is impossible and we´re counting skipFrames

            Skip(); //Add force, play animations/Sounds mm

            skipFrames = 0; 
        }

        if (isSkipping) //Movement after skipping
        {
            skipFrames++;   //Count frames since starting skipping

            if (skipFrames == skipDuration) //If skip has been going on skipDuration frames then start breaking
            {
                Debug.Log("Stoped skipping");
                StartBreaking();
            }

            if(skipFrames >= skipDuration + cancelDuration) //If skip has been going on cancelDurationg frames then stop skipping and check if walking
            {
                isSkipping = false; //Make sure that the player can move again
                CheckIfWalking();
            }
        }

        //Decide values of variables related to moving left and start moving left. Actually moving left is under "void FixedUpdate"
        if (Input.GetKeyDown("a") && !isMovingLeft && !isSkipping)
        {
            Debug.Log("Started walking left after pressing a");
            StartRunningLeft();
        }
        if (Input.GetKeyUp("a")) //Stop moving left when releasing button
        {
            isMovingLeft = false;
        }

        //Decide values of variables related to moving right and start moving right. Actually moving right is under "void FixedUpdate"
        if (Input.GetKeyDown("d") && !isMovingRight && !isSkipping)
        {
            Debug.Log("Started walking right after pressing d");
            StartRunningRight();
        }
        if (Input.GetKeyUp("d")) //Stop moving right when releasing button
        {
            isMovingRight = false;
        }

        //Begin breaking if not moving left, right, player ins't standing still and player isn´t skipping
        if(!isMovingLeft && !isMovingRight && rb.velocity.x != 0 && !isBreaking && !isSkipping)
        {
            StartBreaking();
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

        //Change of velocity to move left
        if (isMovingLeft)
        {
            float unclampedAcceleration = rb.velocity.x + velocityPerFrame; //Subtract to move left

            rb.velocity = new Vector2(Mathf.Clamp(unclampedAcceleration, -maxMoveVelocity, velocityBeforeSpeed), rb.velocity.y);
        }

        //Change of velocity to move right
        if (isMovingRight)
        {
            float unclampedAcceleration = rb.velocity.x + velocityPerFrame; //Add to move right

            rb.velocity = new Vector2(Mathf.Clamp(unclampedAcceleration, velocityBeforeSpeed,maxMoveVelocity ), rb.velocity.y);
        }
    }

    private void StartBreaking()
    {
        isBreaking = true;
        //Calculate the velocity before breaking
        velocityBeforeBreak = rb.velocity.x;
        breakPerFrame = velocityBeforeBreak / framesToStop;

        Debug.Log("breakPerFrame = " + breakPerFrame);

    }

    private void StartRunningLeft()
    {
        isBreaking = false; //Stop breaking and stop moving right
        isMovingRight = false;

        velocityBeforeSpeed = rb.velocity.x; //Calculating velocity before accelerating to make use of during acceleration
        velocityPerFrame = (-maxMoveVelocity - velocityBeforeSpeed) / framesToMax; //adderar maxMoveVelocity i och med att hastigheten åt vänster är negativ, -- = +

        isMovingLeft = true;
        Debug.Log("velocityBeforeSpeed = " + velocityBeforeSpeed + " and velocityPerFrame = " + velocityPerFrame);

    }

    private void StartRunningRight()
    {
        isBreaking = false;
        isMovingLeft = false;

        velocityBeforeSpeed = rb.velocity.x;
        velocityPerFrame = (maxMoveVelocity - velocityBeforeSpeed) / framesToMax;

        isMovingRight = true;
        Debug.Log("velocityBeforeSpeed = " + velocityBeforeSpeed + " and velocityPerFrame = " + velocityPerFrame);

    }

    private void Skip()
    {
        Debug.Log("Skipped");
        Vector2 skipDirection = new Vector2 (0,0); //Create an empty Vector 2 

        //Skip right if walking right and skip left if walking left
        if (Input.GetKey("a"))
        {
            skipDirection = -transform.right;
        }
        else if (Input.GetKey("d"))
        {
            skipDirection = transform.right;
        }

        rb.AddForce(skipDirection * skipForce, ForceMode2D.Impulse);
    }

    private void CheckIfWalking()
    {
        Debug.Log("Checking if walking");
        //Make sure that the player starts to move again if he is holding down a key
        if (Input.GetKey("a") && Input.GetKey("d")) //Controll for if he is holding down both keys in witch case nothing should happen
        {
            Debug.Log("Both A and D are pressed down after skipping so no movement");
            return;
        }
        else if (Input.GetKey("a"))
        {
            Debug.Log("Started running left just after skipping");
            StartRunningLeft();
        }
        else if (Input.GetKey("d"))
        {
            Debug.Log("Started running right just after skipping");
            StartRunningRight();
        }

    }

    //Method for Power-Up objects to run on trigger
    public void PickUp(int power)   //int for what power-up was picked up
    {
        //spela animationer och ljud
        //Ändra färg på sprite?

        if (power == 1)  //1 = Skip Power-Up
        {
            canSkip = true;
        }

    }
}
