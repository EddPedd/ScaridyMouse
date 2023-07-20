using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private PlayerMovement player;
    private GameObject playerPlayer;
    private int powerInt;
    // Start is called before the first frame update
    void Start()
    {
        playerPlayer = GameObject.FindWithTag("Player");
        player = playerPlayer.GetComponent<PlayerMovement>();

        if (gameObject.name == "SkipPowerUp")
        {
            powerInt =1;
        }
        //Continue with else-if statements for each type of power-up
    }

    void Update()
    {
        //Lägg till förändring av animationer 
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player"))
        {
            player.PickUp(powerInt);
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
