using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    //References
    private GameObject gameManager;
    private GameManagerScript manager;
    
    //Static Variables
    public static TransitionScript instance;

    //Variables
    private bool hasTransitioned = false;
    private float startPosition;
    private float middleOfScreen = 0f;
    [SerializeField]
    private float endPosition = -16f;
    [SerializeField]
    private AnimationCurve curve; 
    [SerializeField]
    private float transitionDuration;
    private float percentageComplete;
    private float transitionTime;
    private float currentPosition;
    
   void Awake(){
    if(instance == null ){
        instance=this;
    }
    else{
        GameObject.Destroy(gameObject  );
    }

    DontDestroyOnLoad(gameObject);
   }

    void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
        manager = gameManager.GetComponent<GameManagerScript>();
        
        startPosition = transform.position.y;

        //transform.position = new Vector3 (0f, startPosition, 0f);   //Make sure hat the image is centered on x and z axis

        //if(startPosition > 1  ){      
       //     endPosition = middleOfScreen;   //Else it remains as the predetermined value of (0f, -16, 0f)
       // }  

        Debug.Log(gameObject.name + "starts at the y position " + startPosition + " and is going to teh y position " + endPosition);             
    }

    void Update()
    {
        if(transform.position.y <= 0f && hasTransitioned == false){
            hasTransitioned = true;
            manager.LoadNextScene();
        }
        
        
        transitionTime += Time.deltaTime;
        percentageComplete = transitionTime / transitionDuration;
        float curveMultiplier = curve.Evaluate(percentageComplete);

        if (percentageComplete >= 1){
            if ( endPosition < -1){  //If the transitionImage has passed the screen and a New scene has been loaded
                GameObject.Destroy(gameObject); //Destroy the transition Image and leave room for the rest of the scene
                return;
            }   
            else{                   //Else (If the transitionImage is in the middle of the screen) load the next scene
                //manager.LoadNextScene();    //OBS!! Load the next scene here if not using a "dontDestroyOnLoad" method and an instance
                GameObject.Destroy(gameObject);
                return;
            }               
        }
        else {
            currentPosition = Mathf.Lerp(startPosition, endPosition, curveMultiplier);
        }

        transform.position = new Vector3(0f, currentPosition, 0f);        
        
        //transform.position.y = Vector3.Lerp(startPosition, endPosition, )

    }
}
