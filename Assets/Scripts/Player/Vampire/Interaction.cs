using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {
    BoxCollider2D bC;
    public float timeToWait;
    public bool interacting;
    PlayerComponents pC;

    private void Start() {
        bC = transform.GetChild(0).GetComponent<BoxCollider2D>();
        pC = GetComponent<PlayerComponents>();
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && !interacting) {
            StartCoroutine(Interact());
        }
    }

    IEnumerator Interact() {
        pC.animator.SetTrigger("Interact");
        pC.aS.PlayOneShot(pC.aL.audioClips[1]);
        yield return new WaitForSeconds(timeToWait);
        bC.enabled = true;
        interacting = true;
        yield return new WaitForSeconds(timeToWait);
        bC.enabled = false;
        interacting = false;
    }


}
