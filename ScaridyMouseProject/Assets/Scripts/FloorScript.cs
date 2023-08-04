using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private ObstacleScript obstacle;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            obstacle = collider.GetComponent<ObstacleScript>();

            obstacle.Pop();
        }
    }

}
