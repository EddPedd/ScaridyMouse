using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Obstacle obstacle; //Scriptable Object

    //Refernces to components
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameManagerScript manager;

    //Refernces to prefabs
    [SerializeField]
    private Sprite squareSprite;
    [SerializeField]
    private Sprite triangleSprite;
    [SerializeField]
    private Sprite circleSprite;

    //Variables
    private bool hasPopped = false; //For bounce animation
    [SerializeField]
    [Range(0f, 2f)]
    private float bounceTime = 0.5f; 
    private float elapsedBounceTime;
    [SerializeField]
    private AnimationCurve bounceCurve;
    [SerializeField]
    [Range(0f, 1f)]
    private float bouncePoPScale = .2f;
    private Vector3 startScale;
    private Vector3 finalPopScale;

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
    private Color greenColor;   //Sprites
    [SerializeField]
    private Color blueColor;
    [SerializeField]
    private Color redColor;

    void Start()
    {
        Debug.Log(gameObject.name + " has been instantiated to the scene."); //Debug what has been instatiated

        //Set references
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        GameObject gameManager = GameObject.FindWithTag("Manager");
        if (gameManager != null)
        {
            manager = gameManager.GetComponent<GameManagerScript>();
        }

        gameObject.tag = "Obstacle";

        Debug.Log("A " + obstacle.name + " has been instatiated in the game"); //Debug for Scriptable Object
        //Decide references to Scriptable Object
        Obstacle.Shape shape = obstacle.shape;
        Obstacle.Sieze sieze = obstacle.sieze;
        Obstacle.Colour colour = obstacle.colour;
        Debug.Log("shape = " + obstacle.shape + ", sieze = " + obstacle.sieze + " and colour = " + obstacle.colour); //Debug the references

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
                rb.mass = smallMass;
                sprite.sortingOrder = smallOrderInLayer;
                break;

            case Obstacle.Sieze.Medium:
                transform.localScale = new Vector3(mediumScale, mediumScale, 1f);
                rb.mass = mediumMass;
                sprite.sortingOrder = mediumOrderInLayer;

                break;

            case Obstacle.Sieze.Large:
                transform.localScale = new Vector3(largeScale, largeScale, 1f);
                rb.mass = largeMass;
                sprite.sortingOrder = largeOrderInLayer;

                break;
        }

        //Decide Colour of scriptable object
        switch (colour)
        {
            case Obstacle.Colour.Green:
                sprite.color = greenColor; break;

            case Obstacle.Colour.Blue:
                sprite.color = blueColor; break;

            case Obstacle.Colour.Red:
                sprite.color = redColor; break;
        }
    }

    void Update()
    {
        if(hasPopped)
        {
            elapsedBounceTime += Time.deltaTime;    //Calculate time from start
            float percentageComplete = elapsedBounceTime / bounceTime;  //Calculate how far along the "animation" is

            if (percentageComplete >= 1)    //If the pop is completed - destroy gameObject
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                float scaleMultiplier = bounceCurve.Evaluate(percentageComplete);    //else calculate the curve and change the scale for a pop effect
                transform.localScale = Vector3.Lerp(startScale, finalPopScale, percentageComplete);
            }
        }
    }

    public void Bounce()    //Method to trigger on bounce with floor (triggered by floor as of writing this)
    {
        AudioManagerScript.instance.Play("ObstacleDestroy");    //Play sound

        elapsedBounceTime = 0;  //Decide current time and scale
        startScale = transform.localScale;

        finalPopScale = startScale + new Vector3(bouncePoPScale, bouncePoPScale, 0);    //Decide wanted final pop-scale
    }

}
