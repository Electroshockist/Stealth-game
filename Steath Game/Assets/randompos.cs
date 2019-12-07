using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class randompos : MonoBehaviour
{
    float x;
    float y;
    float z;
    public Transform [] gamepos;
    Vector3 pos;
    int i;
   public float waittime = 20;
    void Start()
    {
       

    
    }

    // Update is called once per frame
    void Update()
    {
        x = Random.Range(0f, 4.0f);
        y = 0;
        z = Random.Range(0f, 4.0f);
        pos = new Vector3(x, y, z);
        waittime -= Time.deltaTime;
        NewPos();
    }

    void NewPos()
    {

        if (waittime <= 0) { 

            for ( i = 0; i < 3; i++)
            {
                gamepos[i].transform.position = pos;

                if(i == 3)
                {
                    i = 0;
                }
            }
            
            waittime = 20;
        }



    }


}
