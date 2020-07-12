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

    private string thirdLevel = "LevelThree";
    private string secondLevel = "LevelTwo";
    private string firstLevel = "Level-2";

    public static int currLevel = 1;

    private GameObject player;
    private PauseMenuScript pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        gameOverCanvas.SetActive(false);
        gameWonCanvas.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelWon)
        {
            player.GetComponent<PlayerMovement>().StopMovingSound();
            isLevelWon = false;
            isGameOver = false;
            LevelBeat();
        } else if (isGameOver)
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
        if (currLevel == 1)
        {
            print("reload");
            //isGameOver = false;
            Invoke("LoadFirstLevel", 2);
            
        }
        else if (currLevel == 2)
        {
            //isGameOver = false;
            Invoke("LoadSecondLevel", 2);

        }
        else if (currLevel == 3)
        {
            //isGameOver = false;
            Invoke("LoadThirdLevel", 2);
        }
        Invoke("ResetCollectables", 2);
        isGameOver = true;
    }

    public void LevelBeat()
    {
        Destroy(player.GetComponent<Rigidbody2D>());
        SoundManagerScript.PlaySound("Win");
        gameWonCanvas.SetActive(true);
        pauseMenu.PauseGameForLoadingScreen();
        if (currLevel == 1)
        {

            ++currLevel;
            //gameText.gameObject.SetActive(true);
            Invoke("LoadSecondLevel", 2);
        }
        else if (currLevel == 2)
        {
            ++currLevel;
            //gameText.gameObject.SetActive(true);
            Invoke("LoadThirdLevel", 2);

        }
        else if (currLevel == 3)
        {
           //gameText.text = "YOU WIN!";
            //gameText.gameObject.SetActive(true);
        }
        Invoke("ResetCollectables", 2);
    }

    public void ResetCollectables()
    {
        CollectableBehavior.maxCookiesPickUpCount = 0;
        CollectableBehavior.maxStarsPickUpCount = 0;
        CollectableBehavior.starsPickedUpCount = 0;
        CollectableBehavior.cookiesPickedUpCount = 0;
    }

    public void LoadThirdLevel()
    {
        SceneManager.LoadScene(thirdLevel);
    }

    public void LoadSecondLevel()
    {
        SceneManager.LoadScene(secondLevel);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevel);
    }
}