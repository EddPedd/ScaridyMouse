using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2
{
    public int currentBounces;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ObstacleColor{
    public int maxBounces;
    public Color color;
    
}

public class ObstacleSieze{
    public float scale;
    public float linearDrag;
    public float squeezeIndx;

}

public class ObstacleShape{
    public Sprite spriteShape;

    public virtual void OnCollisionEnter2D(Collider2D collider){
        //To override for each instance of shape
    }
}

public class Program{
    public ObstacleShape Square; 
    public ObstacleShape triangle; 
    public ObstacleShape circle; 

    public ObstacleColor red;
    public ObstacleColor blue;
    public ObstacleColor green;

    public ObstacleSieze small;
    public ObstacleSieze medium;
    public ObstacleSieze large;
}


