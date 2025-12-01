using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            if (collision.TryGetComponent(out Damageable d)) {
                d.enemyHasMask = false;
                d.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
        } else if (collision.TryGetComponent(out PlatformsActions pT)) {
            pT.Actions();
        
        }

        Debug.Log(collision.tag);

        if (collision.CompareTag("Garlic") && collision.GetComponent<FreeGarlic>().trapped) {
            collision.GetComponent<FreeGarlic>().VisualFeedbackWhenTrapped(false);
            collision.GetComponentInChildren<SpriteRenderer>().sprite = collision.GetComponent<FreeGarlic>().normalSprite;
            collision.GetComponentInChildren<Animator>().enabled = true;
            collision.GetComponent<FreeGarlic>().trapped = false;
        }

    }
}
