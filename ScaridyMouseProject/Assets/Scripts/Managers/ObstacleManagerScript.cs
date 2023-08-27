using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    //Variables for obstacles
    public AnimationCurve popCurve;  //bounceAnimation
    [Range(0f, 2f)]
    public float popTime = 0.5f;
    [Range(0f, 1f)]
    public float popScale = .2f;

    public float largeShakeMagnitude; 
    public float largeShakeRoughness;
    [Range(0f, 2f)]
    public float largeShakeDuration;

    public float maxGravitySqueeze;
    public float gravitySqueezeIndex;

    //List of all possible obstacles named in a systemic way 
    public GameObject GSC;
    
    public GameObject GMC;
    
    public GameObject GLC;
    
    public GameObject BSC;
    
    public GameObject BMC;
    
    public GameObject BLC;
    
    public GameObject RSC;
    
    public GameObject RMC;
    
    public GameObject RLC;
    
    public GameObject GSS;
    
    public GameObject GMS;
    
    public GameObject GLS;
    
    public GameObject BSS;
    
    public GameObject BMS;
    
    public GameObject BLS;
    
    public GameObject RSS;
    
    public GameObject RMS;
    
    public GameObject RLS;
    
    public GameObject GST;
    
    public GameObject GMT;
    
    public GameObject GLT;
    
    public GameObject BST;
    
    public GameObject BMT;
    
    public GameObject BLT;
    
    public GameObject RST;
    
    public GameObject RMT;
    
    public GameObject RLT;
}
