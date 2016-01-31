using UnityEngine;
using System.Collections;
using System;

public class AStarMove : MonoBehaviour
{

    public GameObject maze;
    public float speed;
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;

    private ArrayList openList;
    private ArrayList closedList;
    private int stepSecuence;
    private ArrayList currentPath;
    private bool existsPath;
    private Vector2 aStarDest;

    // Start
    void Start()
    {
        resetAStar();
    }
    public void resetAStar() {
        dest = (Vector2)transform.localPosition;
        position = dest;
        stepSecuence = 1;
        existsPath = false;
        aStarDest = Vector2.zero;
    }
    // Update
    void FixedUpdate()
    {
        if (!existsPath) {
            Node a = new Node(position);
            Node b = new Node(maze.GetComponent<nivel>().getClosestPill(position));
            currentPath = FindPath(a, b);
            if (currentPath.Count > 0)
            {
                Node lastNode = (Node)currentPath[currentPath.Count - 1];
                aStarDest = (Vector2)lastNode.position;
                existsPath = true;
            }
        }
        if (existsPath)
        {
            Node nod = (Node)currentPath[stepSecuence];
            dest = (Vector2)nod.position;
        }
        //Mueve el pacman teniendo en cuenta la velocidad
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;
        //Si ya ha llegado al destino actualiza los vecinos (solo en las posiciones enteras)
        if ((Vector2)transform.localPosition == dest)
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            if(stepSecuence < currentPath.Count-1)
                stepSecuence++;
            if (aStarDest == position) {
                stepSecuence = 1;
                existsPath = false;
            }
        }
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }


    // Coste heuristico del aStar
    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        //Vector3 vecCost = curNode.position - goalNode.position;
        //return  //vecCost.magnitude;
        return Math.Abs(curNode.position.x - goalNode.position.x) + Math.Abs(curNode.position.y - goalNode.position.y);
       
    }


    public ArrayList FindPath(Node start, Node goal)
    {
        openList = new ArrayList();
        start.position.z = HeuristicEstimateCost(start, goal) + 0.0f;
        openList.Add(start);
        closedList = new ArrayList();

        Node node = null;
        while (openList.Count != 0)
        {
            node = (Node)openList[0];
            
            //Check if the current node is the goal node
            if (node.position.x == goal.position.x && node.position.y == goal.position.y)
            {
                return CalculatePath(node);
            }

            //Create an ArrayList to store the neighboring nodes
            ArrayList neighbours = new ArrayList();
            neighbours = maze.GetComponent<nivel>().getNeighbours((int)node.position.x, (int)node.position.y);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = new Node((Vector3)neighbours[i]);
            
                if (!closedList.Contains(neighbourNode))
                {
                    neighbourNode.position.z = HeuristicEstimateCost(neighbourNode, goal);
                    neighbourNode.parent = node;
                    
                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                        openList.Sort();
                        
                        //Node tempd = (Node)openList[0];
                    }
                }
            }
            //Push the current node to the closed list
            closedList.Add(node);
            closedList.Sort();
            //Node temp = (Node)closedList[0];
            //and remove it from openList
            openList.Remove(node);
        }

        if (node.position.x != goal.position.x && node.position.y != goal.position.y)
        {
            return null;
        }
        return CalculatePath(node);
    }

    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }
}

