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
    public float lifeTime = 10f;
    private float life = 0f;

    [Range(0f, 10f)]
    public float smallScale;
    [Range(0f, 10f)]
    public float mediumScale;
    [Range(0f, 10f)]
    public float largeScale;

    [Range(0f, 10f)]
    public float smallMass;
    [Range(0f, 10f)]
    public float mediumMass;
    [Range(0f, 10f)]
    public float largeMass;

    [SerializeField]
    private Color greenColor;
    [SerializeField]
    private Color blueColor;
    [SerializeField]
    private Color redColor;

    // Start is called before the first frame update
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

        //Decide sieze and mass with scriptable object
        switch (sieze)
        {
            case Obstacle.Sieze.Small:
                transform.localScale = new Vector3(smallScale, smallScale,1f);
                rb.mass = smallMass;
                break;

            case Obstacle.Sieze.Medium:
                transform.localScale = new Vector3(mediumScale, mediumScale, 1f);
                rb.mass = mediumMass;
                break;

            case Obstacle.Sieze.Large:
                transform.localScale = new Vector3(largeScale, largeScale, 1f);
                rb.mass = largeMass;
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

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life >=lifeTime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            manager.RestartGame();
        }
    }
}
