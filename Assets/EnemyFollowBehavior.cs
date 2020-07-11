using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBehavior : MonoBehaviour
{

    public static GameObject[] vegetables;
    public static GameObject[] monsters;

    private GameObject player;

    public Animator animator;
    public float speed = 2f;

    public float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        AnimateIfPlayerClose();

        vegetables = GameObject.FindGameObjectsWithTag("EnemyVegetableFollow");
        monsters = GameObject.FindGameObjectsWithTag("EnemyMonster");
        if (SwitchController.isAwake)
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.GetComponent<Renderer>().enabled = true;
                vegetable.GetComponent<CapsuleCollider2D>().enabled = true;
                float step = speed * Time.deltaTime;
                vegetable.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
                //vegetable.SetActive(true);
            }
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<Renderer>().enabled = false;
                monster.GetComponent<CircleCollider2D>().enabled = false;
                float step = speed * Time.deltaTime;
                monster.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
                //monster.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.GetComponent<Renderer>().enabled = false;
                vegetable.GetComponent<CapsuleCollider2D>().enabled = false;
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


    private void AnimateIfPlayerClose()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (distanceToPlayer < 5.0f)
        {
            animator.SetBool("PlayerClose", true);

        }
        else
        {
            animator.SetBool("PlayerClose", false);
        }
    }
}
