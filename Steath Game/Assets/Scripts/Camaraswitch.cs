using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camaraswitch : MonoBehaviour
{

    public GameObject camoneThird;
    public GameObject camtwoTop;


    AudioListener camaudio1;
    AudioListener camaudio2;

    // Start is called before the first frame update
    void Start()
    {
        camaudio1 = camoneThird.GetComponent<AudioListener>();
        camaudio2 = camtwoTop.GetComponent<AudioListener>();

   
    }

    // Update is called once per frame
    void Update()
    {
        switchCam();   
    }

    void switchCam()
    {
      
        if (Input.GetKeyDown(KeyCode.E))
        {
            camoneThird.SetActive((true));
            camaudio1.enabled = true;


            camtwoTop.SetActive((false));
            camaudio2.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            camoneThird.SetActive((false));
            camaudio1.enabled = false;


            camtwoTop.SetActive((true));
            camaudio2.enabled = true;
        }
    }
}
