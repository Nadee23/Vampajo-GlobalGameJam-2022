using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPlatform : MonoBehaviour
{

    public float speed;
    public float minDistance;
    public Transform[ ] positions;

    public bool isMovingDown;

    private void FixedUpdate() {
        if (!isMovingDown) {
            MoveUp();
        } else {
            MoveDown();
        }
    }

    void MoveDown() {
        //0 = finalPoint
        if (Vector2.Distance(transform.position, positions[1].position) > minDistance) {
            transform.position = Vector2.MoveTowards(transform.position, positions[1].position, speed * Time.deltaTime);
        }
    }

    void MoveUp() {
        //0 = startestPoint
        if (Vector2.Distance(transform.position, positions[0].position) > minDistance) {
            transform.position = Vector2.MoveTowards(transform.position, positions[0].position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Vampire")) {
            isMovingDown = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag("Vampire")) {
            isMovingDown = false;
            collision.transform.SetParent(null);
        }
    }
}
