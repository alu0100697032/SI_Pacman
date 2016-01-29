using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour {

    public Transform[] waypoints;
    int cur = 0;

    public float speed = 0.3f;

    void FixedUpdate(){
        // Waypoint not reached yet? then move closer
        if (transform.localPosition != waypoints[cur].localPosition)
        {
            float step = speed * Time.deltaTime;
            Vector2 p = Vector2.MoveTowards(transform.localPosition,
                                            waypoints[cur].localPosition,
                                            step);

            transform.localPosition = p;
        }// Waypoint reached, select next one
        else
        {
            cur = (cur + 1) % waypoints.Length;
        }
        // Animation
        Vector2 dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    void OnTriggerEnter2D(Collider2D co){
        if (co.name == "pacman")
            Destroy(co.gameObject);
    }
}
