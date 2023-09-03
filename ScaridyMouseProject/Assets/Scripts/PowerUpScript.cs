using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    //References
    [SerializeField]
    private PlayerMovement player;
    private GameObject playerPlayer;

    //Variables
    [SerializeField]
    [Range(0,10)]
    private float totalFallTime;
    private Vector3 currentPosition;
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 groundPosition = new Vector3 (0,-9,0);
    private float ground = -9;


    [SerializeField]
    private int powerInt;
    [SerializeField]
    private float lifeTime = 6;
    private float timeSpentLive;



    void Start()
    {
        playerPlayer = GameObject.FindWithTag("Player");
        player = playerPlayer.GetComponent<PlayerMovement>();

        if (gameObject.name == "SkipPowerUp(Clone)")
        {
            powerInt =1;
        }

        gameObject.layer = 9;   //9 for layerindex of interactables
        //Continue with else-if statements for each type of power-up

        startPosition = transform.position;
        groundPosition = new Vector3(startPosition.x, ground, startPosition.z);

        Debug.Log("PowerUp start position = " + startPosition + "groundPosition = " + groundPosition);
    }

    void Update()
    {

        timeSpentLive += Time.deltaTime;
        if (timeSpentLive >= lifeTime)
        {
            GameObject.Destroy(gameObject);
        }

        float t = timeSpentLive/totalFallTime;     //lerp position to for falling effect
        //t = Mathf.Clamp01(t);
        Debug.Log("powerUp t = " + t);
        currentPosition = Vector3.Lerp(startPosition, groundPosition, t);
        //float currentPositionY = Mathf.Lerp(startPosition.y, groundPosition.y, t);
        //currentPosition.y = currentPositionY;
        if(transform.position != groundPosition){
            transform.position = currentPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player"))
        {
            player.PickUp(powerInt);
            AudioManagerScript.instance.Play("PickUp");
            PickedUp();
        }
    }

    public void Destroyed()
    {
        //Spela animationer och ljud
        GameObject.Destroy(gameObject);
    }

    public void PickedUp()
    {
        //Spela animationer och ljud
        GameObject.Destroy(gameObject);
    }
}
