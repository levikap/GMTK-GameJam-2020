using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFollowBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    //public AudioClip pickupSFX;

    public static GameObject[] cookies;
    public static GameObject[] stars;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "CollectableFollowStar")
        {
            CollectableBehavior.maxStarsPickUpCount++;
        }
        if (gameObject.tag == "CollectableFollowCookie")
        {
            CollectableBehavior.maxCookiesPickUpCount++;
        }
        CollectableBehavior.starsPickedUpCount = 0;
        CollectableBehavior.cookiesPickedUpCount = 0;
    }

    void Update()
    {
        stars = GameObject.FindGameObjectsWithTag("CollectableFollowStar");
        cookies = GameObject.FindGameObjectsWithTag("CollectableFollowCookie");
        if (SwitchController.isAwake)
        {
            foreach (GameObject cookie in cookies)
            {
                cookie.GetComponent<Renderer>().enabled = true;
                cookie.GetComponent<CircleCollider2D>().enabled = true;

            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Renderer>().enabled = false;
                star.GetComponent<CircleCollider2D>().enabled = false;
                Transform vegetable = star.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>()[2];
                star.gameObject.transform.position = vegetable.position;
            }
        }
        else
        {
            foreach (GameObject cookie in cookies)
            {
                cookie.GetComponent<Renderer>().enabled = false;
                cookie.GetComponent<CircleCollider2D>().enabled = false;
                Transform monster = cookie.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>()[2];
                cookie.gameObject.transform.position = monster.position;
            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Renderer>().enabled = true;
                star.GetComponent<CircleCollider2D>().enabled = true;
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
            CollectableBehavior.starsPickedUpCount++;
        }
        else
        {
            CollectableBehavior.cookiesPickedUpCount++;
        }
        if (CollectableBehavior.starsPickedUpCount == CollectableBehavior.maxStarsPickUpCount &&
            CollectableBehavior.cookiesPickedUpCount == CollectableBehavior.maxCookiesPickUpCount)
        {
            GameState.isLevelWon = true;
        }
    }
}
