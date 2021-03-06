﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwitchController : MonoBehaviour
{
    public static bool isAwake;
    private GameObject dreamObjects;
    private GameObject awakeObjects;
    private GameObject player;
    private GameObject glow;
    public int timeToWait = 2;
    public static bool startGlow;
    public float lowerSwitchIntervalBound;
    public float higherSwitchIntervalBound;
    [HideInInspector]
    public DoubleBGMPlayer musicPlayer;

    public float time = 0.0f;
    public float waitTime = 1.0f;
    public float randomTimeInterval;
    public static bool glowState = false;

    // Start is called before the first frame update
    void Start()
    {
        dreamObjects = GameObject.FindGameObjectWithTag("DreamObjects");
        awakeObjects = GameObject.FindGameObjectWithTag("AwakeObjects");
        player = GameObject.FindGameObjectWithTag("Player");
        glow = GameObject.FindGameObjectWithTag("Glow");
        glow.SetActive(glowState);
        isAwake = true;
        dreamObjects.SetActive(false);
        awakeObjects.SetActive(true);
        musicPlayer = DoubleBGMPlayer.singular;
    }

    // Update is called once per frame
    void Update()
    {
        SwapEveryTime();

    }

    private void SwapEveryTime()
    {
        if (time >= randomTimeInterval)
        {
            glow.SetActive(true);
            SoundManagerScript.PlaySound("TransitionWarning");
            Invoke("Swap", 2);
            time = 0;
            randomTimeInterval = CalculateRandomTime(false);
        }

        time += UnityEngine.Time.deltaTime;
    }

    public void Swap()
    {
        //SoundManagerScript.PlaySound("Transition1");
        if(musicPlayer != null)
        {
            musicPlayer.swapSongs();
        }
        glow.SetActive(false);
        isAwake = !isAwake;

        if (dreamObjects.activeSelf)
        {
            SoundManagerScript.PlaySound("TransitionToAwake");
            dreamObjects.SetActive(false);
            awakeObjects.SetActive(true);

        }
        else
        {
            SoundManagerScript.PlaySound("TransitionToSleep");
            dreamObjects.SetActive(true);
            awakeObjects.SetActive(false);
        }
    }

    public float CalculateRandomTime(bool doItNow)
    {
        if (doItNow)
        {
            randomTimeInterval = 2.0f;
            return 2.0f;
        }
        else
        {
            float random = Random.Range(lowerSwitchIntervalBound, higherSwitchIntervalBound);
            return random;
        }
    }

    private IEnumerator WaitABit()
    {
        glow.SetActive(!glowState);

        Debug.Log("waiting!");
        yield return new WaitForSeconds(time);

    }
}


