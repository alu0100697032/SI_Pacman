
using UnityEngine;
using System.Collections;
using Behave.Runtime;
using Tree = Behave.Runtime.Tree;
using System;

public class AgentController : MonoBehaviour, IAgent
{
    public GameObject maze;
    public float speed = 10;
    int[] vecinos = new int[4];
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    private ArrayList openList;
    private ArrayList closedList;
    private int stepSecuence;
    private ArrayList currentPath;
    private bool existsPath;
    private Vector2 aStarDest;

    
  
    Tree m_Tree;

    IEnumerator Start()
    {
        stepSecuence = 1;
        existsPath = false;
        aStarDest = Vector2.zero;
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = maze.GetComponent<nivel>().getVecinos(position);
        direction = Vector2.zero;
        m_Tree = BLNewBehaveLibrary0.InstantiateTree(BLNewBehaveLibrary0.TreeType.NewCollection1_NewTree1, this);

        while (Application.isPlaying && m_Tree != null)
        {
            yield return new WaitForSeconds(1.0f / m_Tree.Frequency);
            AIUpdate();
        }
    }

    void AIUpdate()
    {
        m_Tree.Tick();
    }

    public BehaveResult Tick(Tree sender, bool init)
    {
        return BehaveResult.Success;
    }

    public void Reset(Tree sender)
    {

    }

    public BehaveResult TickCheckEmailAction(Tree sender)
    {
        Debug.Log("Checking email");
        return BehaveResult.Success;
    }

    public BehaveResult TickListenMusicAction(Tree sender)
    {
        Debug.Log("While listening music!");
        return BehaveResult.Failure;
    }

    private bool isHungry = true;
    private bool isSleepy = true;

    public int SelectTopPriority(Tree sender, params int[] IDs)
    {
        if (isHungry)
        {
            isHungry = false;
            isSleepy = true;
            return IDs[0]; //eat
        }
        else if (isSleepy)
        {
            isSleepy = false;
            return IDs[1]; //sleep
        }
        else
        {
            isHungry = true;
            return IDs[2]; //play
        }
    }

    private int alpha = 0;
    private int gameLoading = 0;

    public BehaveResult TickFadeInAction(Tree sender)
    {
        if (gameLoading >= 100)
        {
            return BehaveResult.Failure;
        }

        alpha++;
        Debug.Log("FadeIn ticked! Alpha:" + alpha.ToString());
        if (alpha < 255)
        {
            return BehaveResult.Running;
        }
        else
        {
            alpha = 255;
            return BehaveResult.Success;
        }
    }

    public BehaveResult TickFadeOutAction(Tree sender)
    {
        alpha--;
        Debug.Log("FadeOut ticked! Alpha:" + alpha.ToString());
        if (alpha > 0)
        {
            return BehaveResult.Running;
        }
        else
        {
            alpha = 0;
            return BehaveResult.Success;
        }
    }

    public BehaveResult TickGotoGameAction(Tree sender)
    {
        gameLoading++;
        Debug.Log("GotoGame ticked! Game loading: " + gameLoading.ToString());
        if (gameLoading < 100)
        {
            return BehaveResult.Running;
        }
        else
        {
            return BehaveResult.Success;
        }
    }

    private bool shouldDo = true;

    public BehaveResult TickArribaAction(Tree sender)
    {
        if (vecinos[0] == 1)
        {
            float step = speed * Time.deltaTime;
            Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
            transform.localPosition = dest2;

            if ((Vector2)transform.localPosition == dest)
            {
                position = dest;
                if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
                {
                    maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                    GetComponent<pacmanLogic>().scoreUp(10);
                }
                vecinos = maze.GetComponent<nivel>().getVecinos(position);
                if (vecinos[0] == 1)
                    dest = position + Vector2.up;
            }
            // Anima al pacman
            Vector2 dir = dest - (Vector2)transform.localPosition;
            GetComponent<Animator>().SetFloat("DirX", dir.x);


        
    GetComponent<Animator>().SetFloat("DirY", dir.y);
            return BehaveResult.Running;
        }
        else
        {
            return BehaveResult.Failure;
        }
    }

    public BehaveResult TickDerechaAction(Tree sender)
{
    if (vecinos[1] == 1)
    {
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;

        if ((Vector2)transform.localPosition == dest)
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = maze.GetComponent<nivel>().getVecinos(position);
            if (vecinos[1] == 1)
                dest = position + Vector2.right;
        }
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        return BehaveResult.Running;
    }
    else
    {
        return BehaveResult.Failure;
    }
}

public BehaveResult TickAbajoAction(Tree sender)
{
    if (vecinos[2] == 1)
    {
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;

        if ((Vector2)transform.localPosition == dest)
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = maze.GetComponent<nivel>().getVecinos(position);
            if (vecinos[2] == 1)
                dest = position + Vector2.down;
        }
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        return BehaveResult.Running;
    }
    else
    {
        return BehaveResult.Failure;
    }
}

    public BehaveResult TickRandomAction(Tree sender)
    {
        Node a = new Node(position);
        Node b = new Node(maze.GetComponent<nivel>().getClosestPill(position));
        currentPath = FindPath(a, b);
        if (currentPath.Count > 0)
        {
            Node lastNode = (Node)currentPath[currentPath.Count - 1];
            aStarDest = (Vector2)lastNode.position;
            Node nod = (Node)currentPath[1];
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
            if (stepSecuence < currentPath.Count - 1)
                stepSecuence++;
            if (aStarDest == position)
            {
                stepSecuence = 1;
                existsPath = false;
            }
        }
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        return BehaveResult.Running;

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
        list.Reverse()
            ;
        return list;
    }

    public BehaveResult TickIzquierdaAction(Tree sender)
{
    if (vecinos[3] == 1)
    {
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;

        if ((Vector2)transform.localPosition == dest)
        {
            position = dest;
            if (maze.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                maze.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = maze.GetComponent<nivel>().getVecinos(position);
            if (vecinos[3] == 1)
                dest = position + Vector2.left;
        }
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        return BehaveResult.Running;
    }
    else
    {
        return BehaveResult.Failure;
    }
}
public BehaveResult TickShouldDoMyActionDecorator(Tree sender)
{
    shouldDo = !shouldDo;
    if (shouldDo)
    {
        Debug.Log("Should Do!");
        return BehaveResult.Success;
    }
    else
    {
        Debug.Log("Shouldn't Do!");


       
return BehaveResult.Failure;
        }
    }

    private int distWithEnemy = 200;
private int enemyHealth = 100;

public BehaveResult TickPatrolAction(Tree sender)
{
    if (distWithEnemy > 100)
    {
        distWithEnemy -= 10;
        Debug.Log("Enemy is getting closers! " + distWithEnemy.ToString());
        return BehaveResult.Running;
    }
    else
    {
        Debug.Log("Enemy spotted!");
        return BehaveResult.Failure;
    }
}

public BehaveResult TickAttackAction(Tree sender)
{
    enemyHealth -= 5;
    Debug.Log("Attacking enemy! enemy health: " + enemyHealth.ToString());
    if (enemyHealth < 10)
    {
        Debug.Log("Enemy's dead!");
        return BehaveResult.Failure;
    }
    else
    {
        return BehaveResult.Running;
    }
}

public BehaveResult TickIdleAction(Tree sender)
{
    distWithEnemy = 200;
    enemyHealth = 100;
    Debug.Log("Idling for a while!");
    return BehaveResult.Success;
}
}