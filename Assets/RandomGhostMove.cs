﻿using UnityEngine;
using System.Collections;
using System;


public class RandomGhostMove : MonoBehaviour {

    public float speed = 10f;
    private bool insideHouse;
    public GameObject maze;
    private Vector2 ghostInitialPosition;
    private Vector2 ghostPosition;
    private Vector2 dest = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    private int[] vecinos = new int[4];

    // Use this for initialization
    void Start () {
        ghostInitialPosition = new Vector2(15, 17);
        resetGhost();
	}

    public void resetGhost() {
        ghostPosition = ghostInitialPosition;
        dest = ghostPosition;
        transform.localPosition = ghostPosition;
        insideHouse = true;
        vecinos = maze.GetComponent<nivel>().getVecinos((int)ghostPosition.x, (int)ghostPosition.y);
    }

	// Update is called once per frame
	void FixedUpdate () {

        if (insideHouse)
        {
            float step = speed * Time.deltaTime;
            Vector2 moving = Vector2.MoveTowards(transform.localPosition, dest, step);
            transform.localPosition = moving;

            if ((Vector2)transform.localPosition == new Vector2(15, 20))
            {
                insideHouse = false;
                ghostPosition = dest;
                vecinos = maze.GetComponent<nivel>().getVecinos((int)ghostPosition.x, (int)ghostPosition.y);
                //mover a derecha o a izquierda
                System.Random random = new System.Random();
                int randomNumber = random.Next(0, 2);
                if (randomNumber == 0)
                    direction = Vector2.right;
                else if (randomNumber == 1)
                    direction = Vector2.left;
            }
            else if ((Vector2)transform.localPosition == dest)
            {
                ghostPosition = dest;
                vecinos = maze.GetComponent<nivel>().getVecinos((int)ghostPosition.x, (int)ghostPosition.y);
                dest = ghostPosition + Vector2.up;
            }
        }
        else {
            if ((Vector2)transform.localPosition == dest)
            {
                ghostPosition = dest;
                vecinos = maze.GetComponent<nivel>().getVecinos((int)ghostPosition.x, (int)ghostPosition.y);
                if (cruce() || esquina())
                {
                    ArrayList directions = maze.GetComponent<nivel>().getAviableDirections(
                        (int)ghostPosition.x, (int)ghostPosition.y, getOpositeDirection(direction));
                    System.Random random = new System.Random();
                    int randomNumber = random.Next(0, directions.Count);
                    direction = (Vector2)directions[randomNumber];
                }
                dest = ghostPosition + direction;
            }
            
            float step = speed * Time.deltaTime;
            Vector2 moving = Vector2.MoveTowards(transform.localPosition, dest, step);
            transform.localPosition = moving;
        }
        // Animation
        Vector2 dir = direction - (Vector2)transform.localPosition;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    //Devuelve true si se encuentra en un cruce
    public bool cruce()
    {
        int aux = 0;
        for (int i = 0; i < vecinos.Length; i++)
        {
            if (vecinos[i] != -1)
                aux++;
        }
        if (aux > 2)
            return true;
        else
            return false;
    }
    //Devuelve true si se encuentra en una esquina
    public bool esquina()
    {
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
    public int getOpositeDirection(Vector2 currentDirection)
    {
        if (currentDirection == Vector2.up)
            return 2;
        else if (currentDirection == Vector2.right)
            return 3;
        else if (currentDirection == Vector2.down)
            return 0;
        else
            return 1;
    }
}