using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarlicAttack : MonoBehaviour {
    //public float radius;
    public LayerMask interactableLayers;
    public Vector3 offset;
    public Vector2 size;
    PlayerComponents pC;
    public float cooldown, currentCD;
    public Image cdBar;

    [SerializeField] public Collider2D[ ] c;

    private void Start() {
        currentCD = cooldown;
        pC = GetComponent<PlayerComponents>();
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && currentCD >= cooldown) {
            Explosion();
        }

        if (currentCD < cooldown) {
            currentCD += Time.deltaTime;
            cdBar.fillAmount =  currentCD / 1 / 2;
        } else if (currentCD >= cooldown) {
            cdBar.gameObject.SetActive(false);
        }
    }

    void Explosion() {
        //rH = Physics2D.CircleCastAll(transform.position + offset, radius, Vector2.up, 1f, interactableLayers);

        c = Physics2D.OverlapBoxAll(transform.position + offset, size, 0f,interactableLayers);

        pC.animator.SetTrigger("Attack");

        pC.aS.PlayOneShot(pC.aL.audioClips[0]);

        for (int i = 0; i < c.Length; i++) {
            if (c[i].transform.TryGetComponent(out Damageable d)) {
                d.CheckMask();
            } else {
                Debug.Log("Puto bobo pon el Damageable al enemigo");
            }
        }
        currentCD = 0;
        cdBar.fillAmount = 0;
        cdBar.gameObject.SetActive(true);
    }

    private void OnDrawGizmos() {
        //Gizmos.DrawWireSphere(transform.position + offset, radius);
        Gizmos.DrawWireCube(transform.position + offset, size);
    }


}
