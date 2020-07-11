using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFollowBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    //public AudioClip pickupSFX;
    public static int maxCookiesPickUpCount = 0;
    public static int maxStarsPickUpCount = 0;
    public static int starsPickedUpCount = 0;
    public static int cookiesPickedUpCount = 0;
    public GameObject gs;

    public static GameObject[] cookies;
    public static GameObject[] stars;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameObject.FindGameObjectWithTag("GameState");
        if (gameObject.tag == "CollectableStar")
        {
            maxStarsPickUpCount++;
        }
        if (gameObject.tag == "CollectableCookie")
        {
            maxCookiesPickUpCount++;
        }
        starsPickedUpCount = 0;
        cookiesPickedUpCount = 0;
    }

    void Update()
    {
        stars = GameObject.FindGameObjectsWithTag("CollectableStar");
        cookies = GameObject.FindGameObjectsWithTag("CollectableCookie");
        if (SwitchController.isAwake)
        {
            foreach (GameObject cookie in cookies)
            {
                //cookie.SetActive(true);
                cookie.GetComponent<Renderer>().enabled = true;
                cookie.GetComponent<CircleCollider2D>().enabled = true;

            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Renderer>().enabled = false;
                star.GetComponent<CircleCollider2D>().enabled = false;
                //star.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject cookie in cookies)
            {
                cookie.GetComponent<Renderer>().enabled = false;
                cookie.GetComponent<CircleCollider2D>().enabled = false;
                //cookie.SetActive(false);
            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Renderer>().enabled = true;
                star.GetComponent<CircleCollider2D>().enabled = true;
                //star.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject parentObject = transform.parent.gameObject;
            Destroy(parentObject);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (gameObject.tag == "CollectableStar")
        {
            starsPickedUpCount++;
        }
        else
        {
            cookiesPickedUpCount++;
        }
        if (starsPickedUpCount == maxStarsPickUpCount && cookiesPickedUpCount == maxCookiesPickUpCount)
        {
            GameState.isLevelWon = true;
        }
    }
}
