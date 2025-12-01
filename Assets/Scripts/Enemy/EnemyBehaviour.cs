using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum EnemyTipe {
    straightWebEnemy
}

public class EnemyBehaviour : MonoBehaviour {
    [Header("Referencias")]
    //Referencia al prefab de la red que lanzarán los enemigos.
    public GameObject webPrefab;
    //Referencia al player.
    public Follower follower;
    //
    public Transform teleportationPointContainer;

    PlayerDetection pD;

    [Header("Tipo de enemigo")]
    //Referencia al enumerador que define el tipo de enemigo.
    public EnemyTipe eT;

    [Header("Parámetros para el straightWebEnemy")]
    //Fuerza con la que se lanzará la red.
    public float straightWebThrowingForce;
    //Veces que lanzará la red antes de teletransportarse.
    public int timesThatEnemyThrowsStraightWeb;
    //Delay entre ataque y ataque.
    public float straigtWebDelay;

    [Header("Teletransportación")]
    public bool isTeleporting;
    public float teleportingSpeed;
    public float tpPointOffset;
    public float tpWait;
    public Transform tPPoint;
    Vector2 newLocationPos;
    Vector2 vampirePos;

    Quaternion rotation;

    Animator anim;
    AudioSource aS;
    AudioList aL;

    private void Start() {
        //
        SetReferences();
        //
        //StartCoroutine(AttackPattern()); //[REVISAR]
    }

    private void Update() {
        LookAtPlayer();
    }

    private void FixedUpdate() {

        if (isTeleporting) {
            Teleportation();
        }

    }

    void SetReferences() {
        //Referencia al Script follower de la Main Camera.

        anim = GetComponentInChildren<Animator>();
        aS = GetComponentInChildren<AudioSource>();
        aL = GetComponentInChildren<AudioList>();
        pD = GetComponentInChildren<PlayerDetection>();
    }

    void LookAtPlayer() {

        if (follower.targets[follower.target].position.x < transform.position.x) {
            rotation.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = rotation;
        } else if (follower.targets[follower.target].position.x > transform.position.x) {
            rotation.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = rotation;
        }
    }

    IEnumerator ThrowStraightWeb(Transform tT) {
        //
        int r = 1;

        for (int i = 0; i < r; i++) {
            //
            yield return new WaitForSeconds(straigtWebDelay);

            anim.SetTrigger("Attack");
            aS.PlayOneShot(aL.audioClips[0]);
            //Instanciamos la red y la guardamos.
            GameObject wP = Instantiate(webPrefab, gameObject.transform);

            wP.transform.SetParent(null);

            wP.transform.right = tT.position - transform.position;

            wP.GetComponent<Rigidbody2D>().AddForce(wP.transform.right * straightWebThrowingForce);
            //
            yield return new WaitForSeconds(straigtWebDelay);
            //
            if (pD.fG) {
                if (pD.fG.trapped) {
                    StopCoroutine(AttackPattern(tT));
                }
            }
        }
    }

    public IEnumerator AttackPattern(Transform tT) {

        switch (eT) {
            //
            case EnemyTipe.straightWebEnemy:

                yield return ThrowStraightWeb(tT);
                anim.SetTrigger("Teleportation");

                aS.PlayOneShot(aL.audioClips[1]);

                yield return new WaitForSeconds(0.8f);
                newLocationPos = FindTeleportationPoint();
                vampirePos = transform.position;

                isTeleporting = true;
                //

                GetComponent<Collider2D>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                //
                Debug.Log("Vampiro teletransportan");
                break;
        }
    }

    void Teleportation() {

        //Lerpeo del vampiro y del punto
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)newLocationPos, teleportingSpeed);
        //

        //
        if (Vector2.Distance(transform.position, newLocationPos) < tpPointOffset) {
            anim.SetTrigger("FinalDestination");
            tPPoint.position = vampirePos;
            StartCoroutine(waitForTp());
        }

    }

    Vector2 FindTeleportationPoint() {
        //Indice aleatorio
        int r = Random.Range(0, teleportationPointContainer.childCount);

        tPPoint = teleportationPointContainer.GetChild(r);

        //
        return teleportationPointContainer.GetChild(r).position;

    }

    IEnumerator waitForTp() {
        yield return new WaitForSeconds(tpWait);
        isTeleporting = false;
        GetComponent<Collider2D>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
