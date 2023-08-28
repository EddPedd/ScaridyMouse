using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //References
    [SerializeField]
    private Transform leftSpawn;
    [SerializeField]
    private Transform rightSpawn;
    [SerializeField]
    private Transform upperSpawn;
    private ObstacleManagerScript oManager;
    private GameObject gameManager;

    //Common variables
    private GameObject obstacleToSpawn;
    [SerializeField]
    private float obstacleFrequency;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
        if(gameManager != null)
        {
            oManager = gameManager.GetComponent<ObstacleManagerScript>();
        }

        InvokeRepeating("SpawnObstacleUpper", obstacleFrequency, obstacleFrequency);
    }

    void Update()
    {
        //if (Input.GetKeyDown("t"))
        //{
          //  int testShape = Random.Range(1, 4);
            //int testColour = Random.Range(1, 4);
            //int testSieze = Random.Range(1, 4);
          //  Vector3 testPlayerPosition = new Vector3(testShape, testSieze, testColour);

            //int upperSpawnInt = 1;

       //     SpawnObstacle(testShape, testColour, testSieze, upperSpawnInt, testPlayerPosition);
        //}
    }

    public void SpawnObstacleUpper()
    {
        int testShape = Random.Range(1, 4);
        int testColour = Random.Range(1, 4);
        int testSieze = Random.Range(1, 4);
        Vector3 testPlayerPosition = new Vector3(testShape, testSieze, testColour);

        int spawnInt = Random.Range(1,4);

        SpawnObstacle(testShape, testColour, testSieze, spawnInt, testPlayerPosition);
    }

    public void SpawnObstacle(int shape, int colour, int sieze, int spawnPosition, Vector3 playerPosition)
    {
        int finalObstacle=0;

        finalObstacle += 100 * colour; //100 = Green, 200 = Blue, 300 = Red
        finalObstacle += 10 * sieze;   //10 = Small, 20 = Medium, 30 = Large
        finalObstacle += 1 * shape;    //1 = Circle, 2 = Square, 3 = Triangle
        Debug.Log(finalObstacle);

        //27 if-statements; one for each possible Obstacle
        if(finalObstacle == 111)
        {
            obstacleToSpawn = oManager.GSC;
        }
        if (finalObstacle == 121)
        {
            obstacleToSpawn = oManager.GMC;
        }
        if (finalObstacle == 131)
        {
            obstacleToSpawn = oManager.GLC;
        }
        if (finalObstacle == 211)
        {
            obstacleToSpawn = oManager.BSC;
        }
        if (finalObstacle == 221)
        {
            obstacleToSpawn = oManager.BMC;
        }
        if (finalObstacle == 231)
        {
            obstacleToSpawn = oManager.BLC;
        }
        if (finalObstacle == 311)
        {
            obstacleToSpawn = oManager.RSC;
        }
        if (finalObstacle == 321)
        {
            obstacleToSpawn = oManager.RMC;
        }
        if (finalObstacle == 331)
        {
            obstacleToSpawn = oManager.RLC;
        }
        if (finalObstacle == 112)
        {
            obstacleToSpawn = oManager.GSS;
        }
        if (finalObstacle == 122)
        {
            obstacleToSpawn = oManager.GMS;
        }
        if (finalObstacle == 132)
        {
            obstacleToSpawn = oManager.GLS;
        }
        if (finalObstacle == 212)
        {
            obstacleToSpawn = oManager.BSS;
        }
        if (finalObstacle == 222)
        {
            obstacleToSpawn = oManager.BMS;
        }
        if (finalObstacle == 232)
        {
            obstacleToSpawn = oManager.BLS;
        }
        if (finalObstacle == 312)
        {
            obstacleToSpawn = oManager.RSS;
        }
        if (finalObstacle == 322)
        {
            obstacleToSpawn = oManager.RMS;
        }
        if (finalObstacle == 332)
        {
            obstacleToSpawn = oManager.RLS;
        }
        if (finalObstacle == 113)
        {
            obstacleToSpawn = oManager.GST;
        }
        if (finalObstacle == 123)
        {
            obstacleToSpawn = oManager.GMT;
        }
        if (finalObstacle == 133)
        {
            obstacleToSpawn = oManager.GLT;
        }
        if (finalObstacle == 213)
        {
            obstacleToSpawn = oManager.BST;
        }
        if (finalObstacle == 223)
        {
            obstacleToSpawn = oManager.BMT;
        }
        if (finalObstacle == 233)
        {
            obstacleToSpawn = oManager.BLT;
        }
        if (finalObstacle == 313)
        {
            obstacleToSpawn = oManager.RST;
        }
        if (finalObstacle == 323)
        {
            obstacleToSpawn = oManager.BMT;
        }
        if (finalObstacle == 333)
        {
            obstacleToSpawn = oManager.BLT;
        }

        //V�lj plats f�r 
        if (spawnPosition == 1)//Upper spawn
        {
            float randomOffSet = Random.Range(-7, 7);
            Vector3 finalSpawn = new Vector3(upperSpawn.position.x + randomOffSet, upperSpawn.position.y, 0f);

            Instantiate(obstacleToSpawn, finalSpawn, upperSpawn.rotation);
        }

        if (spawnPosition == 2) //Left Spawn
        {
            Debug.Log("spawnPosition = leftSpawn");
            int randomHeight = Random.Range(5, 18);
            float randomHeightFloat = (float)randomHeight; 
            Vector3 finalHeight = new Vector3(leftSpawn.position.x, leftSpawn.position.y + randomHeightFloat, 0f);

            Instantiate(obstacleToSpawn, finalHeight, leftSpawn.rotation);
        }

        if (spawnPosition == 3) //Right Spawn   
        {
            Debug.Log("spawnPosition = rightSpawn");

            float randomHeight = Random.Range(5, 11);
            Vector3 finalHeight = new Vector3(rightSpawn.position.x, rightSpawn.position.y + randomHeight, 0f);

            Instantiate(obstacleToSpawn, finalHeight, rightSpawn.rotation);
        }
    }
}
