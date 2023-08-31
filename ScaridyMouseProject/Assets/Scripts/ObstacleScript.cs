using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ObstacleScript : MonoBehaviour
{
    public Obstacle obstacle; //Scriptable Object

    //Refernces to components
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Rigidbody2D rb;
    private Collider2D colliderCollider;
    [SerializeField]
    private GameManagerScript manager;
    [SerializeField]
    private ObstacleManagerScript oManager;

    //Refernces to prefabs
    [SerializeField]
    private Sprite squareSprite;
    [SerializeField]
    private Sprite triangleSprite;
    [SerializeField]
    private Sprite circleSprite;

    //Variables
    private Vector3 startScale;     //StartScale is used for pop and for squeeze

    private int bounces;    //For bounces is defined in the oManager
    private bool isBouncing  = false;
    private float elapsedBounceTime;
    private Vector3 finalForceDirection;
    private float finalForceMagnitude;


    private bool hasPopped = false; //For PoP animation
    private float elapsedPopTime;   //total PoP time is defined in the oManager
    private Vector3 finalPopScale;
    private Color startColor;

    private float largeRoughness;   //For large obstacles camera shake
    private float largeMagnitude;
    private float largeDuration;

    [SerializeField]
    [Range(0f, 10f)]
    private float smallScale; //Scale
    [SerializeField]
    [Range(0f, 10f)]
    private float mediumScale;
    [SerializeField]
    [Range(0f, 10f)]
    private float largeScale;

    [SerializeField]
    [Range(0f, 10f)]
    private float smallMass;    //Mass
    [SerializeField]
    [Range(0f, 10f)]
    private float mediumMass;
    [SerializeField]
    [Range(0f, 10f)]
    private float largeMass;

    private int smallOrderInLayer = 2;  //Render layer
    private int mediumOrderInLayer = 1;
    private int largeOrderInLayer = 0;

    [SerializeField]
    private Color greenColor;   //Colors
    [SerializeField]
    private Color blueColor;
    [SerializeField]
    private Color redColor;

    private float maxGravitySqueeze;    //Gravity Squeeze (Juice)
    private float gravitySqueezeIndex;
    private Vector3 currentGravitySqueeze;

    void Start()
    {
        // Debug.Log(gameObject.name + " has been instantiated to the scene."); //Debug what has been instatiated

        //Set references
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        colliderCollider = GetComponent<Collider2D>();

        colliderCollider.isTrigger = true;
        
        GameObject gameManager = GameObject.FindWithTag("Manager");
        if (gameManager != null)
        {
            manager = gameManager.GetComponent<GameManagerScript>();
            oManager = gameManager.GetComponent<ObstacleManagerScript>();
        }

        gameObject.tag = "Obstacle";

        //Decide references to Scriptable Object
        Obstacle.Shape shape = obstacle.shape;
        Obstacle.Sieze sieze = obstacle.sieze;
        Obstacle.Colour colour = obstacle.colour;

        //Decide shape with scriptable Object
        switch (shape)
        {
            case Obstacle.Shape.Square:
                sprite.sprite = squareSprite;
                break;

            case Obstacle.Shape.Triangle:
                sprite.sprite = triangleSprite;
                break;
            
            case Obstacle.Shape.Circle:
                sprite.sprite = circleSprite;
                break;
        }

        //Decide sieze with scriptable object
        switch (sieze)
        {
            case Obstacle.Sieze.Small:
                transform.localScale = new Vector3(smallScale, smallScale,1f);
                rb.gravityScale = smallMass;
                sprite.sortingOrder = smallOrderInLayer;
                gravitySqueezeIndex = oManager.smallGravitySqueezeIndex;
                break;

            case Obstacle.Sieze.Medium:
                transform.localScale = new Vector3(mediumScale, mediumScale, 1f);
                rb.gravityScale = mediumMass;
                sprite.sortingOrder = mediumOrderInLayer;
               gravitySqueezeIndex = oManager.mediumGravitySqueezeIndex;
                break;

            case Obstacle.Sieze.Large:
                transform.localScale = new Vector3(largeScale, largeScale, 1f);
                rb.gravityScale = largeMass;
                sprite.sortingOrder = largeOrderInLayer;
                gravitySqueezeIndex = oManager.largeGravitySqueezeIndex;
                break;
        }

        //Decide Colour of scriptable object
        switch (colour)
        {
            case Obstacle.Colour.Green:
                sprite.color = greenColor; 
                bounces = oManager.greenBounces; break;

            case Obstacle.Colour.Blue:
                sprite.color = blueColor; 
                bounces = oManager.blueBounces; break;

            case Obstacle.Colour.Red:
                sprite.color = redColor; 
                bounces = oManager.redBounces; break;

        }

        if (transform.position.y <= 11 && transform.position.x <= -17){       //If spawn at left side of screen
            finalForceMagnitude = (transform.position.y + 8) * ((oManager.hightForceIndex* oManager.sideForceIndex)/(transform.position.y+8));
            finalForceDirection = oManager.leftSideForceAngle.normalized; 
            
            rb.AddForce(finalForceDirection*finalForceMagnitude, ForceMode2D.Impulse);
        }
        else if (transform.position.y <= 11 && transform.position.x >= 17){        //If Spawn at right side of screen
            finalForceMagnitude = (transform.position.y + 8) * ((oManager.hightForceIndex* oManager.sideForceIndex)/(transform.position.y+8));
            finalForceDirection = oManager.rightSideForceAngle.normalized; 
            
            rb.AddForce(finalForceDirection*finalForceMagnitude, ForceMode2D.Impulse);
        }
        
        finalForceMagnitude =oManager.straightForceIndex * rb.gravityScale * oManager.bounceForceSiezeIndex;
        finalForceDirection =Vector3.up; 
        

        //Define variables by Obstacle Manager
        largeDuration = oManager.largeShakeDuration;    //Screen Shake Variables
        largeMagnitude = oManager.largeShakeMagnitude;
        largeRoughness = oManager.largeShakeRoughness;

        maxGravitySqueeze = oManager.maxGravitySqueeze; //Gravity Squeeze Variables

        startScale = transform.localScale;              //StartScale
    }

    void Update()
    {
        if(hasPopped)   //popping juice animation
        {
            elapsedPopTime += Time.deltaTime;    //Calculate time from start
            float percentageComplete = elapsedPopTime / oManager.popTime;  //Calculate how far along the "animation" is

            if (percentageComplete >= 1)    //If the pop is completed - destroy gameObject
            {
                GameObject.Destroy(gameObject);
                return;
            }
            else
            {
                float scaleMultiplier = oManager.popCurve.Evaluate(percentageComplete);    //else calculate the curve and change the scale for a pop effect
                transform.localScale = Vector3.Lerp(startScale, finalPopScale, scaleMultiplier);
                sprite.color = Color.Lerp(startColor, Color.white, scaleMultiplier);
            }
        }   

        if(isBouncing)
        {
            elapsedBounceTime += Time.deltaTime;    //Calculate time from start
            float percentageComplete = elapsedBounceTime / oManager.bounceTime;  //Calculate how far along the "animation" is

            if (percentageComplete >= 1)    //If the pop is completed - destroy gameObject
            {
                isBouncing = false;
                elapsedBounceTime = 0;
                return;
            }
            else
            {
                Vector3 preBounceScale = transform.localScale;
                float scaleMultiplierX = oManager.bounceCurveX.Evaluate(percentageComplete);    //else calculate the curve and change the scale for a pop effect
                float scaleMultiplierY = oManager.bounceCurveY.Evaluate(percentageComplete);
                float lerpedScaleX = Mathf.Lerp(preBounceScale.x, startScale.x, scaleMultiplierX);
                float lerpedScaleY = Mathf.Lerp(preBounceScale.y, startScale.y, scaleMultiplierY);

                transform.localScale = new Vector3(lerpedScaleX, lerpedScaleY, transform.localScale.z );
            }
        }

        if(rb.velocity.y<0 && !hasPopped && !isBouncing){        //Squeeze Juice Effect
            currentGravitySqueeze.x = startScale.x + (rb.velocity.y * gravitySqueezeIndex);        //Calculate the x-scale for squeeze effect
            float clampedSqueezeX = Mathf.Clamp(currentGravitySqueeze.x, startScale.x/maxGravitySqueeze, startScale.x );

            currentGravitySqueeze.y = startScale.y - (rb.velocity.y * gravitySqueezeIndex);         //Calculate the y-scale for squeeze effect
            float clampedSqueezeY = Mathf.Clamp(currentGravitySqueeze.y, startScale.y, startScale.y*maxGravitySqueeze);

            transform.localScale = new Vector3 (clampedSqueezeX, clampedSqueezeY, transform.localScale.z);      //change the x and Y scale according to the calculated squeeze values
        }
    }

    void OnTriggerEnter2D(Collider2D collider){ 
        if(collider.tag == "Floor")
        {
            if(bounces == 0){
                Pop();
            }
            else
            {
                Bounce();
                bounces --;
            }
        }
 
    }
    
    private void Pop()    //Method to trigger on bounce with floor (triggered by floor as of writing this)
    {
        AudioManagerScript.instance.Play("ObstacleDestroy");    //Play sound

        switch (obstacle.sieze){
           case Obstacle.Sieze.Large:
                CameraShaker.Instance.ShakeOnce(largeMagnitude,largeRoughness,largeDuration,largeDuration);
                break;
        }
 
        elapsedPopTime = 0;  //Decide current time and scale
        startColor = sprite.color;

        finalPopScale = transform.localScale + new Vector3(oManager.popScale, oManager.popScale, 0);    //Decide wanted final pop-scale from ObstacelManager
        
        rb.velocity = new Vector3(0, 0, 0);
        rb.isKinematic = true;  //Stop the obstacles movement and remove the RigidBody component

        colliderCollider.enabled = false;

        hasPopped = true;
    }

    private void Bounce(){
        
        elapsedBounceTime = 0;
        isBouncing = true;
        
        AudioManagerScript.instance.Play("ObstacleDestroy");    //Play sound

        switch (obstacle.sieze){
           case Obstacle.Sieze.Large:
                CameraShaker.Instance.ShakeOnce(largeMagnitude,largeRoughness,largeDuration,largeDuration);
                break;
        }

        ApplyForce();
        
    }

    private void ApplyForce()
    {
        Vector3 bounceForce = finalForceDirection*finalForceMagnitude;
        rb.AddForce(bounceForce, ForceMode2D.Impulse);
    }

}
