using UnityEngine;
using System.Collections;

public class eatPacman : MonoBehaviour {

    public GameObject maze;

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacmanMS")
        {
            co.gameObject.GetComponent<pacmanLogic>().pacmanToInitalPosition();
            co.gameObject.GetComponent<MovesSecuence>().resetMovesSecuence();
            co.gameObject.GetComponent<pacmanLogic>().livesDown();
            maze.GetComponent<enableGhostMove>().ghostToInitialPosition();
        }else if (co.name == "pacmanPA")
        {
            co.gameObject.GetComponent<pacmanLogic>().pacmanToInitalPosition();
            co.gameObject.GetComponent<PATableMove>().resetPAMove();
            co.gameObject.GetComponent<pacmanLogic>().livesDown();
            maze.GetComponent<enableGhostMove>().ghostToInitialPosition();
        }
        else if (co.name == "pacmanAS")
        {
            co.gameObject.GetComponent<pacmanLogic>().pacmanToInitalPosition();
            co.gameObject.GetComponent<AStarMove>().resetAStar();
            co.gameObject.GetComponent<pacmanLogic>().livesDown();
            maze.GetComponent<enableGhostMove>().ghostToInitialPosition();
        }
        else if (co.name == "pacmanNN")
        {
            co.gameObject.GetComponent<pacmanLogic>().pacmanToInitalPosition();
            co.gameObject.GetComponent<pacmanLogic>().livesDown();
            maze.GetComponent<enableGhostMove>().ghostToInitialPosition();
        }
    }
}
