using UnityEngine;
using System.Collections;

public class nivel : MonoBehaviour {

    public GameObject dotPrefab;
    public GameObject pacman;
    private ArrayList dots;
    private Vector2[] ghosts;
    private int nDots;
    private int[,] maze = { 
        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
        {-1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, 0, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1 },
        {-1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1 },
        {-1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, 1, -1, -1, -1, 1, -1, -1 },
        {-1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1 },
        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
    };
    private int[,] mazeCopy;
    // Use this for initialization
    void Start () {
        instantiateMaze();
    }
    public void instantiateMaze() {
        nDots = 0;
        mazeCopy = (int[,])maze.Clone();
        GameObject dot;
        dots = new ArrayList();
        for (int i = 0; i < 30; i++)
            for (int j = 0; j < 33; j++)
            {
                {
                    if (mazeCopy[i, j] == 1)
                    {
                        nDots++;
                        dot = Instantiate(dotPrefab) as GameObject;
                        dot.transform.parent = this.transform;
                        dot.transform.localPosition = new Vector2(i, j);
                        dots.Add(dot);
                    }
                }
            }
        ghosts = new Vector2[4];
        setGhostsInitialPosition();
    }
    public void resetMaze()
    {
        removeAllDots();
        instantiateMaze();
    
    }

    public void setGhostsInitialPosition() {
        GetComponent<enableGhostMove>().ghosts[0].GetComponent<RandomGhostMove>().getGhostPosition();
        GetComponent<enableGhostMove>().ghosts[1].GetComponent<RandomGhostMove>().getGhostPosition();
        GetComponent<enableGhostMove>().ghosts[2].GetComponent<RandomGhostMove>().getGhostPosition();
        GetComponent<enableGhostMove>().ghosts[3].GetComponent<RandomGhostMove>().getGhostPosition();
    }

    public void setGhosPosition(Vector2 pos, int idGhost) {
        ghosts[idGhost] = pos;
    }

    public Vector2 getClosestPill(Vector2 pacman) {
        Vector2 closest = Vector2.zero;
        float closestDist = 999999;
        for (int i = 2; i < 30-2; i++)
            for (int j = 2; j < 33-2; j++)
            {
                {
                    if (mazeCopy[i, j] == 1 && isGhost(i, j) == false)
                    {
                        Vector2 pillPosition = new Vector2(i, j);
                        float currentDist = (pacman - pillPosition).magnitude;
                        if (closestDist > (pacman - pillPosition).magnitude)
                        {
                            closestDist = currentDist;
                            closest = pillPosition;
                        }
                    }
                }
            }
        return closest;
    }
    public void removeAllDots() {
        for (int i = 0; i < dots.Count; i++)
        {
            Destroy(dots[i] as GameObject);
        }
    }
    public int[] getVecinos(Vector2 position) {
        int[] vecinos = new int[4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (((position.y + 2) == ghosts[j].y && position.x == ghosts[j].x) ||
                    ((position.y - 2) == ghosts[j].y && position.x == ghosts[j].x) ||
                    (position.y == ghosts[j].y && position.x + 2 == ghosts[j].x) ||
                    (position.y == ghosts[j].y && (position.x - 2) == ghosts[j].x))
                {
                    Debug.Log("funciona");
                    vecinos[i] = -1;
                    break;
                }
                else
                {
                    if(i == 0)
                        vecinos[i] = mazeCopy[(int)position.x, (int)position.y + 1];//arriba
                    else if(i == 1)
                        vecinos[i] = mazeCopy[(int)position.x + 1, (int)position.y];//derecha
                    else if (i == 2)
                        vecinos[i] = mazeCopy[(int)position.x, (int)position.y - 1];//abajo
                    else if(i == 3)
                        vecinos[i] = mazeCopy[(int)position.x - 1, (int)position.y];//izquierda
                }
            }
        }
        
        return vecinos;
    }
    public int[] getVecinosGhosts(Vector2 position)
    {
        int[] vecinos = new int[4];
        vecinos[0] = mazeCopy[(int)position.x, (int)position.y + 1];//arriba
        vecinos[1] = mazeCopy[(int)position.x + 1, (int)position.y];//derecha
        vecinos[2] = mazeCopy[(int)position.x, (int)position.y - 1];//abajo
        vecinos[3] = mazeCopy[(int)position.x - 1, (int)position.y];//izquierda
        return vecinos;
    }
    public bool isGhost(int x, int y) {
        bool isGhost = false;
        for (int i = 0; i < ghosts.Length; i++){
            if(ghosts[i].x == x && ghosts[i].y == y)
            {
                isGhost = true;
                break;
            }
        }
        return isGhost;
    }
    public ArrayList getAviableDirections(int posX, int posY, int opositeDirection)
    {
        ArrayList directionsAviable = new ArrayList();
        if (mazeCopy[posX, posY + 1] != -1 && opositeDirection != 0) { directionsAviable.Add(Vector2.up); }  // UP
        if (mazeCopy[posX + 1, posY] != -1 && opositeDirection != 1) { directionsAviable.Add(Vector2.right); }  // RIGHT
        if (mazeCopy[posX, posY - 1] != -1 && opositeDirection != 2) { directionsAviable.Add(Vector2.down); }  // DOWN
        if (mazeCopy[posX - 1, posY] != -1 && opositeDirection != 3) { directionsAviable.Add(Vector2.left); }  // LEFT
        return directionsAviable;
    }
    public ArrayList getAviableDirectionsGhost(int[] vecinos, int opositeDirection)
    {
        ArrayList directionsAviable = new ArrayList();
        if (vecinos[0] != -1 && opositeDirection != 0) { directionsAviable.Add(Vector2.up); }  // UP
        if (vecinos[1] != -1 && opositeDirection != 1) { directionsAviable.Add(Vector2.right); }  // RIGHT
        if (vecinos[2] != -1 && opositeDirection != 2) { directionsAviable.Add(Vector2.down); }  // DOWN
        if (vecinos[3] != -1 && opositeDirection != 3) { directionsAviable.Add(Vector2.left); }  // LEFT
        return directionsAviable;
    }
    public ArrayList getNeighbours(int posX, int posY)
    {
        ArrayList Neighbours = new ArrayList();
        if (mazeCopy[posX, posY + 1] != -1 && isGhost(posX, posY + 1) == false) { Neighbours.Add(new Vector3(posX, posY + 1, 0)); }  // UP
        if (mazeCopy[posX + 1, posY] != -1 && isGhost(posX + 1, posY) == false) { Neighbours.Add(new Vector3(posX + 1, posY, 0)); }  // RIGHT
        if (mazeCopy[posX, posY - 1] != -1 && isGhost(posX, posY - 1) == false) { Neighbours.Add(new Vector3(posX, posY - 1, 0)); }  // DOWN
        if (mazeCopy[posX - 1, posY] != -1 && isGhost(posX - 1, posY) == false) { Neighbours.Add(new Vector3(posX - 1, posY, 0)); }  // LEFT
        return Neighbours;
    }

    public void eliminarPastilla(int x, int y) {
        nDots--;
        mazeCopy[x, y] = 0;
        if(nDots == 0)
        {
            pacman.GetComponent<pacmanLogic>().win();
            GetComponent<enableGhostMove>().enabled = false;
            GetComponent<enableGhostMove>().disableGhostMove();
        }
    }

    public bool hayPastilla(int x, int y) {
        if (mazeCopy[x, y] == 1)
            return true;
        else
            return false;
    }
}
