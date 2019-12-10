using System.Collections.Generic;
using UnityEngine;

//abstract node class for comparing any dataType
public abstract class Node {
    protected abstract bool greaterThan(Node Node2);
    protected abstract bool lessThan(Node Node2);

    public abstract List<Node> getNeighbours();

    public abstract float getDistance(Node n);

    public static bool operator >(Node n, Node n2) {
        return n.greaterThan(n2);
    }

    public static bool operator <(Node n, Node n2) {
        return n.lessThan(n2);
    }
}


public class AStar {
    List<Node> visited = new List<Node>();
    List<Node> unvisited = new List<Node>();

    Dictionary<Node, Node> predecessorDict = new Dictionary<Node, Node>();
    Dictionary<Node, float> fCostDict = new Dictionary<Node, float>();
    Dictionary<Node, float> gCostDict = new Dictionary<Node, float>();

    public List<Node> Search(Node start, Node goal, Node[] allNodes) {
        foreach(Node node in allNodes) {
            fCostDict[node] = float.MaxValue;
            gCostDict[node] = float.MaxValue;
        }
        fCostDict[start] = 0;
        gCostDict[start] = 0;

        visited.Clear();
        unvisited = new List<Node>(allNodes);

        predecessorDict.Clear();

        while(unvisited.Count > 0) {
            Node u = getClosestFromUnvisited();

            if(u == goal) {
                break;
            }

            visited.Add(u);


            MonoBehaviour.print(u);

            foreach(Node v in u.getNeighbours()) {
                if(visited.Contains(v)) {
                    continue;
                }

                // 6. If new shortest path found then set new value of shortest path                
                // To do: update fDDict[v], gDDict[v] and predDict[v]
                if(fCostDict[v] > gCostDict[u] + u.getDistance(v) + v.getDistance(goal)) {
                    fCostDict[v] = gCostDict[u] + u.getDistance(v) + v.getDistance(goal);
                    gCostDict[v] = gCostDict[u] + u.getDistance(v);
                }
                predecessorDict[v] = u;
            }
        }

        List<Node> path = new List<Node>();

        // Generate the shortest path
        path.Add(goal);
        Node p = predecessorDict[goal];

        while(p != start) {
            path.Add(p);
            p = predecessorDict[p];
        }

        path.Reverse();
        return path;
    }

    Node getClosestFromUnvisited() {
        float shortest = float.MaxValue;
        Node shortestNode = null;
        foreach(var node in unvisited) {
            if(shortest > fCostDict[node]) {
                shortest = fCostDict[node];
                shortestNode = node;
            }
        }

        unvisited.Remove(shortestNode);
        return shortestNode;
    }
}
