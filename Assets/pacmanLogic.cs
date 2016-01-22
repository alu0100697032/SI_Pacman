using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pacmanLogic : MonoBehaviour {

    private int lives;
    private int score;
    private Vector2 pacmanInitialPosition;
    public GameObject scoreText;
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
        transform.localPosition = pacmanInitialPosition;
        scoreUp(0);
    }
    public void scoreUp(int pointScored) {
        score += pointScored;
        scoreText.GetComponent<Text>().text = "" + score;
    }

    public void livesDown() {
        if(lives > 0)
            lives--;
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
