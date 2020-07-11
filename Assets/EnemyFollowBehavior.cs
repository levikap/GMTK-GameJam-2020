using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBehavior : MonoBehaviour
{

    public static GameObject[] vegetables;
    public static GameObject[] monsters;

    private GameObject player;

    public Animator animator;
    public float speed = 1f;

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
        monsters = GameObject.FindGameObjectsWithTag("EnemyMonsterFollow");
        if (SwitchController.isAwake)
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.GetComponent<Renderer>().enabled = true;
                vegetable.GetComponent<CapsuleCollider2D>().enabled = true;
                float step = speed * Time.deltaTime;
                vegetable.transform.position = Vector2.MoveTowards(vegetable.transform.position, player.transform.position, step);
            }
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<Renderer>().enabled = false;
                monster.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject vegetable in vegetables)
            {
                vegetable.GetComponent<Renderer>().enabled = false;
                vegetable.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<Renderer>().enabled = true;
                monster.GetComponent<CircleCollider2D>().enabled = true;
                float step = speed * Time.deltaTime;
                monster.transform.position = Vector2.MoveTowards(monster.transform.position, player.transform.position, step);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            print("DEATH!");

            GameState.isGameOver = true;
        }
    }


    private void AnimateIfPlayerClose()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
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
