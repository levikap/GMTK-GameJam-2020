using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    public GameObject gs;

    private GameObject[] vegetables;
    private GameObject[] monsters;

    // Start is called before the first frame update
    void Start()
    {
        vegetables = GameObject.FindGameObjectsWithTag("EnemyVegetable");
        monsters = GameObject.FindGameObjectsWithTag("EnemyMonster");
    }

    void Update()
    {
        if (SwitchController.isAwake)
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.SetActive(true);
            }
            foreach (GameObject monster in monsters)
            {
                monster.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.SetActive(false);
            }
            foreach (GameObject monster in monsters)
            {
                monster.SetActive(true);
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
