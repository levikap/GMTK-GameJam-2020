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

    public static GameObject[] cookies;
    public static GameObject[] stars;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "CollectableFollowStar")
        {
            maxStarsPickUpCount++;
        }
        if (gameObject.tag == "CollectableFollowCookie")
        {
            maxCookiesPickUpCount++;
        }
        starsPickedUpCount = 0;
        cookiesPickedUpCount = 0;
    }

    void Update()
    {
        stars = GameObject.FindGameObjectsWithTag("CollectableFollowStar");
        cookies = GameObject.FindGameObjectsWithTag("CollectableFollowCookie");
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
                Transform vegetable = gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>()[1];
                star.GetComponent<Transform>().position = vegetable.position;
                //star.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject cookie in cookies)
            {
                cookie.GetComponent<Renderer>().enabled = false;
                cookie.GetComponent<CircleCollider2D>().enabled = false;
                Transform monster = gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>()[1];
                cookie.GetComponent<Transform>().position = monster.position;
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
        if (gameObject.tag == "CollectableFollowStar")
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
