using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public bool IsitPaused;
    public GameObject PauseMenu;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (IsitPaused)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsitPaused = !IsitPaused;

        }
	}
}
