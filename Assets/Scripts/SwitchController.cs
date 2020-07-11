using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public static bool isAwake;
    public GameObject dreamObjects;
    public GameObject awakeObjects;
    public GameObject player;
    public GameObject glow;
    public int timeToWait = 2;
    public static bool startGlow;

    public float time = 0.0f;
    public float waitTime = 1.0f;
    public float randomTimeInterval = 10f;
    public bool glowState = false;

    // Start is called before the first frame update
    void Start()
    {
        glow.SetActive(glowState);
        isAwake = true;
        dreamObjects.SetActive(false);
        awakeObjects.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= randomTimeInterval)
            {
                glow.SetActive(true);
                Invoke("Swap", 1);
                time = 0;
                randomTimeInterval = CalculateRandomTime();
         }

        time += UnityEngine.Time.deltaTime;
       
    }


    private void Swap()
    {
        glow.SetActive(false);
        isAwake = !isAwake;

        if (dreamObjects.activeSelf)
        {
            dreamObjects.SetActive(false);
            awakeObjects.SetActive(true);
        }
        else
        {
            dreamObjects.SetActive(true);
            awakeObjects.SetActive(false);
        }
    }

    private float CalculateRandomTime()
    {
        float random = Random.Range(2f, 10f);
        return random;
    }

    private IEnumerator WaitABit()
    {
        glow.SetActive(!glowState);

        Debug.Log("waiting!");
        yield return new WaitForSeconds(time);



        
    }
}


