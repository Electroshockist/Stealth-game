using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] pickups;
    //public GameObject[] spawningPoints;
    public Transform[] spawnPointsTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (Input.GetKeyDown("y") && collision.gameObject.CompareTag("Player"))
          {

            Debug.Log("hit");
            int randomDrop = Random.Range(0, 2);
            int randomPickup = Random.Range(0, pickups.Length - 1);
            //int spawnPointIndex = Random.Range(0, spawningPoints.Length);
     
            if (randomDrop < 2)
            {
                Debug.Log("drop");
                Instantiate(pickups[randomPickup], spawnPointsTransform[randomPickup].position, spawnPointsTransform[randomPickup].rotation);

            }


            //            int spawnPointIndex = Random.Range(0, spawningPoints.Length);
            //////creates enemys then spawn them 

            //Instantiate(alien, spawnPointsTransform[spawnPointIndex].position, spawnPointsTransform[spawnPointIndex].rotation).GetComponent<Alien>().target = player;



        }

    }
}
