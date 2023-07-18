using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawnerScript : MonoBehaviour
{
    //References
    [SerializeField]
    private Transform spawnpoint;

    //Common variables
    private GameObject obstacleToSpawn;
    
    //List of all possible obstacles
    [SerializeField]
    private GameObject GSC;
    [SerializeField]
    private GameObject GMC;
    [SerializeField]
    private GameObject GLC;
    [SerializeField]
    private GameObject BSC;
    [SerializeField]
    private GameObject BMC;
    [SerializeField]
    private GameObject BLC;
    [SerializeField]
    private GameObject RSC;
    [SerializeField]
    private GameObject RMC;
    [SerializeField]
    private GameObject RLC;
    [SerializeField]
    private GameObject GSS;
    [SerializeField]
    private GameObject GMS;
    [SerializeField]
    private GameObject GLS;
    [SerializeField]
    private GameObject BSS;
    [SerializeField]
    private GameObject BMS;
    [SerializeField]
    private GameObject BLS;
    [SerializeField]
    private GameObject RSS;
    [SerializeField]
    private GameObject RMS;
    [SerializeField]
    private GameObject RLS;
    [SerializeField]
    private GameObject GST;
    [SerializeField]
    private GameObject GMT;
    [SerializeField]
    private GameObject GLT;
    [SerializeField]
    private GameObject BST;
    [SerializeField]
    private GameObject BMT;
    [SerializeField]
    private GameObject BLT;
    [SerializeField]
    private GameObject RST;
    [SerializeField]
    private GameObject RMT;
    [SerializeField]
    private GameObject RLT;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            int testShape = Random.Range(1, 4);
            int testColour = Random.Range(1, 4);
            int testSieze = Random.Range(1, 4);
            Vector3 testPlayerPosition = new Vector3(testShape, testSieze, testColour);

            //SpawnObstacle(testShape, testColour, testSieze,testPlayerPosition);
            SpawnObstacle(1,1,1,testPlayerPosition);
        }
    }

    public void SpawnObstacle(int shape, int colour, int sieze, Vector3 playerPosition)
    {
        int finalObstacle=0;

        finalObstacle += 100 * shape;
        finalObstacle += 10 * colour;
        finalObstacle += 1 * sieze;
        Debug.Log(finalObstacle);

        //27 if-statements, one for each possible Obstacle
        //Exempel:
        if(finalObstacle == 111)
        {
            obstacleToSpawn = GSC;
        }

        float randomHeight = Random.Range(5, 11);
        Vector3 finalHeight = new Vector3(spawnpoint.position.x, spawnpoint.position.y + randomHeight, 0f) ;

        Instantiate(obstacleToSpawn, finalHeight, spawnpoint.rotation);
        

    }

}
