using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float groundSpeed, airSpeed;
    Quaternion rotation;
    PlayerComponents pC;

    private void Start() {
       pC = GetComponent<PlayerComponents>();
    }

    private void FixedUpdate() {
        Move();
    }

    void Move() {
        float axis = Input.GetAxisRaw("Horizontal");
        
        if (axis > 0) {
            pC.animator.SetBool("IsWalking",true);
            rotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0f);
            transform.rotation = rotation;
        } else if (axis < 0) {
            pC.animator.SetBool("IsWalking",true);
            rotation.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = rotation;
        }

        if (axis == 0) {
            pC.animator.SetBool("IsWalking", false);
        }

        //[REVISAR] al mantener, ir aumentando poco a poco la velocidad entre dos valores: minimo y maximo.
        pC.rB.velocity = new Vector2(axis *speed * Time.deltaTime, pC.rB.velocity.y);
    }
}
