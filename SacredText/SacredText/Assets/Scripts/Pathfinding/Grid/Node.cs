using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public int X;
    public int Y;
    public Vector3 WorldPosition;

    public bool Walkable;
    public int MovementPenalty;

    public int GCost;
    public int HCost;

    public Node PreviousNode;
    private int heapIndex;

    public Node(int _X, int _Y, Vector3 _WorldPosition, bool _Walkable, int _Penalty)
    {
        X = _X;
        Y = _Y;
        WorldPosition = _WorldPosition;
        Walkable = _Walkable;
        MovementPenalty = _Penalty;
    }

    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }

    public int HeapIndex
    {
        get { return heapIndex; }
        set { heapIndex = value; }
    }

    public int CompareTo(Node _Comparitor)
    {
        int compare = FCost.CompareTo(_Comparitor.FCost);

        if (compare == 0) compare = HCost.CompareTo(_Comparitor.HCost);

        return -compare;
    }
}
