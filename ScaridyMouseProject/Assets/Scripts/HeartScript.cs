using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerMovement player;
    [SerializeField]
    private float lifeTime;
    private float timeSpentLive = 0;

    [SerializeField]
    private float fallTime;
    private Vector3 startPosition;
    private Vector3 groundPosition;
    private float ground = -9;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        Debug.Log(playerObject.name);
        gameObject.layer = 9;       //9 for the index of the interactable layer
        
        startPosition = transform.position;
        groundPosition = new Vector3(startPosition.x, ground, startPosition.z);
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void Update () {
        timeSpentLive += Time.deltaTime;

        if(timeSpentLive>=lifeTime){
            GameObject.Destroy(gameObject);
            return;
        }

        float t = timeSpentLive/fallTime;
        t = Mathf.Clamp01(t);
        Vector3 currentPosition = Vector3.Lerp(startPosition, groundPosition, t);
        transform.position = currentPosition;
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
