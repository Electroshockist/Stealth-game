using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float projectileForce;
    public float lifeTime;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        if (projectileForce < 1.0f)
        {
            projectileForce = 10.0f;
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

    void OnCollisionEnter(Collision c)
    {
        Destroy(gameObject);
    }
}
