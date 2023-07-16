using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Obstacle obstacle;

    public GameManagerScript manager;

    public float lifeTime = 10f;
    public float life = 0f;

    [SerializeField]
    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + " has been instantiated to the scene."); //Debug what has been instatiated

        //Set references
        sprite = GetComponent<SpriteRenderer>();
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

        //Decide sieze with scriptable Object
        switch (shape)
        {
            case Obstacle.Shape.Square:
                //OBSOBSOBS fortsätt här!!!
                //Skapa public referenser till förbestämda sprites som du sen kan besäma utifrån datan i scriptable objectet
                Debug.Log(Obstacle.Shape.Square); 
                break;    
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
