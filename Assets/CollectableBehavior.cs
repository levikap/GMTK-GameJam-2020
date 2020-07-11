using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    public static int maxPickUpCount = 0;
    public static int pickedUpCount = 0;
    public GameObject gs;

    public static GameObject[] cookies;
    public static GameObject[] stars;

    // Start is called before the first frame update
    void Start()
    {
        maxPickUpCount++;
        pickedUpCount = 0;

    }

    void Update()
    {
        print("maxpickup " + maxPickUpCount);
        print("picked up " + pickedUpCount);
        stars = GameObject.FindGameObjectsWithTag("CollectableStar");
        cookies = GameObject.FindGameObjectsWithTag("CollectableCookie");
        if (SwitchController.isAwake)
        {
            foreach(GameObject cookie in cookies)
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
        } else
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
        pickedUpCount++;
        if (pickedUpCount == maxPickUpCount)
        {
            GameState.isLevelWon = true;
        }
    }
}
