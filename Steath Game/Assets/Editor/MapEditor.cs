using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MapEditor : MonoBehaviour
{
    [MenuItem("Tools/assign tile Mat")]
    public static void AssignTileMat()
    {
        GameObject[] tile = GameObject.FindGameObjectsWithTag("tile");
        Material material = Resources.Load<Material>("tile");



        foreach(GameObject t in tile)
        {
            t.GetComponent<Renderer>().material = material;

        }



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
