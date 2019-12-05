using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EProjectile : MonoBehaviour
{
    public float speed;
    private Transform playerPos;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(playerPos.position.x, playerPos.position.y);

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {

            Destroy(gameObject);
        }
    }



   

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);
        }
    }
}
