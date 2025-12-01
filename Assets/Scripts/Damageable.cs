using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {
    public float hp;
    public bool enemyHasMask;

    public void CheckMask() {
        if (!enemyHasMask) {
            ReceiveDamage();
        } else {
            Debug.Log("El enemigo tiene mascara de gas");
        }
    }

    void ReceiveDamage() {
        if (--hp <= 0) {
            if (transform.CompareTag("Vampire") || transform.CompareTag("Garlic")) {
                //[REVISAR] poner animaciones, menú de game over.
                Debug.Log("Has perdido");

                StartCoroutine(DeathStop());


            } else {
                gameObject.SetActive(false);
                UIManager.instance.EnemyKilled();
            }
        }
    }

    public IEnumerator DeathStop() {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);

        GameManager.instance.GameOver();
    }

}
