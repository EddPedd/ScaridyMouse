using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public GameManagerScript manager;

    public float lifeTime = 10f;
    public float life = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + " has been instantiated to the scene.");

        GameObject gameManager = GameObject.FindWithTag("Manager");

        if (gameManager != null)
        {
            manager = gameManager.GetComponent<GameManagerScript>();
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
