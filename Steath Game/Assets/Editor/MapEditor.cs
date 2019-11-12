using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (MapGen))]
public class MapEditor : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapGen map = target as MapGen;
        map.GenrateMap();
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
