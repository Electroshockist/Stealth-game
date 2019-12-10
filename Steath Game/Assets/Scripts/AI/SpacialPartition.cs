using System.Collections.Generic;
using UnityEngine;



public class SpacialPartition : MonoBehaviour {

    public class Node3D : Node {
        public Vector3 position;
        private SpacialPartition partition;

        public Node3D(Vector3 position, SpacialPartition spacialPartition) {
            this.position = position;
            partition = spacialPartition;
        }

        public override float getDistance(Node node) {
            Node3D n = (Node3D)node;
            return Vector3.Distance(position, n.position);
        }

        public override List<Node> getNeighbours() {
            return partition.getNeighbours(this);
        }

        protected override bool greaterThan(Node Node2) {
            return getDistance(Node2) > 0;
        }

        protected override bool lessThan(Node Node2) {
            return getDistance(Node2) < 0;
        }
    }

    public float mapSize;
    public int divisions;
    public Vector3 position;
    private List<List<List<Node3D>>> nodes = new List<List<List<Node3D>>>();

    private Node3D start, goal;

    private bool isBuilt = false;

    public void setup() {
        float divisionSize = mapSize / divisions;
        Vector3 pos = new Vector3(0, -1, 0);
        //loop throughall 3 lists
        for(int i = 0; i < divisions; i++) {
            List<List<Node3D>> face = new List<List<Node3D>>();
            for(int j = 0; j < divisions; j++) {
                List<Node3D> colomn = new List<Node3D>();
                for(int k = 0; k < divisions; k++) {
                    //each node has a position = the current loop times the size of each division + its position - halfof the map size(so that it is centered
                    pos = new Vector3(
                       i * divisionSize + position.x - (mapSize / 2),
                       j * divisionSize + position.y - (mapSize / 2),
                       k * divisionSize + position.z - (mapSize / 2)
                       );
                    if(pos.y > 0) {
                        colomn.Add(new Node3D(pos, this));
                    }
                }
                if(pos.y > 0) {
                    face.Add(colomn);
                }
            }
            if(pos.y > 0) {
                nodes.Add(face);
            }
        }
        isBuilt = true;

    }

    public List<Node> getNeighbours(Node3D node) {
        if(node == goal || node == start) {
            return getSpecialNeighbours(node);
        }
        for(int i = 0; i < nodes.Count; i++) {
            for(int j = 0; j < nodes[i].Count; j++) {
                for(int k = 0; k < nodes[i][j].Count; k++) {
                    print("iteration: " + i + ", " + j + ", " + k + " size: " + nodes[i][j].Count);
                    if(node == nodes[i][j][k]) {
                        List<Node> neighbours = new List<Node>();
                        Node3D neighbour = null;

                        neighbour = getNode(i + 1, j, k);
                        if(neighbour != null) {
                            neighbours.Add(neighbour);
                        }

                        neighbour = getNode(i, j + 1, k);
                        if(neighbour != null) {
                            neighbours.Add(neighbour);
                        }

                        neighbour = getNode(i, j, k + 1);
                        if(neighbour != null) {
                            neighbours.Add(neighbour);
                        }

                        neighbour = getNode(i - 1, j, k);
                        if(neighbour != null) {
                            neighbours.Add(neighbour);
                        }

                        neighbour = getNode(i, j - 1, k);
                        if(neighbour != null) {
                            neighbours.Add(neighbour);
                        }

                        neighbour = getNode(i, j, k - 1);
                        if(neighbour != null) {
                            neighbours.Add(neighbour);
                        }
                        return neighbours;
                    }
                }
            }
        }
        print("Node " + node.position + " does not exist in grid");
        return null;
    }

    public Node3D getNodeByPosition(Vector3 position) {
        return getNearestNode(position);
    }

    public void addStart(Node3D node) {
        start = node;
    }

    public void addEnd(Node3D node) {
        goal = node;
    }

    public Node[] getNodes() {
        List<Node> allNodes = new List<Node>();
        for(int i = 0; i < nodes.Count; i++) {
            for(int j = 0; j < nodes[i].Count; j++) {
                allNodes.AddRange(nodes[i][j]);
            }
        }
        allNodes.Add(goal);
        allNodes.Add(start);
        return allNodes.ToArray();
    }

    public Node3D getNearestNode(Vector3 vec3) {
        float nearest = float.MaxValue;
        Node3D nearestNode = null;
        foreach(Node3D node in getNodes()) {
            float distance = Vector3.Distance(vec3, node.position);
            if(nearest > distance) {
                nearest = distance;
                nearestNode = node;
            }
        }
        return nearestNode;
    }

    private Node3D getNode(int i, int j, int k) {
        if(i < 0 || j < 0 || k < 0 || i >= nodes.Count || j >= nodes[i].Count || k >= nodes[i][j].Count) {
            return null;
        }
        return nodes[i][j][k];
    }

    private List<Node> getSpecialNeighbours(Node3D node) {
        float divisionSize = mapSize / divisions;
        List<Node> neighbours = new List<Node>();
        Node3D neighbour = null;

        Vector3 temp = new Vector3(node.position.x + divisionSize, node.position.y, node.position.z);
        neighbour = getNodeByPosition(temp);

        if(checkNodeValidity(neighbour, node)) {
            neighbours.Add(neighbour);
        }

        temp = new Vector3(node.position.x - divisionSize, node.position.y, node.position.z);
        neighbour = getNodeByPosition(temp);

        if(checkNodeValidity(neighbour, node)) {
            neighbours.Add(neighbour);
        }

        temp = new Vector3(node.position.x, node.position.y + divisionSize, node.position.z);
        neighbour = getNodeByPosition(temp);

        if(checkNodeValidity(neighbour, node)) {
            neighbours.Add(neighbour);
        }

        temp = new Vector3(node.position.x, node.position.y - divisionSize, node.position.z);
        neighbour = getNodeByPosition(temp);

        if(checkNodeValidity(neighbour, node)) {
            neighbours.Add(neighbour);
        }

        temp = new Vector3(node.position.x, node.position.y, node.position.z + divisionSize);
        neighbour = getNodeByPosition(temp);

        if(checkNodeValidity(neighbour, node)) {
            neighbours.Add(neighbour);
        }

        temp = new Vector3(node.position.x, node.position.y, node.position.z - divisionSize);
        neighbour = getNodeByPosition(temp);

        if(checkNodeValidity(neighbour, node)) {
            neighbours.Add(neighbour);
        }

        return neighbours;
    }

    private bool checkNodeValidity(Node3D node, Node original) {
        return node != null && node != original;
    }

    //uncomment if you want to see the generated nodes(extremely laggy)
    //void OnDrawGizmosSelected() {
    //    if(isBuilt) {
    //        // Draw a yellow sphere at the transform's position
    //        Gizmos.color = Color.yellow;
    //        for(int i = 0; i < nodes.Count; i++) {
    //            for(int j = 0; j < nodes[i].Count; j++) {
    //                for(int k = 0; k < nodes[i][j].Count; k++) {
    //                    if(nodes[i][j][k] != null) {
    //                        Gizmos.DrawSphere(nodes[i][j][k].position, 0.01f);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}

