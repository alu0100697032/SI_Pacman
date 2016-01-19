using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pacmanLogic : MonoBehaviour {

    private int lives;
    private int score;
    public GameObject scoreText;
	// Use this for initialization
	void Start () {
        lives = 3;
        score = 0;
	}

    public void scoreUp(int pointScored) {
        score += pointScored;
        scoreText.GetComponent<Text>().text = "" + score;
    }

    public int getScore() {
        return score;
    }
    public int getLives() {
        return lives;
    }
}
