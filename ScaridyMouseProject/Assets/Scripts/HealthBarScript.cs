using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerMovement player;
    private GameObject manager;
    private GameManagerScript gManager;
    private GameObject heart1;
    private GameObject heart2;
    private Transform heart1Transform;
    private Transform heart2Transform;
    private int playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        heart1Transform = transform.GetChild(0);
        heart2Transform = transform.GetChild(1);

        heart1 = heart1Transform.gameObject;
        heart2 = heart2Transform.gameObject;
        
        if (heart1 != null || heart2 != null)
        {
            Debug.Log(heart1 + " " + heart2);
        }

        manager = GameObject.FindWithTag("Manager");
        gManager = manager.GetComponent<GameManagerScript>();

        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();

        player.healthBar = this;
    }

    public void UpdateHealthBar()
    {
        playerHealth = player.currentHealth;
        Debug.Log("After updating the healthbar in the HealthBarScript, playerHealth = " + playerHealth);
        if (playerHealth <= 0 )
        {
            Debug.Log("HealthBarScript restarted the game");
            gManager.RestartGame(); 
        }
        else if (playerHealth == 1)
        {
            heart2.SetActive(false);

        }
        else if (playerHealth == 2)
        {
            heart2.SetActive(true);
        }
    }
}
