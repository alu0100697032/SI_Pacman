using UnityEngine;
using System.Collections;

public class PATableMove : MonoBehaviour {

    public GameObject maze;
    public float speed;
    int[] vecinos = new int[4];
    string[][] PATable;
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;
    private Vector2 direction = Vector2.zero;

    void Start()
    {
        resetPAMove();
    }
    public void resetPAMove() {
        PATable = GetComponent<PATableConfigReader>().getPATable();
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = maze.GetComponent<nivel>().getVecinos(position);
        direction = Vector2.zero;
    }

    void FixedUpdate()
    {
        //Mueve el pacman teniendo en cuenta la velocidad
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;
        vecinos = maze.GetComponent<nivel>().getVecinos(position);//borrar si hay mal comportamiento

        if ((Vector2)transform.localPosition == dest)
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = maze.GetComponent<nivel>().getVecinos(position);

            if (existNearPills())
            {
                direction = Vector2.zero;
                bool stop = false;
                for (int i = 0; i < PATable.Length; i++)
                {
                    if (stop == true)
                        break;
                    for (int j = 0; j < PATable[i].Length; j++)
                    {
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
                        else
                        {
                            if ((vecinos[j] == -1 || vecinos[j] == 0) && PATable[i][j] != "WALL")
                            {
                                break;
                            }
                            else if (vecinos[j] == 1 && PATable[i][j] != "PILL")
                            {
                                break;
                            }
                        }

                    }
                }
            }
            else
            {
                randomMove();
            }
        }
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    public bool existNearPills() {
        bool nearPills = false;
        for (int i = 0; i < vecinos.Length; i++) {
            if (vecinos[i] == 1)
            {
                nearPills = true;
                break;
            }
        }
        return nearPills;
    }

    public void randomMove() {
        if (cruce() || esquina() || direction == Vector2.zero)
        {
            ArrayList directions = maze.GetComponent<nivel>().getAviableDirectionsGhost(
                vecinos, getOpositeDirection(direction));
            System.Random random = new System.Random();
            if (directions.Count > 0)
            {
                int randomNumber = random.Next(0, directions.Count);
                direction = (Vector2)directions[randomNumber];
            }
            else {//borrar si hay mal comportamiento
                direction = Vector2.zero;
            }
        }
        dest = position + direction;
    }

    //Devuelve true si se encuentra en un cruce
    public bool cruce()
    {
        int aux = 0;
        for (int i = 0; i < vecinos.Length; i++)
        {
            if (vecinos[i] != -1)
                aux++;
        }
        if (aux > 2)
            return true;
        else
            return false;
    }
    //Devuelve true si se encuentra en una esquina
    public bool esquina()
    {
        if (vecinos[0] == -1 && vecinos[1] == -1)
            return true;
        else if (vecinos[0] == -1 && vecinos[3] == -1)
            return true;
        else if (vecinos[2] == -1 && vecinos[3] == -1)
            return true;
        else if (vecinos[2] == -1 && vecinos[1] == -1)
            return true;
        else
            return false;
    }
    public int getOpositeDirection(Vector2 currentDirection)
    {
        if (currentDirection == Vector2.up)
            return 2;
        else if (currentDirection == Vector2.right)
            return 3;
        else if (currentDirection == Vector2.down)
            return 0;
        else if (currentDirection == Vector2.left)
            return 1;
        else
            return -1;
    }
}
