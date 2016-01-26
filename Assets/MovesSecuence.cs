using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovesSecuence : MonoBehaviour {

    public GameObject mazeMS;
    public GameObject inputField;
    public float speed;
    int stepSecuence;
    int[] vecinos = new int[4];
    int[] vecinosAux;
    string[] secuence;
    private Vector2 dest = Vector2.zero;
    private Vector2 position = Vector2.zero;
    
    void Start()
    {
        resetMovesSecuence();
    }
    public void resetMovesSecuence() {
        stepSecuence = 0;
        secuence = inputField.GetComponent<InputField>().text.Split(' ');
        dest = (Vector2)transform.localPosition;
        position = dest;
        vecinos = mazeMS.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
        vecinosAux = vecinos;
    }
    void FixedUpdate()
    {
        //Si se encuentra en un cruce cambia al siguiente movimiento de la secuencia
        if (cruce())
            stepSecuence++;
        if (stepSecuence >= secuence.Length)
            stepSecuence = 0;

        //Fija el destino del pacman
        if (secuence[stepSecuence] == "UP" && vecinos[0] != -1)
        {
            //Debug.Log("Posicion pacman: " + position);
            //Debug.Log("Arriba: " + vecinos[0] + " Derecha: " + vecinos[1] + " Abajo: " + vecinos[2] + " Izquierda: " + vecinos[3]);
            dest = position + Vector2.up;
            vecinosAux[0] = -1;
        }
        else if (secuence[stepSecuence] == "RIGHT" && vecinos[1] != -1)
        {
            //Debug.Log("Arriba: " + vecinos[0] + " Derecha: " + vecinos[1] + " Abajo: " + vecinos[2] + " Izquierda: " + vecinos[3]);
            dest = position + Vector2.right;
            vecinosAux[1] = -1;
        }
        else if (secuence[stepSecuence] == "DOWN" && vecinos[2] != -1)
        {
            dest = position + Vector2.down;
            vecinosAux[2] = -1;
        }
        else if (secuence[stepSecuence] == "LEFT" && vecinos[3] != -1)
        {
            dest = position + Vector2.left;
            vecinosAux[3] = -1;
        }

        //Mueve el pacman teniendo en cuenta la velocidad
        float step = speed * Time.deltaTime;
        Vector2 dest2 = Vector2.MoveTowards(transform.localPosition, dest, step);
        transform.localPosition = dest2;
        //Si ya ha llegado al destino actualiza los vecinos (solo en las posiciones enteras)
        if ((dest2.x == position.x + 1) || (dest2.x == position.x - 1) || (dest2.y == position.y + 1) || (dest2.y == position.y - 1))
        {
            position = dest;
            if (mazeMS.GetComponent<nivel>().hayPastilla((int)position.x, (int)position.y))
            {
                mazeMS.GetComponent<nivel>().eliminarPastilla((int)position.x, (int)position.y);
                GetComponent<pacmanLogic>().scoreUp(10);
            }
            vecinos = mazeMS.GetComponent<nivel>().getVecinos((int)position.x, (int)position.y);
            vecinosAux = vecinos;
        }
        
        // Anima al pacman
        Vector2 dir = dest - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    //Devuelve true si se encuentra en un cruce
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
    //Devuelve true si se encuentra en una esquina
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
