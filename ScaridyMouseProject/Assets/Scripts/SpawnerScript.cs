using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float spawnInterval = 5f;
    public float spawnIntervalReduction = 1 / 2;
    public Transform transform1;
    public Transform transform2;
    public Transform transform3;
    public Transform transform4;
    public Transform transform5;

    public Vector3 spawnOffSet = new Vector3(0f, 0f, 0f);
    public GameObject ballObstacle1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RandomObstacle", 1, spawnInterval);
        InvokeRepeating("ReduceInterval", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceInterval()
    {
        spawnInterval -= spawnIntervalReduction;
        Mathf.Clamp(spawnInterval, 1/2, 20);
    }

    public void RandomObstacle()
    {
        int randomSpawn = Random.Range(0, 3);
        Debug.Log("randomSpawn rolled "+ randomSpawn);

        if(randomSpawn == 0)
        {
            Instantiate(ballObstacle1, transform1.position+spawnOffSet, transform1.rotation);
        }
        if (randomSpawn == 1)
        {
            Instantiate(ballObstacle1, transform2.position + spawnOffSet, transform2.rotation);
        }
        if (randomSpawn == 2)
        {
            Instantiate(ballObstacle1, transform3.position + spawnOffSet, transform3.rotation);
        }
    }

}