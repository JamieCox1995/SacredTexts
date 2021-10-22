using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private PathRequestManager RequestManager;
    private Grid Grid;

    //public Pathfinding(Vector2Int _WorldSize, Vector3 _Origin)
    //{
    //    Grid = new Grid(_WorldSize.x, _WorldSize.y, 1f, _Origin);
    //}

    public void Initialize()
    {
        Grid = Grid.Instance;
    }
    public void FindPath(Vector3 _StartPosition, Vector3 _EndPosition)
    {
        StartCoroutine(CalculatePath(_StartPosition, _EndPosition));
    }

    private IEnumerator CalculatePath(Vector3 _StartPosition, Vector3 _EndPosition)
    {
        List<Vector3> path = new List<Vector3>();
        bool pathFound = false;

        Node startingNode = Grid.GetCell(_StartPosition);
        Node endNode = Grid.GetCell(_EndPosition);

        // We are going to make sure that the start and end are both reach
        if(startingNode.Walkable && endNode.Walkable)
        {
            // We are going to create a heap for the open list of nodes, and a hash set for closed nodes.
            // this way we can effiecently retrieve the next closest cell and we can ensure no duplicate are added to the closed list
            Heap<Node> openCells = new Heap<Node>(Grid.MaxSize);
            HashSet<Node> closedCells = new HashSet<Node>();

            openCells.Add(startingNode);

            while(openCells.CurrentCount > 0)
            {
                Node currentNode = openCells.RemoveFirst();
                closedCells.Add(currentNode);

                // Checking to see if we have reached the final node.
                if(currentNode == endNode)
                {
                    pathFound = true;
                    break;
                }

                // Getting all of the neighbours to this node.
                foreach(Node node in Grid.GetNeighbours(currentNode))
                {
                    // if the neighbour has been searched before, or if the cell is not walkable we will skip the neighbour
                    if (!node.Walkable || closedCells.Contains(node)) continue;

                    // now we are going to get the movement cost to move from our current node to the neighbour
                    int movementCost = currentNode.GCost + GetDistanceCost(currentNode, node);

                    if (movementCost < node.GCost || !openCells.Contains(node))
                    {
                        node.GCost = movementCost;
                        node.HCost = GetDistanceCost(node, endNode);
                        node.PreviousNode = currentNode;

                        if (!openCells.Contains(node))
                        {
                            openCells.Add(node);
                        }
                        else
                        {
                            openCells.UpdateItem(node);
                        }
                    }
                }
            }
        }

        yield return null;

        if(pathFound)
        {
            path = BuildPath(endNode);
        }

        PathRequestManager.Instance.FinishProcessingPath(path.ToArray(), pathFound);
    }

    private List<Vector3> BuildPath(Node _EndNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = _EndNode;

        while(currentNode.PreviousNode != null)
        {
            path.Add(new Vector3(currentNode.X, 0f, currentNode.Y));
            currentNode = currentNode.PreviousNode;
        }

        path.Reverse();
        return path;

    }

    private int GetDistanceCost(Node _NodeA, Node _NodeB)
    {
        int x = Mathf.Abs(_NodeA.X - _NodeB.X);
        int y = Mathf.Abs(_NodeA.Y - _NodeB.Y);

        int remaining = Mathf.Abs(x - y);

        return WorldConstant.DiagonalMoveCost * Mathf.Min(x, y) + WorldConstant.DefaultMoveCost * remaining;
    }
}
