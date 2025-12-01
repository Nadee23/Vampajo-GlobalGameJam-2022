using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBehaviour : MonoBehaviour
{

    public CanReceiveEnemyDamage player;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Floor")) {
            //Se destruye la red.
            Destroy(gameObject);
        }

        if (collision.CompareTag("Garlic")) {
            //[REVISAR]
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, collision.GetComponent<Rigidbody2D>().velocity.y);
            //Quitamos el movimiento.
            collision.GetComponent<PlayerMovement>().enabled = false;
            //Desactivamos el salto.
            collision.GetComponent<Jump>().enabled = false;
            //
            collision.GetComponent<GarlicAttack>().enabled = false;

            collision.GetComponentInChildren<Animator>().enabled = false;

            collision.GetComponent<FreeGarlic>().trapped = true;
            //Mostrar bocadillo.
            collision.GetComponent<FreeGarlic>().VisualFeedbackWhenTrapped(true);

            collision.GetComponentInChildren<SpriteRenderer>().sprite = collision.GetComponent<PlayerComponents>().trapped;
            //Se destruye la red.
            Destroy(gameObject);
        }

        if (collision.CompareTag("Vampire")) {
            //[REVISAR]
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, collision.GetComponent<Rigidbody2D>().velocity.y);
            //Desactivamos el movimiento.
            collision.GetComponent<PlayerMovement>().enabled = false;
            //Desactivamos el salto.
            collision.GetComponent<Jump>().enabled = false;
            //
            collision.GetComponent<Interaction>().enabled = false;

            collision.GetComponentInChildren<Animator>().enabled = false;

            //
            collision.GetComponentInChildren<SpriteRenderer>().sprite = collision.GetComponent<PlayerComponents>().trapped;
            //Hace daño.
            collision.GetComponent<Damageable>().CheckMask();
            //Se destruye la red.
            Destroy(gameObject);
        }
    }
}
