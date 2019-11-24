using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class PathFinding : MonoBehaviour
{
    Transform waypoint;
    GameObject Enemy2;


    private float enemytimer = 10;

    public List<GameObject> enemy;
    public int totalenemy = 10;
    public GameObject[] pickups;
    public GameObject player;
    public int enemyhealth = 1;
    public Transform eyes;
    private NavMeshAgent agent;
    static Animator animate;
    private string state = "idle";
    private bool alive = true;
    private float wait = 0f;
    private bool alert = false;
    private float awareness = 20f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animate = GetComponent<Animator>();
        agent.speed = 10.2f;
        animate.speed = 1.2f;

    }






    public void CheckPlayerinsight()
    {
        if (alive)
        {
            RaycastHit rayHit;
            if (Physics.Linecast(eyes.position, player.transform.position, out rayHit))
            {
                // print("hit" + rayHit.collider.gameObject.name);
                if (rayHit.collider.gameObject.name == "Player")
                {

                    if (state != "kill")
                    {
                        state = "chase";
                        Debug.Log("chase");
                        agent.speed = 10.5f;
                        // animate.speed = 3.5f;

                    }
                }

            }

        }
    }












    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            animate.SetFloat("velocity", agent.velocity.magnitude);

            //Idle
            if (state == "idle")
            {
                     state = "Walk";

                //way point walk
            }

        }



        if (state == "Walk")
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
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
            agent.SetDestination(player.transform.position);

            // enemy loses player
            float distance = Vector3.Distance(transform.position, player.transform.position);
            print("Distance to other: " + distance);

            //search
            if (distance > 1f)
            {
                state = "somethingwrong";
                Debug.Log("somethingwrong");
            }

            else if (distance < 1)
            {
                //if player is alive kill them
                if (player.GetComponent<Player>().alive)
                {

                    state = "kill";
                    Debug.Log("kill");
                    player.GetComponent<Player>().alive = false;
                }

            }

            if (enemyhealth < 2)
            {

                state = "Walk";

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
                ;
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
