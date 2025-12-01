using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {
    public bool attacking = false;
    bool checkAttack => !attacking && !GetComponentInParent<EnemyBehaviour>().isTeleporting;
    public FreeGarlic fG;

    private void OnEnable() {
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Garlic")) {
            fG = collision.GetComponent<FreeGarlic>();
            if (checkAttack && !fG.trapped) {
                StartCoroutine(GetComponentInParent<EnemyBehaviour>().AttackPattern(collision.transform));
                attacking = true;
            }
        }

        if (collision.CompareTag("Vampire")) {
            if (checkAttack) {
                StartCoroutine(GetComponentInParent<EnemyBehaviour>().AttackPattern(collision.transform));
                attacking = true;
            }
        }

    }

}
