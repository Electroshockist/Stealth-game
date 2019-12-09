using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
    public Transform target;

    SpacialPartition partition;
    AStar aStar = new AStar();

    List<Node> path = null;

    // Start is called before the first frame update
    void Start() {
        partition = GetComponent<SpacialPartition>();
        partition.setup();

        updatePath();

        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update() {
        if(onPlayerMove()) {
            updatePath();
        }

        //move();
    }

    //void move() {
    //    SpacialPartition.Node3D n = (SpacialPartition.Node3D)path[i];

    //    float distance = Vector3.Distance(transform.position, n.position);

    //    Vector3 tempPos = Vector3.MoveTowards(transform.position, n.position, distance / Time.deltaTime);
    //    transform.position = tempPos;
    //}

    IEnumerator test() {
        for(int i = 0; i < path.Count; i++) {
            SpacialPartition.Node3D n = (SpacialPartition.Node3D)path[i];
            transform.position = n.position;
            yield return new WaitForSeconds(1);
        }
    }

    bool onPlayerMove() {
        return
            Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);
    }


    void updatePath() {
        SpacialPartition.Node3D start = new SpacialPartition.Node3D(transform.position, partition);
        SpacialPartition.Node3D goal = new SpacialPartition.Node3D(target.position, partition);

        partition.addStart(start);
        partition.addEnd(goal);

        path = aStar.Search(start, goal, partition.getNodes());
    }
}
