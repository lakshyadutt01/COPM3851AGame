using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatforms : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    public GameObject Player;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position; //assigning the coordinates for destination later on
    }

    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        // checks where the platform currently is located and then assigns it its next location

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        // then tells it to move to the next destination
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    // Draws a line so I can edit the and see the path clearly
}
