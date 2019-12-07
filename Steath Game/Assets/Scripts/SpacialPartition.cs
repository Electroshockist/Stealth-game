using System.Collections.Generic;
using UnityEngine;



public class SpacialPartition : MonoBehaviour {

    public class Node3D : Node {
        public Vector3 position;

        public Node3D(Vector3 position) {
            this.position = position;
        }

        public override float getDistance(Node n) {
            Node3D node = (Node3D)n;
            return Vector3.Distance(position, node.position);
        }

        public override List<Node> getNeighbours() {
            return SpacialPartition.getNeighbours(this);
        }

        protected override bool greaterThan(Node Node2) {
            return getDistance(Node2) > 0;
        }

        protected override bool lessThan(Node Node2) {
            return getDistance(Node2) < 0;
        }
    }

    public static float mapSize;
    public static int divisions;
    public Vector3 position;
    public static List<List<List<Node3D>>> nodes;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void setup() {
        float divisionSize =  mapSize / divisions;
        for(int i = 0; i < divisions; i++) {
			List<List<Node3D>> face = new List<List<Node3D>>();
            for(int j = 0; j < divisions; j++) {
				List<Node3D> colomn = new List<Node3D>();
                for(int k = 0; k < divisions; k++) {
                    Vector3 pos = new Vector3(
                        i * divisionSize + position.x,
                        j * divisionSize + position.y,
                        k * divisionSize + position.z
                        );
                    colomn.Add(new Node3D(pos));
                }
                face.Add(colomn);
            }
            nodes.Add(face);
        }
    }

    public static List<Node> getNeighbours(Node3D node) {
		float divisionSize =  mapSize / divisions;
        NeighbourHelper setNeigbours = (n) => {
            List<Node> neighbours = new List<Node>();

            Vector3 temp = new Vector3(n.position.x + divisionSize, n.position.y, n.position.z);
            neighbours.Add(getNodeByPosition(temp));

            temp = new Vector3(n.position.x - divisionSize, n.position.y, n.position.z);
            neighbours.Add(getNodeByPosition(temp));

            temp = new Vector3(n.position.x, n.position.y + divisionSize, n.position.z);
            neighbours.Add(getNodeByPosition(temp));

            temp = new Vector3(n.position.x, n.position.y - divisionSize, n.position.z);
            neighbours.Add(getNodeByPosition(temp));

            temp = new Vector3(n.position.x, n.position.y, n.position.z + divisionSize);
            neighbours.Add(getNodeByPosition(temp));

            temp = new Vector3(n.position.x, n.position.y, n.position.z - divisionSize);
            neighbours.Add(getNodeByPosition(temp));

            return neighbours;
        };

        for(int i = 0; i < divisions; i++) {
            for(int j = 0; j < divisions; j++) {
                for(int k = 0; k < divisions; k++) {
                    if(nodes[i][j][k] == node) {
                        return setNeigbours(node);
                    }
                }
            }
        }
        return null;
    }

    private static Node3D getNodeByPosition(Vector3 position) {
        for(int i = 0; i < divisions; i++) {
            for(int j = 0; j < divisions; j++) {
                for(int k = 0; k < divisions; k++) {
                    if(nodes[i][j][k].position == position) {
                        return nodes[i][j][k];
                    }
                }
            }
        }
        return null;
    }

    delegate List<Node> NeighbourHelper(Node3D n);
}
