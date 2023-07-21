using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour
{
    public GameObject skip;
    public Transform upperSpawn;
    public float timeBetweenPowerSpawns = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSkip", timeBetweenPowerSpawns, timeBetweenPowerSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSkip()
    {
        float offSetVolume = Random.Range(-10f, 10f);
        Vector3 offSet = new Vector3(offSetVolume, 0,0);
        Instantiate(skip, upperSpawn.position + offSet, upperSpawn.rotation);
    }
}
