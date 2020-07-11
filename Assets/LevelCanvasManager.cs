using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvasManager : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level - " + GameState.currLevel;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + CollectableBehavior.pickedUpCount + "/" + CollectableBehavior.maxPickUpCount;
    }
}
