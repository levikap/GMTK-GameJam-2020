﻿using System.Collections;
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
    private string firstLevel = "LevelOne";

    public static int currLevel = 1;

    GameObject player;

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
        }
        if (isLevelWon)
        {
            LevelBeat();
        }
    }

    public void LevelLost()
    {
        isGameOver = true;
        if (currLevel == 1)
        {
            Invoke("LoadFirstLevel", 2);
        }
        else if (currLevel == 2)
        {
            Invoke("LoadSecondLevel", 2);

        }
        else if (currLevel == 3)
        {
            Invoke("LoadThirdLevel", 2);
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
        SceneManager.LoadScene(firstLevel);
    }
}