using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EProjectile : MonoBehaviour
{

  
    private Vector3 target;
    private Player player;


    public float projectileForce;
    public float lifeTime;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

        //target = new Vector3(player.transform.position.x, player.transform.position.y);
        if (projectileForce < 1.0f)
        {
            projectileForce = 2.0f;
            Debug.LogWarning("ProjectileForce defaulting to " + projectileForce);
        }

        if (lifeTime < 1.0f)
        {
            lifeTime = 2.0f;
            Debug.LogWarning("LifeTime defaulting to " + lifeTime);
        }

        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            //throw new System.ArgumentNullException("Nothing Entered");
        }


        rb.useGravity = false;
        rb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        Destroy(gameObject, lifeTime);

    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
