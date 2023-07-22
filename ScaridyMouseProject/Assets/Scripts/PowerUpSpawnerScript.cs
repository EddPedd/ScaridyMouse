using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour
{
    public Transform upperSpawn;

    public GameObject skip;
    [SerializeField]
    public float timeBetweenPowerSpawns = 1;

    public GameObject heart;
    [SerializeField]
    private float timeBetweenHeartSpawns = 10;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSkip", timeBetweenPowerSpawns, timeBetweenPowerSpawns);
        InvokeRepeating("SpawnHeart", timeBetweenHeartSpawns, timeBetweenHeartSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnHeart()
    {
        float offSetVolume = Random.Range(-10f, 10f);
        Vector3 offSet = new Vector3(offSetVolume, 0, 0);
        Instantiate(heart, upperSpawn.position + offSet, upperSpawn.rotation);
    }

    private void SpawnSkip()
    {
        float offSetVolume = Random.Range(-10f, 10f);
        Vector3 offSet = new Vector3(offSetVolume, 0,0);
        Instantiate(skip, upperSpawn.position + offSet, upperSpawn.rotation);
    }
}
