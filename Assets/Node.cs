using UnityEngine;
using System.Collections;
using System;
public class Node : IComparable
{
    public int nodeTotalCost;
    public Node parent;
    public Vector3 position;

    public Node()
    {
        this.nodeTotalCost = 0;
        this.parent = null;
    }

    public Node(Vector3 pos)
    {
        this.nodeTotalCost = 0;
        this.parent = null;
        this.position = pos;
    }

    public override bool Equals(object o)
    {
        Node temp = (Node)o;
        return ((this.position.x == temp.position.x) && (this.position.y == temp.position.y));
    }

    public override int GetHashCode()
    {
        return this.GetHashCode();
    }





    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        //Negative value means object comes before this in the sort
        //order.
        if (this.position.z < node.position.z)
            return -1;
        //Positive value means object comes after this in the sort
        //order.
        if (this.position.z > node.position.z) return 1;
        return 0;
    }
}