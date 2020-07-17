using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvasManager : MonoBehaviour
{
    public Text scoreCookiesText;
    public Text scoreStarsText;
    public Text levelText;

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.isGamePaused)
        {
            levelText.text = "Level - " + GameState.currLevel;
            scoreCookiesText.text = "Cookies: " + CollectableBehavior.cookiesPickedUpCount + "/" + CollectableBehavior.maxCookiesPickUpCount;
            scoreStarsText.text = "Stars: " + CollectableBehavior.starsPickedUpCount + "/" + CollectableBehavior.maxStarsPickUpCount;
        } else
        {
            levelText.text = "";
            scoreCookiesText.text = "";
            scoreStarsText.text = "";
        }
        if (GameState.isGameCompleted)
        {
            levelText.text = "";
            scoreCookiesText.text = "";
            scoreStarsText.text = "";
        }
    }
}
