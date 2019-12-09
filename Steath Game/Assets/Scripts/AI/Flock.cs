using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject[] boids;
    List<GameObject> boidsList = new List<GameObject>();

    //Use this for initialization
    void Start()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boidsList.Add(boids[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < boidsList.Capacity; i++)
            {
                boids[i].GetComponent<Boid>().flock = this;
                boids[i].GetComponent<Boid>().StartFly();
            }
        }
    }

    public List<GameObject> GetBoids()
    {
        return boidsList;
    }
}
