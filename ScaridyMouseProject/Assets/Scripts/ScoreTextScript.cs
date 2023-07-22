using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField]
    private GameManagerScript manager;
    private TextMeshProUGUI textMesh;
    private int scoreTextInt;
    private string scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        //References
        var gameManager = GameObject.FindWithTag("Manager"); //Get the gameManager object
        if (gameManager != null)
        {
            manager = gameManager.GetComponent<GameManagerScript>();    //Get the GameManagerScript from the GameManager object
        }

        textMesh = GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        scoreTextInt = (int)manager.scoreTime;
        scoreText = (scoreTextInt * 10).ToString();
        textMesh.text = scoreText;
    }
}
