using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    public GameObject gs;

    public static GameObject[] vegetables;
    public static GameObject[] monsters;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        vegetables = GameObject.FindGameObjectsWithTag("EnemyVegetable");
        monsters = GameObject.FindGameObjectsWithTag("EnemyMonster");
        if (SwitchController.isAwake)
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.GetComponent<Renderer>().enabled = true;
                vegetable.GetComponent<CircleCollider2D>().enabled = true;
                //vegetable.SetActive(true);
            }
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<Renderer>().enabled = false;
                monster.GetComponent<CircleCollider2D>().enabled = false;
                //monster.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.GetComponent<Renderer>().enabled = false;
                vegetable.GetComponent<CircleCollider2D>().enabled = false;
                //vegetable.SetActive(false);
            }
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<Renderer>().enabled = true;
                monster.GetComponent<CircleCollider2D>().enabled = true;
                //monster.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            print("DEATH!");
            
            GameState.isGameOver = true;
            //var cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;

            //AudioSource.PlayClipAtPoint(pickupSFX, cameraPosition);
        }
    }
}
