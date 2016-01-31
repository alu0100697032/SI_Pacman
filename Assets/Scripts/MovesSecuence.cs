using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovesSecuence : MonoBehaviour {

    public GameObject mazeMS;
    public GameObject inputField;
    public float speed;
    int stepSecuence;
    int[] vecinos = new int[4];
    int[] vecinosCruce;
    int[] vecinosEsquina;
    string[] secuence;
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;
    
    void Start()
    {
        resetMovesSecuence();
    }
    public void resetMovesSecuence() {
        stepSecuence = 0;
        readImputField();
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = mazeMS.GetComponent<nivel>().getVecinos(position);
        vecinosCruce = (int[])vecinos.Clone();
        vecinosEsquina = (int[])vecinos.Clone();
    }
    void readImputField() {
        secuence = inputField.GetComponent<InputField>().text.Split(' ');
    }
    void FixedUpdate()
    {
        //Mueve el pacman teniendo en cuenta la velocidad
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;
        if ((Vector2)transform.localPosition == dest)
        {
            position = dest;
            if (mazeMS.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                mazeMS.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = mazeMS.GetComponent<nivel>().getVecinos(position);
            vecinosCruce = (int[])vecinos.Clone();
            vecinosEsquina = (int[])vecinos.Clone();

            //Si se encuentra en un cruce cambia al siguiente movimiento de la secuencia
            if (cruce() || esquina())
            {
                stepSecuence++;
            }
            if (stepSecuence >= secuence.Length)
                stepSecuence = 0;

            //Fija el destino del pacman
            if (secuence[stepSecuence] == "UP" && vecinos[0] != -1)
            {
                dest = position + Vector2.up;
                vecinosCruce[0] = -1;
            }
            else if (secuence[stepSecuence] == "RIGHT" && vecinos[1] != -1)
            {
                dest = position + Vector2.right;
                vecinosCruce[1] = -1;
            }
            else if (secuence[stepSecuence] == "DOWN" && vecinos[2] != -1)
            {
                dest = position + Vector2.down;
                vecinosCruce[2] = -1;
            }
            else if (secuence[stepSecuence] == "LEFT" && vecinos[3] != -1)
            {
                dest = position + Vector2.left;
                vecinosCruce[3] = -1;
            }
            else
            {
                stepSecuence++;
            }
        }

        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    //Devuelve true si se encuentra en un cruce
    public bool cruce() {
        int aux = 0;
        for (int i = 0; i < vecinosCruce.Length; i++) {
            if (vecinosCruce[i] != -1)
                aux++;
        }
        if (aux > 2)
            return true;
        else
            return false;
    }
    //Devuelve true si se encuentra en una esquina
    public bool esquina() {
        if (vecinosEsquina[0] == -1 && vecinosEsquina[1] == -1)
        {
            vecinosEsquina[0] = 0;
            return true;
        }
        else if (vecinosEsquina[0] == -1 && vecinosEsquina[3] == -1)
        {
            vecinosEsquina[0] = 0;
            return true;
        }
        else if (vecinosEsquina[2] == -1 && vecinosEsquina[3] == -1)
        {
            vecinosEsquina[2] = 0;
            return true;
        }
        else if (vecinosEsquina[2] == -1 && vecinosEsquina[1] == -1)
        {
            vecinosEsquina[2] = 0;
            return true;
        }
        else
            return false;
    }
}
