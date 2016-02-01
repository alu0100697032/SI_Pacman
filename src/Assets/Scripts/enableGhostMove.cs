using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class enableGhostMove : MonoBehaviour {

    public GameObject[] ghosts;
    public GameObject score;
    private bool[] restarGhostMove = new bool[4];
    private int initialScore;

    void start() {
        resetGhostMove();
    }
    public void resetGhostMove() {
        for (int i = 0; i < 4; i++)
        {
            restarGhostMove[i] = false;
        }
        initialScore = Int32.Parse(score.GetComponent<Text>().text);
    }

    public void disableGhostMove()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<RandomGhostMove>().enabled = false;
        }
    }

    public void restartMove()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            if (restarGhostMove[i] == true)
                ghosts[i].GetComponent<RandomGhostMove>().enabled = true;
        }
    }

    public void resetAllGhost()
    {
        disableGhostMove();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<RandomGhostMove>().resetGhost();
        }

    }

    public void ghostToInitialPosition()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<RandomGhostMove>().resetGhost();
        }
        disableGhostMove();
        resetGhostMove();
    }
    // Update is called once per frame
    void Update () {
        if (Int32.Parse(score.GetComponent<Text>().text) >= initialScore + 20)
        {
            ghosts[0].GetComponent<RandomGhostMove>().enabled = true;
            restarGhostMove[0] = true;
        }
        if (Int32.Parse(score.GetComponent<Text>().text) >= initialScore + 40)
        {
            ghosts[1].GetComponent<RandomGhostMove>().enabled = true;
            restarGhostMove[1] = true;
        }
        if (Int32.Parse(score.GetComponent<Text>().text) == initialScore + 60)
        {
            ghosts[2].GetComponent<RandomGhostMove>().enabled = true;
            restarGhostMove[2] = true;
        }
        if (Int32.Parse(score.GetComponent<Text>().text) == initialScore + 80)
        {
            ghosts[3].GetComponent<RandomGhostMove>().enabled = true;
            restarGhostMove[3] = true;
        }

    }
}
