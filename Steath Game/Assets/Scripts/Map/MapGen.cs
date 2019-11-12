using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    public Transform tilePrefab;
    public Vector3 mapsize;



    [Range(0, 1)]
    public float tileOutline;
    // Start is called before the first frame update
    void Start()
    {
        GenrateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public  void GenrateMap()
    {
        
        for (int x = 0; x < mapsize.x;  x++)
        {
            for (int y = 0; y < mapsize.y; y++)
            {
                string mapcontrol = "GenrateMap";
                
                //if string is true it will destory tile 
                if (transform.Find(mapcontrol))
                {
                    DestroyImmediate(transform.Find(mapcontrol).gameObject);

                }
                //
                Transform mapholder = new GameObject(mapcontrol).transform;
                mapholder.parent = transform;




                //creates clones of tiles to change size of maps tile in x,y,z
                Vector3 tilepos = new Vector3(-mapsize.x / 2 + 0.5f + x, 0, -mapsize.y/2 + 0.5f + y);
                Transform newTiles = Instantiate(tilePrefab , tilepos , Quaternion.Euler(Vector3.right*90)) as Transform;

                // controls tile outline
                newTiles.localScale = Vector3.one * (1 - tileOutline);
                newTiles.parent = mapholder;
            }

        }

    }

    

}
