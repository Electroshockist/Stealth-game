using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public int index;

 
    public void Load()
    {
      
            SceneManager.LoadScene(index);
        
    }
}
