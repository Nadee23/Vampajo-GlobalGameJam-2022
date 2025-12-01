using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanReceiveEnemyDamage : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collider) {
        if (!collider.collider.CompareTag("Enemy")) {
            return;
        }


        if (TryGetComponent(out PlayerComponents playerComponents)) {
            PlayerComponents pC = playerComponents;
            if (collider.collider.CompareTag("Enemy") && !pC.interact || !pC.attack) {
                if (TryGetComponent(out Damageable d)) {
                    d.CheckMask();
                }
            }
        }
    }
}
