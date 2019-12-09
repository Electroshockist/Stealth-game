using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{

    public Transform player;
    static Animator animate;


    void Start()
    {

        animate = GetComponent<Animator>();
    }


    void Update()
    {

        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);


            animate.SetBool("isIdle", false);
            if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.05f);
                animate.SetBool("isWalking", true);
                animate.SetBool("isAttacking", false);
            }
            else
            {
                animate.SetBool("isAttacking", true);
                animate.SetBool("isWalking", false);
            }
        }
        else
        {
            animate.SetBool("isIdle", true);
            animate.SetBool("isWalking", false);
            animate.SetBool("isAttacking", false);
        }


    }


}
