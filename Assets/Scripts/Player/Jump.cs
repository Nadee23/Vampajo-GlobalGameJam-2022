using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    [Header("Jump")]
    public float jumpForce;
    public int extraJumps, currentJumps;
    public float timeElapsed, coyoteTime;
    public bool garlicJumped;

    [Header("Raycast")]
    public float maxDistance;
    public LayerMask interactableLayers;
    public Vector2 rightOffset;
    public Vector2 leftOffset;

    PlayerComponents pC;

    private void Awake() {
        pC = GetComponent<PlayerComponents>();
    }

    private void Update() {
        CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && (CheckIfGrounded() || (timeElapsed < coyoteTime && !garlicJumped))) {
            PlayerJump();
            pC.pM.speed = pC.pM.airSpeed;
        }

        if (pC.rB.velocity.y == 0) {
            pC.animator.SetBool("Grounded", true);
            pC.pM.speed = pC.pM.groundSpeed;
        }
    }

    void PlayerJump() {

        if (extraJumps > 0) {
            if (currentJumps < extraJumps) {
                pC.animator.SetBool("Grounded", false);
                pC.animator.SetTrigger("DobleJump");
                pC.rB.velocity = new Vector2(pC.rB.velocity.x, 0f);
                pC.rB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                currentJumps++;
            }
        } else {
            pC.animator.SetBool("Grounded", false);
            if (currentJumps <= extraJumps) {
                pC.rB.velocity = new Vector2(pC.rB.velocity.x, 0f);
                pC.rB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                currentJumps++;
            }
            garlicJumped = true;
        }
    }

    bool CheckIfGrounded() {
        //bool middleRay = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, interactableLayers);
        bool rightRay = Physics2D.Raycast((Vector2)transform.position + rightOffset, Vector2.down, maxDistance, interactableLayers);
        bool leftRay = Physics2D.Raycast((Vector2)transform.position + leftOffset, Vector2.down, maxDistance, interactableLayers);

        if (/*middleRay ||*/ rightRay || leftRay) {

            if (timeElapsed != 0) {
                timeElapsed = 0;
                garlicJumped = false;
            }
            currentJumps = 0;

            return true;
        } else {

            if (currentJumps == extraJumps) {
                timeElapsed += Time.deltaTime;
            }
            return false;
        }

    }

    //private void OnDrawGizmos() {
    //    if (CheckIfGrounded()) {
    //        //Debug.DrawRay(transform.position, Vector2.down * maxDistance, Color.green);
    //        Debug.DrawRay((Vector2)transform.position + rightOffset, Vector2.down * maxDistance, Color.green);
    //        Debug.DrawRay((Vector2)transform.position + leftOffset, Vector2.down * maxDistance, Color.green);
    //    } else {
    //        //Debug.DrawRay(transform.position, Vector2.down * maxDistance, Color.red);
    //        Debug.DrawRay((Vector2)transform.position + rightOffset, Vector2.down * maxDistance, Color.red);
    //        Debug.DrawRay((Vector2)transform.position + leftOffset, Vector2.down * maxDistance, Color.red);
    //    }
    //}
}
