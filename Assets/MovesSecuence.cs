using UnityEngine;
using System.Collections;

public class MovesSecuence : MonoBehaviour {

    public GameObject maze;
    public float speed;
    int[] vecinos = new int[4];
    int[] vecinosAux;
    Vector2 dest = Vector2.zero;
    Vector2 position = Vector2.zero;
    string[] secuence = {"RIGHT","UP","LEFT", "UP"};
    int stepSecuence = 0;
    // Use this for initialization
    void Start()
    {
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        vecinosAux = vecinos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cruce())
            stepSecuence++;
        if (stepSecuence >= secuence.Length)
            stepSecuence = 0;
        if (secuence[stepSecuence] == "UP" && vecinos[0] == 1)
        {
            //Debug.Log("Posicion pacman: " + position);
            //Debug.Log("Arriba: " + vecinos[0] + " Derecha: " + vecinos[1] + " Abajo: " + vecinos[2] + " Izquierda: " + vecinos[3]);
            dest = position + Vector2.up;
            vecinosAux[0] = -1;
        }
        else if (secuence[stepSecuence] == "RIGHT" && vecinos[1] == 1)
        {
            //Debug.Log("Arriba: " + vecinos[0] + " Derecha: " + vecinos[1] + " Abajo: " + vecinos[2] + " Izquierda: " + vecinos[3]);
            dest = position + Vector2.right;
            vecinosAux[1] = -1;
        }
        else if (secuence[stepSecuence] == "DOWN" && vecinos[2] == 1)
        {
            dest = position - Vector2.up;
            vecinosAux[2] = -1;
        }
        else if (secuence[stepSecuence] == "LEFT" && vecinos[3] == 1)
        {
            dest = position - Vector2.right;
            vecinosAux[3] = -1;
        }
        else {

        }

        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;

        if ((dest2.x == position.x + 1) || (dest2.x == position.x - 1) || (dest2.y == position.y + 1) || (dest2.y == position.y - 1))
        {
            position = dest;
            vecinos = maze.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
            vecinosAux = vecinos;
        }
        
        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    public bool cruce() {
        int aux = 0;
        for (int i = 0; i < vecinosAux.Length; i++) {
            if (vecinosAux[i] != -1)
                aux++;
        }
        if (aux > 2)
            return true;
        else
            return false;
    }

    public bool esquina() {
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
}
