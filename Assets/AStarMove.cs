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
        ArrayList List = FindPath(a, b);
        Debug.Log("==================:" );

        for (int i = 0; i < List.Count; i++)
        {
            Node nod = (Node)List[i];
            Debug.Log("NODE: (" + nod.position.x + ", " + nod.position.y + ", " + nod.position.z + ", " + nod.parent + ") <");

        }

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
        closedList = new ArrayList();

        Node node = null;
        while (openList.Count != 0)
        {
            Debug.Log("OpenList no vacía");
            
            node = (Node)openList[0];
            Debug.Log("NODE: (" + node.position.x + ", " + node.position.y + ", " + node .position.z + ", " + node.parent + ") <");


            //Check if the current node is the goal node
            if (node.position.x == goal.position.x && node.position.y == goal.position.y)
            {
                Debug.Log("Final, calculamos RUTA obtenida");
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

                    Debug.Log("NEIGHTBOURNODE: (" + neighbourNode.position.x + ", " + neighbourNode.position.y + ", " + neighbourNode.position.z + ", " + neighbourNode.parent.position.x + ") <");

                    if (!openList.Contains(neighbourNode))
                    {
                        //Debug.Log("NODE: (" + node.x + ", " + node.y + ", " + node.z + ")");
                       
                      
                        openList.Add(neighbourNode);
                        openList.Sort();

                        Debug.Log("Añadimos a Openlist");
                        Node tempd = (Node)openList[0];
                        Debug.Log("OPENLIST: (" + tempd.position.x + ", " + tempd.position.y + ", " + tempd.position.z + ", " + tempd.parent + ") <");

                        //Node tempdA = (Node)openList[openList.Count - 1];
                        //Debug.Log("OPENLIST: (" + tempdA.position.x + ", " + tempdA.position.y + ", " + tempdA.position.z + ", " + tempdA.parent + ") <");

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
        Debug.Log("RUTAAA1");
        return CalculatePath(node);
    }

    private static ArrayList CalculatePath(Node node)
    {
        Debug.Log("RUTAAA2");
        ArrayList list = new ArrayList();
        Debug.Log("RUTAAA2" + node);
        while (node != null)
        {
            Debug.Log("entro");
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }

 

  
}

