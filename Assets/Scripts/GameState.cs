using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static bool isGameOver = false;
    public static bool isLevelWon = false;

    private string thirdLevel = "LevelThree";
    private string secondLevel = "LevelTwo";
    private string firstLevel = "Level-1";

    public static int currLevel = 1;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            
            LevelLost();
            isGameOver = false;
        }
        if (isLevelWon)
        {
            isLevelWon = false;
            LevelBeat();
        }
    }

    public void LevelLost()
    {
        CollectableBehavior.maxCookiesPickUpCount = 0;
        CollectableBehavior.maxStarsPickUpCount = 0;
        CollectableBehavior.starsPickedUpCount = 0;
        CollectableBehavior.cookiesPickedUpCount = 0;
        if (currLevel == 1)
        {
            //isGameOver = false;
            Invoke("LoadFirstLevel", 1);
            
        }
        else if (currLevel == 2)
        {
            //isGameOver = false;
            Invoke("LoadSecondLevel", 1);

        }
        else if (currLevel == 3)
        {
            //isGameOver = false;
            Invoke("LoadThirdLevel", 1);
        }
    }

    public void LevelBeat()
    {
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
        isGameOver = false;
        SceneManager.LoadScene(firstLevel);
    }
}