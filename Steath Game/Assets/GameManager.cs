using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    public GameObject[] spawningPoints;
    public GameObject[] Badguy;
    public GameObject player;

    NavMeshAgent navMeshAgent;
    public Transform[] spawnPointsTransform;
    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            // To do : Select one of the spawning points(spawningPoints) randomly
            // and then instantiate an alien object
            // Don't forget to set the alien's target to player
           
            navMeshAgent = Badguy[0].GetComponent<NavMeshAgent>();


            int spawnPointIndex = Random.Range(0, spawningPoints.Length);
            ////creates enemys then spawn them 

            Instantiate(Badguy[0], spawnPointsTransform[spawnPointIndex].position, spawnPointsTransform[spawnPointIndex].rotation);



            yield return new WaitForSeconds(10f);


        }
    }

    private void Update()
    {
        

    }
}
