using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static bool isGameOver = false;
    public static bool isLevelWon = false;

    public GameObject gameOverCanvas;
    public GameObject gameWonCanvas;

    private string firstLevel = "Level1";
    private string secondLevel = "Level2";
    private string thirdLevel = "Level3";
    private string fourthLevel = "Level4";

    public static int currLevel = 1;

    private GameObject player;
    private PauseMenuScript pauseMenu;

    public string currLevelName;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        gameOverCanvas.SetActive(false);
        gameWonCanvas.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuScript>();
        currLevelName = SceneManager.GetActiveScene().name;
        switch(currLevelName)
        {
            case "Level1":
                currLevel = 1;
                break;
            case "Level2":
                currLevel = 2;
                break;
            case "Level3":
                currLevel = 3;
                break;
            case "Level4":
                currLevel = 4;
                break;
            default:
                currLevel = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(currLevel);
        if (isLevelWon)
        {
            player.GetComponent<PlayerMovement>().StopMovingSound();
            isLevelWon = false;
            isGameOver = false;
            LevelBeat();
        }
        else if (isGameOver)
        {
            player.GetComponent<PlayerMovement>().StopMovingSound();
            LevelLost();
            isGameOver = false;
        }
    }

    public void LevelLost()
    {
        Destroy(player.GetComponent<Rigidbody2D>());
        SoundManagerScript.PlaySound("Death");
        gameOverCanvas.SetActive(true);
        pauseMenu.PauseGameForLoadingScreen();
        Invoke("RestartLevel", 2);
        //if (currLevel == 1)
        //{
        //    print("reload");
        //    //isGameOver = false;
        //    Invoke("LoadFirstLevel", 2);

        //}
        //else if (currLevel == 2)
        //{
        //    //isGameOver = false;
        //    Invoke("LoadSecondLevel", 2);

        //}
        //else if (currLevel == 3)
        //{
        //    //isGameOver = false;
        //    Invoke("LoadThirdLevel", 2);
        //}
        Invoke("ResetCollectables", 2);
        isGameOver = true;
    }

    public void LevelBeat()
    {
        Destroy(player.GetComponent<Rigidbody2D>());
        SoundManagerScript.PlaySound("Win");
        gameWonCanvas.SetActive(true);
        pauseMenu.PauseGameForLoadingScreen();
        Invoke("LoadNextLevel", 2);
        Invoke("ResetCollectables", 2);
    }

    public void ResetCollectables()
    {
        CollectableBehavior.maxCookiesPickUpCount = 0;
        CollectableBehavior.maxStarsPickUpCount = 0;
        CollectableBehavior.starsPickedUpCount = 0;
        CollectableBehavior.cookiesPickedUpCount = 0;
    }

    public void LoadNextLevel()
    {
        switch (currLevel)
        {
            case 1:
                ++currLevel;
                SceneManager.LoadScene(secondLevel);
                break;
            case 2:
                ++currLevel;
                SceneManager.LoadScene(thirdLevel);
                break;
            case 3:
                ++currLevel;
                SceneManager.LoadScene(fourthLevel);
                break;
            default:
                break;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(currLevelName);
    }
}