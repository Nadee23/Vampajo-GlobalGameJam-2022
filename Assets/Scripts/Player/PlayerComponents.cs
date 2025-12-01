using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour {

    [HideInInspector] public Rigidbody2D rB;
    [HideInInspector] public PlayerMovement pM;
    [HideInInspector] public Jump jump;
    [HideInInspector] public Collider2D playerCollider;
    [HideInInspector] public GarlicAttack attack;
    [HideInInspector] public Interaction interact;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource aS;
    [HideInInspector] public AudioList aL;
    public Sprite trapped;

    private void Awake() {
        Setup();
    }

    void Setup() {
        animator = GetComponentInChildren<Animator>();
        rB = GetComponent<Rigidbody2D>();
        pM = GetComponent<PlayerMovement>();
        jump = GetComponent<Jump>();
        playerCollider = GetComponent<Collider2D>();
        aS = GetComponentInChildren<AudioSource>();
        aL = GetComponentInChildren<AudioList>();
        if (TryGetComponent(out GarlicAttack a)) {
            attack = a;
        } else if (TryGetComponent(out Interaction i)) {
            interact = i;
        }
    }
}
