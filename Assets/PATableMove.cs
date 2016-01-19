using UnityEngine;
using System.Collections;

public class PATableMove : MonoBehaviour {

    public GameObject maze;
    public float speed;
    int[] vecinos = new int[4];
    string[][] PATable;
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;

    void Start()
    {
        PATable = GetComponent<PATableConfigReader>().getPATable();
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
    }

    void FixedUpdate()
    {
        bool stop = false;
        for (int i = 0; i < PATable.Length; i++) {
            if (stop == true)
                break;
            for (int j = 0; j < PATable[i].Length; j++) {
                if (j == PATable[i].Length - 1)
                {
                    if (PATable[i][j].Split()[0] == "UP")
                        dest = position + Vector2.up;
                    if (PATable[i][j].Split()[0] == "RIGHT")
                        dest = position + Vector2.right;
                    if (PATable[i][j].Split()[0] == "DOWN")
                        dest = position - Vector2.up;
                    if (PATable[i][j].Split()[0] == "LEFT")
                        dest = position - Vector2.right;
                    stop = true;
                }
                else {
                    if (vecinos[j] == -1)
                    {
                        if (PATable[i][j] != "WALL")
                            break;
                    }
                    else {
                        if (vecinos[j] == 0 && PATable[i][j] != "EMPTY")
                            break;
                        if (vecinos[j] == 1 && PATable[i][j] != "PILL")
                            break;
                    }
                }

            }
        }
       
        //Mueve el pacman teniendo en cuenta la velocidad
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;
        //Si ya ha llegado al destino actualiza los vecinos (solo en las posiciones enteras)
        if ((dest2.x == position.x + 1) || (dest2.x == position.x - 1) || (dest2.y == position.y + 1) || (dest2.y == position.y - 1))
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y)) {
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
}
