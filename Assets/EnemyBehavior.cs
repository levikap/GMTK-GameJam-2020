using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //public AudioClip pickupSFX;
    public GameObject gs;

    // Start is called before the first frame update
    void Start()
    {
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
