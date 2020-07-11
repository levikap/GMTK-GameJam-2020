using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    public static int maxPickUpCount = 0;
    public static int pickedUpCount = 0;
    public GameObject gs;

    private GameObject[] cookies;
    private GameObject[] stars;

    // Start is called before the first frame update
    void Start()
    {
        ++maxPickUpCount;
        pickedUpCount = 0;

        stars = GameObject.FindGameObjectsWithTag("CollectableStar");
        cookies = GameObject.FindGameObjectsWithTag("CollectableCookie");
    }

    void Update()
    {
        SwitchController.isAwake = false;
        if (SwitchController.isAwake)
        {
            foreach(GameObject cookie in cookies)
            {
                cookie.SetActive(true);
            }
            foreach (GameObject star in stars)
            {
                star.SetActive(false);
            }
        } else
        {
            foreach (GameObject cookie in cookies)
            {
                cookie.SetActive(false);
            }
            foreach (GameObject star in stars)
            {
                star.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //var cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

            //AudioSource.PlayClipAtPoint(pickupSFX, cameraPosition);
            Destroy(gameObject);
        }
    }

   void OnDestroy()
    {
        pickedUpCount++;
        if (pickedUpCount == maxPickUpCount)
        {
            GameState.isLevelWon = true;
        }
        //if (!GameState.isGameOver)
        //{
        //    if (pickupCount <= 0)
        //    {
        //        gs.GetComponent<GameState>().LevelBeat();
        //    }
        //}
    }
}
