using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public int maxBoids;
    public GameObject boid;
    //public GameObject[] boids;
    List<GameObject> boids = new List<GameObject>();

    // obstacles
    GameObject[] obstacles;
    List<GameObject> obstacleList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < maxBoids; i++)
        {
            boids.Add(Instantiate(boid, Vector3.zero, transform.rotation));
        }

        foreach (GameObject boid in boids)
        {
            boid.SetActive(true);
            boid.GetComponent<Boid>().flock = this;
        }

        // obstacles
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacleList = new List<GameObject>(obstacles);
        Debug.Log(obstacles.Length);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<GameObject> GetBoids()
    {
        return boids;
    }

    public List<GameObject> GetObstacles()
    {
        return obstacleList;
    }
}
