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
        //Continue with else-if statements for each type of power-up
    }

    void Update()
    {

        timeSpentLive += Time.deltaTime;
        if (timeSpentLive >= lifeTime)
        {
            GameObject.Destroy(gameObject);
        }
        //Lägg till förändring av animationer 
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
