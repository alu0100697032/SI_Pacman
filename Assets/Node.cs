using UnityEngine;
using System.Collections;
using System;
public class Node : MonoBehaviour, IComparable
{
    public int nodeTotalCost;
    //public float estimatedCost;
    //public bool bObstacle;
    public Node parent;
    public Vector3 position;

    public Node()
    {
        //this.estimatedCost = 0.0f;
        this.nodeTotalCost = 0;
        //this.bObstacle = false;
        this.parent = null;
    }

    public Node(Vector3 pos)
    {
        //this.estimatedCost = 0.0f;
        this.nodeTotalCost = 0;
        //this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }



    /*public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }*/
    public override bool Equals(object o)
    {
        Node temp = (Node)o;
        if (this.position.x == temp.position.x && this.position.y == temp.position.y)
            return true;
        else
            return false;
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