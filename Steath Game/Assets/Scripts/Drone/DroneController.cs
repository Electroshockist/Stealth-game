using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
    public Transform target;

    float elapsedTime = 0;

    float updateTime = 10;

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
        elapsedTime += Time.deltaTime;
        if(onPlayerMove()) {
            updatePath();
        }

        //move();
    }

    //void move(int i) {
    //    SpacialPartition.Node3D n = (SpacialPartition.Node3D)path[i];

    //    float distance = Vector3.Distance(transform.position, n.position);

    //    Vector3 tempPos = Vector3.MoveTowards(transform.position, n.position, distance / Time.deltaTime);
    //    transform.position = tempPos;
    //}

    IEnumerator test() {
        for(int i = 0; i < path.Count; i++) {
            SpacialPartition.Node3D n = (SpacialPartition.Node3D)path[i];
            transform.position = new Vector3(n.position.x, n.position.y + 0.25f, n.position.z);
            yield return new WaitForSeconds(1);
        }
    }

    bool onPlayerMove() {
        return
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
    }


    void updatePath() {
        SpacialPartition.Node3D start = new SpacialPartition.Node3D(transform.position, partition);
        SpacialPartition.Node3D goal = new SpacialPartition.Node3D(target.position, partition);

        partition.addStart(start);
        partition.addEnd(goal);

        path = aStar.Search(start, goal, partition.getNodes());

        StartCoroutine(test());
    }
}
