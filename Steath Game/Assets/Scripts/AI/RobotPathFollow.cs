using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RobotPathFollow : MonoBehaviour
{
    private float awareness = 20f;
    private string state = "idle";
    private float wait = 0f;
    public GameObject player;
    public int badguyhealth = 1;
    public Transform eyes;
    private NavMeshAgent agent;
    static Animator animate;


    public Transform[] path;
    private float speed = 1.0f;
    private int current = 0;
    private float disreached = 1.0f;
    public bool seeyou = false;





    public Transform bulletSpawn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animate = GetComponent<Animator>();
        agent.speed = 1.0f;
        animate.speed = 1.2f;
        //Bshoottime = starttimeshot;
        state = "idle";
    }




    void Pathfollowing()
    {
        //total distance between pathfolling and enemy pos
        float dist = Vector3.Distance(path[current].position, transform.position);
        //enemy will move towards curennnt path position
        Vector3 pos = Vector3.MoveTowards(transform.position, path[current].position, speed * Time.deltaTime);


        transform.position = pos;

        if (dist <= disreached)
        {
            current++;
        }

        if (current >= path.Length)
        {

            current = 0;

        }

        animate.SetBool("isWalking", true);
        agent.SetDestination(path[current].transform.position);


        state = "Walk";

    }



    public void CheckPlayerinsight()
    {

        RaycastHit rayHit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(eyes.position, fwd, Color.red);

        if (Physics.Linecast(eyes.position, fwd * 6.0f, out rayHit))
        {

            if (rayHit.collider.gameObject.tag == "Player")
            {

                state = "chase";
                Debug.Log("chase");
                agent.speed = 1.0f;
                animate.SetBool("isWalking", true);


            }

        }
    }





    // Update is called once per frame
    void Update()
    {

        CheckPlayerinsight();


        if (player.GetComponent<Player>().alive)
        {
            //animate.SetFloat("velocity", agent.velocity.magnitude);
            animate.SetBool("isWalking", true);
            //Idle
            if (state == "idle")
            {
                Pathfollowing();
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
                    state = "idle";
                }

                else if (distance < 2)
                {
                    //if player is alive kill them
                    if (player.GetComponent<Player>().alive)
                    {
                        agent.SetDestination(player.transform.position);
                        state = "kill";
                        Debug.Log("kill");
                    }

                }



            }



            // will try to kill player 
            if (state == "kill")
            {

                agent.SetDestination(player.transform.position);

                animate.SetBool("isAttacking", true);
                animate.SetBool("isWalking", false);

                animate.speed = 1f;


                // enemy loses player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                print("Distance to other: " + distance);

                //search
                if (distance > 4f)
                {
                    animate.SetBool("isWalking", true);
                    animate.SetBool("isAttacking", true);
                    state = "idle";
                }

                else if (distance < 2)
                {
                    //if player is alive kill them
                    if (player.GetComponent<Player>().alive)
                    {
                        animate.SetBool("isWalking", false);
                        animate.SetBool("isAttacking", true);
                        state = "kill";
                        Debug.Log("kill");


                    }

                }

            }
            else
            {
                animate.SetBool("isAttacking", false);
            }
        }

    }



    void OnTriggerEnter(Collider other)
    {
        // if emeny is hit by bulllet health goes down 
        if (other.gameObject.tag == "Bullet")
        {
            badguyhealth = badguyhealth - 1;
            Debug.Log("hit");
        }
    }
}
