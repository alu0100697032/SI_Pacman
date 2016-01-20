using UnityEngine;
using System.Collections;

public class AStarMove : MonoBehaviour {

    public GameObject maze;
    public float speed;
    int[] vecinos = new int[4];
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;
    private ArrayList openList;
    private ArrayList closedList;

    void Start()
    {
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
    }


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

    public void aStar(Vector2 start, Vector2 goal)
    {
        closedList = new ArrayList();     	  // The set of nodes already evaluated.
        openList = new ArrayList();// The set of tentative nodes to be evaluated, initially containing the start node
        openList.Add(start);

        while (openList.Capacity != 0) {

        }
    }

    public int manhattan()
    {
        return 0;
    }
}
