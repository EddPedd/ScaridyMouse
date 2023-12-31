using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerM;
    public GameObject menu;
    public GameObject inGameUI;
    public GameObject spawner;
    [SerializeField]
    public float scoreTime; //Score time to send to UI
    private bool trackingScore = true;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerM = player.GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if(trackingScore)   //If the game has begun start tracking the time to use for score
        {
            scoreTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    public void Play()
    {
        menu.SetActive(false);
        inGameUI.SetActive(true);
        spawner.SetActive(true );
        trackingScore = true;
        AudioManagerScript.instance.Play("Laughter");
    }

    public void OpenMenu()
    {
        spawner.SetActive(false ); //Sort of a pause for now
        menu.SetActive(true);
        inGameUI.SetActive(false);
        trackingScore=false;

        //Add some sort of pause to the game so that the spawner stops and resets
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene(){
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex + 1);    
        }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
