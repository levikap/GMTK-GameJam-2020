using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private GameObject player;
    public bool isHittingWall = false;
    BoxCollider2D deathCollider;

    private void Start()
    {
        isHittingWall = false;
        player = GameObject.FindGameObjectWithTag("Player");
        deathCollider = GameObject.FindGameObjectWithTag("DeathSpawnCheck").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("GroundTile");
        foreach(GameObject wall in walls)
        {
            bool isHitting = wall.gameObject.GetComponent<WallBehavior>().isHittingWall;
            PlayerMovement.isHittingWall = PlayerMovement.isHittingWall || isHitting;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "WallChecker")
    //    {
    //        //player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //        player.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 1f, 0f));
    //        isHittingWall = true;
    //        print("fall!");
    //    }
    //    else
    //    {
    //        isHittingWall = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathSpawnCheck")
        {
            GameState.isGameOver = true;
        }
        if (collision.gameObject.tag == "WallChecker")
        {
            //deathCollider.enabled = false;
            // player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                        isHittingWall = true;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 1f, 0f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallChecker")
        {
            //deathCollider.enabled = false;
            // player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            isHittingWall = true;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 1f, 0f));
        } else
        {
            isHittingWall = false;
            //deathCollider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallChecker")
        {
            isHittingWall = false;
            PlayerMovement.isHittingWall = false;
        }
    }
}
