﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public static bool isAwake;
    public GameObject dreamObjects;
    public GameObject awakeObjects;
    public GameObject player;
    public int timeToWait = 2;
    public static bool playParticles;

    public float time = 0.0f;
    public float randomTimeInterval = 10f;

    // Start is called before the first frame update
    void Start()
    {
        isAwake = true;
        dreamObjects.SetActive(false);
        awakeObjects.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= randomTimeInterval)
            {
                Swap();
                time = 0;
                randomTimeInterval = CalculateRandomTime();
         }

        time += UnityEngine.Time.deltaTime;
       
    }


    private void Swap()
    {
        playParticles = true;
        Debug.Log("playing particles, swapping");
        //yield return new WaitForSecondsRealtime(1);
        isAwake = !isAwake;

        if (dreamObjects.activeSelf)
        {
            dreamObjects.SetActive(false);
            awakeObjects.SetActive(true);
        } else
        {
            dreamObjects.SetActive(true);
            awakeObjects.SetActive(false);
        }

        WaitABit();
     
        playParticles = false;
    }

    private float CalculateRandomTime()
    {
        float random = Random.Range(2f, 10f);
        return random;
    }

    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(time);
    }
}


