using UnityEngine;
using System.Collections;

public class simpleMovement : MonoBehaviour {

    public GameObject maze;
    public float speed;
    int[] vecinos = new int[4];
    Vector2 dest = Vector2.zero;
    Vector2 position = Vector2.zero;
    // Use this for initialization
    void Start()
    {
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (vecinos[0] == 1)
        {
            //Debug.Log((Vector2)transform.localPosition + Vector2.up);
            dest = position + Vector2.up;
        }
        if (vecinos[1] == 1)
        {
            //Debug.Log((Vector2)transform.localPosition + Vector2.right);
            dest = position + Vector2.right;
        }
        if (vecinos[2] == 1)
        {
            //Debug.Log((Vector2)transform.localPosition - Vector2.up);
            //dest = (Vector2)transform.localPosition - Vector2.up;
        }
        if (vecinos[3] == 1)
        {
            //Debug.Log((Vector2)transform.localPosition + Vector2.right);
            //dest = (Vector2)transform.localPosition - Vector2.right;
        }
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;

        if (dest2.x == position.x + 1)
        {
            position = dest;
            vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        }
        if (dest2.x == position.x - 1)
        {
            position = dest;
            vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        }
        if (dest2.y == position.y + 1)
        {
            position = dest;
            vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        }
        if (dest2.y == position.y - 1)
        {
            position = dest;
            vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        }
        
        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

}
