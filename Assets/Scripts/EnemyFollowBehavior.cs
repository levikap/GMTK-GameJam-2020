using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBehavior : MonoBehaviour
{

    public static GameObject[] vegetables;
    public static GameObject[] monsters;

    public static GameObject[] vegetablesLimited;
    public static GameObject[] monstersLimited;

    private GameObject player;

    public Animator animator;
    public float speed = 1f;

    public float distanceToPlayer;

    private bool hitPos1;

    // Start is called before the first frame update
    void Start()
    {
        hitPos1 = false;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        AnimateIfPlayerClose();

        vegetables = GameObject.FindGameObjectsWithTag("EnemyVegetableFollow");
        monsters = GameObject.FindGameObjectsWithTag("EnemyMonsterFollow");
        vegetablesLimited = GameObject.FindGameObjectsWithTag("EnemyLimitedVeggie");
        monstersLimited = GameObject.FindGameObjectsWithTag("EnemyLimitedMonster");
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
            foreach (GameObject vegetable in vegetablesLimited)
            {
                vegetable.GetComponent<Renderer>().enabled = true;
                vegetable.GetComponent<CapsuleCollider2D>().enabled = true;
                LimitedFollow limitedFollow = vegetable.GetComponent<LimitedFollow>();
                float step = speed * Time.deltaTime;
                //print(hitPos1);
                print(limitedFollow.limitedPosition2.position);
                print(limitedFollow.limitedPosition1.position);
                if (hitPos1)
                {
                    vegetable.transform.position = Vector2.MoveTowards(vegetable.transform.position, limitedFollow.targetPos.position, step);
                    if (Vector2.Distance(vegetable.transform.position, limitedFollow.targetPos.position) <= 0.2f)
                    {
                        hitPos1 = false;
                        limitedFollow.targetPos = vegetable.GetComponent<LimitedFollow>().limitedPosition1;
                    }
                } else
                {
                    //print(Vector2.Distance(vegetable.transform.position, limitedFollow.targetPos.position));
                    vegetable.transform.position = Vector2.MoveTowards(vegetable.transform.position, limitedFollow.targetPos.position, step);
                    if (Vector2.Distance(vegetable.transform.position, limitedFollow.targetPos.position) <= 0.2f)
                    {
                        hitPos1 = true;
                        limitedFollow.targetPos = vegetable.GetComponent<LimitedFollow>().limitedPosition2;
                    }
                }
            }
            foreach (GameObject monster in monstersLimited)
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
            foreach (GameObject vegetable in vegetablesLimited)
            {
                vegetable.GetComponent<Renderer>().enabled = false;
                vegetable.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            foreach (GameObject monster in monstersLimited)
            {
                monster.GetComponent<Renderer>().enabled = true;
                monster.GetComponent<CircleCollider2D>().enabled = true;
                LimitedFollow limitedFollow = monster.GetComponent<LimitedFollow>();
                float step = speed * Time.deltaTime;
                if (hitPos1)
                {
                    monster.transform.position = Vector2.MoveTowards(monster.transform.position, limitedFollow.targetPos.position, step);
                    if (Vector2.Distance(monster.transform.position, limitedFollow.targetPos.position) <= 0.2f)
                    {
                        hitPos1 = false;
                        limitedFollow.targetPos = monster.GetComponent<LimitedFollow>().limitedPosition1;
                    }
                }
                else
                {
                    monster.transform.position = Vector2.MoveTowards(monster.transform.position, limitedFollow.targetPos.position, step);
                    if (Vector2.Distance(monster.transform.position, limitedFollow.targetPos.position) <= 0.2f)
                    {
                        hitPos1 = true;
                        limitedFollow.targetPos = monster.GetComponent<LimitedFollow>().limitedPosition2;
                    }
                }
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
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < 2.5f)
        {
            animator.SetBool("PlayerClose", true);

        }
        else
        {
            animator.SetBool("PlayerClose", false);
        }
    }
}
