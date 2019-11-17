using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyRandomMove : MonoBehaviour
{
    private float enemytimer = 10;

    public List<GameObject> enemy;
    public int totalenemy = 10;
    public GameObject[] pickups;
    public GameObject player;
    public GameObject deathwatch;
    public Transform Campos;
    public int enemyhealth = 1;
    public Transform eyes;
    private NavMeshAgent agent;
    static Animator animate;
    private string state = "idle";
    private bool alive = true;
    private float wait = 0f;
    private bool alert = false;
    private float awareness = 20f;
    public Vector3 offset = new Vector3(5f, 0f, 0f);

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animate = GetComponent<Animator>();
        agent.speed = 10.2f;
        animate.speed = 1.2f;
    }

    // this makes sure that the player is seen by enemy 
    public void CheckPlayerinsight()
    {
        if (alive)
        {
            RaycastHit rayHit;
            if (Physics.Linecast(eyes.position, player.transform.position, out rayHit))
            {
                // print("hit" + rayHit.collider.gameObject.name);
                if (rayHit.collider.gameObject.name == "unitychan")
                {
                    if (state != "kill")
                    {
                        state = "chase";
                        agent.speed = 15.5f;
                        // animate.speed = 3.5f;

                    }
                }

            }

        }
    }



    void OnTriggerEnter(Collider other)
    {
        // if emeny is hit by bulllet health goes down 
        if (other.gameObject.tag == "Bullet")
        {
            enemyhealth = enemyhealth - 1;
            Debug.Log("hit");
        }



    }


 
    void Update()

    {
        ///  Debug.DrawLine(eyes.position, player.transform.position, Color.green);
        if (enemyhealth <= 0)
        {
            animate.SetTrigger("Dead");
            //enemy is dead 

            death();
        }

        if (alive)
        {
            animate.SetFloat("velocity", agent.velocity.magnitude);

            //Idle
            if (state == "idle")
            {
                //walk randomly within 20f;
                Vector3 randomPos = Random.insideUnitSphere * awareness;
                NavMeshHit navHit;

                /// enemy finds random place wiith 20f to walk around on nav mesh 
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);

                // if enemy is alerted it will follow player tracks 
                if (alert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);

                    awareness += 5f;
                    //enemy awares wil lower over time 
                    if (awareness > 20f)
                    {
                        alert = false;
                        agent.speed = 10.2f;
                        animate.speed = 1.2f;
                    }
                }
                agent.SetDestination(navHit.position);
                state = "Walk";
            }

            //walking 
            if (state == "Walk")
            {
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = "search";
                    wait = 5f;

                }
            }


            //look for player
            if (state == "search")
            {
                if (wait > 0f)
                {
                    wait -= Time.deltaTime;
                    transform.Rotate(0f, 120f * Time.deltaTime, 0f);
                }
                else
                {
                    state = "idle";
                }
            }


            //Chase
            if (state == "chase")
            {

                agent.destination = player.transform.position;

                // enemy loses player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > 10f)
                {
                    state = "somethingwrong";
                }

                //kill the player
                else if (agent.remainingDistance <= agent.stoppingDistance + 15f && !agent.pathPending)
                {

                    //if player is alive kill them
                    //if (player.GetComponent<realCharacter>().alive)
                    //{

                    //    state = "kill";
                    //    player.GetComponent<realCharacter>().alive = false;

                    //}

                }
            }

            //some thing is wrong look around
            if (state == "somethingwrong")
            {
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {

                    state = "search";
                    wait = 5f;
                    alert = true;
                    awareness = 5f;
                    CheckPlayerinsight();

                }
            }

            // will try to kill player 
            if (state == "kill")
            {
                animate.SetBool("isAttacking", true);
                animate.SetBool("isWalking", false);

                animate.speed = 1f;
                agent.SetDestination(player.transform.position);

            }
            else
            {
                animate.SetBool("isAttacking", false);
            }
        }
    }

    //die//
    public void death()
    {
        animate.speed = 1f;


        agent.gameObject.SetActive(false);


        /// enemy will drop items random when dead 
        int randomDrop = Random.Range(0, 2);
        int randomPickup = Random.Range(0, pickups.Length - 1);
        if (randomDrop < 2)
        {
            Instantiate(pickups[randomPickup], transform.position
           + new Vector3(0, 0.25f, 0), Quaternion.Euler(0, 0, 0));
        }


        enemy.Remove(gameObject);

        totalenemy--;
        Respawn();
        animate.SetTrigger("Dead");
    }


    //enemy respawn 
    void Respawn()
    {
        totalenemy++;
    }
}
