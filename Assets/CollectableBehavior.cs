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
        ++maxPickUpCount;
        pickedUpCount = 0;

        stars = GameObject.FindGameObjectsWithTag("CollectableStar");
        cookies = GameObject.FindGameObjectsWithTag("CollectableCookie");
    }

    void Update()
    {
        print(stars);
        print("cookie" + cookies);
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
            GameObject parentObject = transform.parent.gameObject;
            Destroy(parentObject);
            Destroy(gameObject);
            GameObject all = GameObject.FindGameObjectWithTag("AllCollectablesEnemies");
            Transform[] allChildren = all.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                EnemyBehavior.monsters = new GameObject[0]; 
                EnemyBehavior.monsters.Add<GameObject>(child.gameObject);
            }

            if (gameObject.tag == "CollectableCookie")
            {
                EnemyBehavior.monsters = GameObject.FindGameObjectsWithTag("EnemyMonster");
            }
            else
            {
                EnemyBehavior.vegetables = GameObject.FindGameObjectsWithTag("EnemyVegetable");
            }
            //if (gameObject.tag == "CollectableCookie")
            //{
            //    stars = GameObject.FindGameObjectsWithTag("CollectableCookie");
            //}
            //else
            //{
            //    cookies = GameObject.FindGameObjectsWithTag("CollectableStar");
            //}
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
