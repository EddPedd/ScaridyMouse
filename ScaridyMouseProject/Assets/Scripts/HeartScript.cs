using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerMovement player;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        Debug.Log(playerObject.name);
        gameObject.layer = 9;       //9 for the index of the interactable layer
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log(collider.gameObject.name + "picked up " + gameObject.name);
            PickUp();
        }
    }

    private void PickUp()
    {
        player.GainHealth(1);
        GameObject.Destroy(gameObject);
    }

}
