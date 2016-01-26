using UnityEngine;
using System.Collections;
using System;

public class AStarMove : MonoBehaviour
{

    public GameObject maze;
    public float speed;
    int[] vecinos = new int[4];
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;

    private ArrayList openList;
    private ArrayList closedList;

    

    // Start
    void Start()
    {
        Debug.Log("Start");
        dest = (Vector2)transform.localPosition;
        position = dest;
        //vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        Node a = new Node(position);
        Node b = new Node(new Vector3(2, 2, 0));
        FindPath(a , b);
    }


    // Update
    void FixedUpdate()
    {


        //Mueve el pacman teniendo en cuenta la velocidad
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;
        //Si ya ha llegado al destino actualiza los vecinos (solo en las posiciones enteras)
        if ((dest2.x == position.x + 1) || (dest2.x == position.x - 1) || (dest2.y == position.y + 1) || (dest2.y == position.y - 1))
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
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
        Debug.Log("Empezando FindPath");
        openList = new ArrayList();
        start.position.z = HeuristicEstimateCost(start, goal) + 0.0f;
        openList.Add(start);

        Debug.Log("COMIENZO: (" + start.position.x + ", " + start.position.y + ", " + start.position.z + ")" );
        Debug.Log("FINAL: (" + goal.position.x + ", " + goal.position.y + ", " + goal.position.z + ")");

        //start.nodeTotalCost = 0.0f;
        //start.estimatedCost = HeuristicEstimateCost(start, goal);
        

        closedList = new ArrayList();
        //Vector3 node = Vector3.zero;
        Node node = null;

        // IMPRIMIR ESTADO
        /*for (int w = 0; w < openList.Count; w++)
        {
            node = (Node)openList[w];
            Debug.Log("(" + node.position.x + ", " + node.position.y + ", " + node.position.z + ")");

        }*/

        while (openList.Count != 0)
        {
            Debug.Log("OpenList no vacía");
            node = (Node)openList[0];

          
                //Check if the current node is the goal node
            if (node.position.x == goal.position.x && node.position.y == goal.position.y)
            {
                Debug.Log("Final, calculamos RUTA obtenida");
                return CalculatePath(node);
            }

            //Create an ArrayList to store the neighboring nodes
            ArrayList neighbours = new ArrayList();
            // GridManager.instance.GetNeighbours(node, neighbours);

            neighbours = maze.GetComponent<nivel>().getNeighbours((int)node.position.x, (int)node.position.y);
           /* Debug.Log("NEIGHBOUR:" + neighbours.Count);*/
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = new Node((Vector3)neighbours[i]);
                /*Debug.Log("NEIGHBOUR0: (" + neighbourNode.position.x + ", " + neighbourNode.position.y + ", " + neighbourNode.position.z + ")");*/

                if (!closedList.Contains(neighbourNode))
                {
                   /* Debug.Log("NEIGHBOUR1: (" + neighbourNode.position.x + ", " + neighbourNode.position.y + ", " + neighbourNode.position.z + ")");*/


                    float cost = HeuristicEstimateCost(neighbourNode, goal);
                    /*Debug.Log("COST: " + cost);*/
                    /*float totalCost = /*node.nodeTotalCost neighbourNode.z +  cost; */

                    //float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);
                    neighbourNode.position.z /*nodeTotalCost*/ = cost;
                    neighbourNode.parent = node;
                    /*neighbourNode.parent = node;*/
                    /*neighbourNode. estimatedCost = totalCost + neighbourNodeEstCost;*/
                    if (!openList.Contains(neighbourNode))
                    {
                        //Debug.Log("NODE: (" + node.x + ", " + node.y + ", " + node.z + ")");
                       
                      
                        openList.Add(neighbourNode);
                        openList.Sort();

                        Debug.Log("Añadimos a Openlist");
                        Node tempd = (Node)openList[0];
                        Debug.Log("OPENLIST: (" + tempd.position.x + ", " + tempd.position.y + ", " + tempd.position.z + ") <");

                        Node tempdA = (Node)openList[openList.Count - 1];
                        Debug.Log("OPENLIST: (" + tempdA.position.x + ", " + tempdA.position.y + ", " + tempdA.position.z + ")");

                    }
                }
            }
            //Push the current node to the closed list

            closedList.Add(node);
            closedList.Sort();
            Node temp = (Node)closedList[0];
            Debug.Log("CLOSEDLIST: (" + temp.position.x + ", " + temp.position.y + ", " + temp.position.z + ")");

            //and remove it from openList
            openList.Remove(node);
        }

        if (node.position.x != goal.position.x && node.position.y != goal.position.y)
        {
            Debug.LogError("Goal Not Found");
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
            //node = node.parent;
        }
        list.Reverse();
        return list;
    }

 

  
}

