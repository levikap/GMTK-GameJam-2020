using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public static bool isAwake;
    public GameObject dreamObjects;
    public GameObject awakeObjects;

    public float time = 0.0f;
    public float randomTimeInterval = 10f;

    // Start is called before the first frame update
    void Start()
    {
        isAwake = true;
        dreamObjects.active = true;
        awakeObjects.active = false;
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
        FlashScreen();

        yield return new WaitForSecondsRealtime(1);
        isAwake = !isAwake;

        if (dreamObjects.active)
        {
            dreamObjects.active = false;
            awakeObjects.active = true;
        } else
        {
            dreamObjects.active = true;
            awakeObjects.active = false;
        }
    }

    private float CalculateRandomTime()
    {
        float random = Random.Range(2f, 10f);
        return random;
    }

    private void FlashScreen()
    {

    }
}

