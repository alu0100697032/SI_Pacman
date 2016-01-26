using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pacmanLogic : MonoBehaviour {

    private int lives;
    private int score;
    private Vector2 pacmanInitialPosition;
    public GameObject scoreText;
    public GameObject gameOver;
    public GameObject[] visualLives;

	// Use this for initialization
	void Start () {
        pacmanInitialPosition = new Vector2(14, 14);
        instantiatePacman();
	}

    public void instantiatePacman() {
        lives = 3;
        score = 0;
    }

    public void resetPacman() {
        instantiatePacman();
        pacmanToInitalPosition();
        scoreUp(0);
        gameOver.SetActive(false);
    }

    public void pacmanToInitalPosition()
    {
        transform.localPosition = pacmanInitialPosition;
    }
    public void scoreUp(int pointScored) {
        score += pointScored;
        scoreText.GetComponent<Text>().text = "" + score;
    }

    public void livesDown() {
        if (lives > 0)
        {
            visualLives[lives - 1].SetActive(false);
            lives--;
        }if(lives == 0)//si no game over
        {
            gameOver.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    public int getScore() {
        return score;
    }

    public int getLives() {
        return lives;
    }

    public Vector2 getPacmanInitialPosition() {
        return pacmanInitialPosition;
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "dot(Clone)")
            Destroy(co.gameObject);
    }
}
