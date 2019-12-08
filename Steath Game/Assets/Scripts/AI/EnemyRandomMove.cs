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
    public GameObject player;
    public int enemyhealth = 4;
    public Transform eyes;
    private NavMeshAgent agent;
    static Animator animate;
    private string state = "idle";
    private float wait = 0f;
    private bool alert = false;
    private float awareness = 20f;
    public bool see = false;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animate = GetComponent<Animator>();
        agent.speed = 0.5f;
        animate.speed = 1.2f;
    }

     //this makes sure that the player is seen by enemy
    public void CheckPlayerinsight()
    {

        RaycastHit rayHit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(eyes.position, fwd * 0.5f, Color.red);

        if (Physics.Linecast(eyes.position, fwd * 0.5f, out rayHit))
        {

            if (rayHit.collider.gameObject.tag == "Player")
            {
                state = "chase";
                Debug.Log("chase");
                agent.SetDestination(player.transform.position);
                agent.speed = 0.5f;
                animate.SetBool("isWalking", true);



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

    {   CheckPlayerinsight();

        if (see == true)
        {
         
        }



        if (enemyhealth <= 0)
        {
            death();
        }

        if (player.GetComponent<Player>().alive)
        {
            animate.SetFloat("velocity", agent.velocity.magnitude);
            animate.SetBool("isWalking", true);
            //Idle
            if (state == "idle")
            {
                Debug.Log(state);
                //walk randomly within 20f; 
                Vector3 randomPos = Random.insideUnitSphere * awareness;
                NavMeshHit navHit;
                animate.SetBool("isWalking", true);
                /// enemy finds random place wiith 20f to walk around on nav mesh 
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);


                // if enemy is alerted it will follow player tracks  
                if (alert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                    animate.SetBool("isWalking", true);
                    awareness += 5f;
                    Debug.Log("awareness::" + awareness);
                    //enemy awares wil lower over time 
                    if (awareness > 20f)
                    {
                        alert = false;
                        agent.speed = 5.2f;
                        animate.speed = 1.2f;
                        agent.SetDestination(navHit.position);
                        Debug.Log("im alert");
                    }
                }
                agent.SetDestination(navHit.position);
                state = "Walk";
            }

            //walking 
            if (state == "Walk")
            {
         
                animate.SetBool("isWalking", true);
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    animate.SetBool("isWalking", true);
                    state = "search";
                    wait = 5f;
                    Debug.Log("search");

                }
            }


            //look for player
            if (state == "search")
            {
                if (wait > 0f)
                {
                    animate.SetBool("isWalking", false);
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
                animate.SetBool("isWalking", true);
                agent.SetDestination(player.transform.position);

                // enemy loses player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                print("Distance to other: " + distance);

                //search
                if (distance > 4f)
                {
                    state = "somethingwrong";
                    Debug.Log("somethingwrong");
                }

              else  if (distance< 1)
                {
                    //if player is alive kill them
                    if (player.GetComponent<Player>().alive)
                    {
                        agent.SetDestination(player.transform.position);
                        state = "kill";
                        Debug.Log("kill");
                    }

                }

                if (enemyhealth < 2)
                {

                    state = "Run";

                }
         
            }




            //edavde
            if (state == "Run")
            {
                animate.SetBool("isWalking", true);
                Vector3 enemypos = transform.position;
                float distance = Vector3.Distance(transform.position, player.transform.position);
                print("Distance to other: " + distance);
               Vector3 enemyMove = Vector3.zero;

                if (distance > 1f)
                {
                    if (enemypos.x > player.transform.position.x)
                    {
                        enemyMove.x *= 6;
                    }


                    if (enemypos.z > player.transform.position.z)
                    {
                        enemyMove.z *= 6;
                    }
                }

                enemypos += enemyMove;

            }

                //some thing is wrong look around
                if (state == "somethingwrong")
                {
                    if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                    {
                        animate.SetBool("isWalking", true);
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

                agent.SetDestination(player.transform.position);

          

                animate.speed = 2f;
                
                
                // enemy loses player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                print("Distance to other: " + distance);

                //search
                if (distance > 2f)
                {
                    animate.SetBool("isWalking", true);
                    animate.SetBool("isAttacking", false);
                    state = "somethingwrong";
                    Debug.Log("somethingwrong");
                }

                else if (distance < 1.0f)
                {
                    //if player is alive kill them
                    if (player.GetComponent<Player>().alive)
                    {
                       
                        animate.SetBool("isAttacking", true);
                        animate.SetBool("isWalking", false);
                        state = "kill";
                        Debug.Log("kill");
                    }

                }

            }
        
        }
    }

    //die//
    public void death()
    {


        animate.SetTrigger("Dead");
        animate.speed = 1f;


        enemy.Remove(gameObject);

        //totalenemy--;
        //Respawn();
      
    }


    void Respawn()
    {
        totalenemy++;
    }
}
