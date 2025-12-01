using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Follower : MonoBehaviour {
    public Transform[] targets;
    public int target;
    public float lerpDuration;
    public Vector3 offset;

    public Sprite[] facesSprites;
    public Image imageHUD;

    private void Update() {
        SetTarget();
    }

    void SetTarget() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (target == targets.Length - 1) {
                //0 = vampiro
                if (targets[target].TryGetComponent(out FreeGarlic fG)) {
                    if (fG.trapped) {
                        target = 0;
                        GetComponent<CinemachineVirtualCamera>().Follow = targets[target];
                        EnableOrDisableComponents(target);
                    } else {
                        EnableOrDisableComponents(target);
                        target = 0;
                        GetComponent<CinemachineVirtualCamera>().Follow = targets[target];
                        EnableOrDisableComponents(target);
                    }
                }
            } else {
                if (targets[1].GetComponent<FreeGarlic>().trapped) {
                    return;
                }
                //1 = ajo
                EnableOrDisableComponents(target);
                target = 1;
                GetComponent<CinemachineVirtualCamera>().Follow = targets[target];
                EnableOrDisableComponents(target);
            }
            imageHUD.sprite = facesSprites[target];
        }
    }

    void EnableOrDisableComponents(int target) {
        PlayerComponents pC = targets[target].GetComponent<PlayerComponents>();

        pC.jump.enabled = !pC.jump.enabled;
        pC.rB.velocity = new Vector2(0f, pC.rB.velocity.y);
        pC.pM.enabled = !pC.pM.enabled;
        if (pC.attack) {
            pC.attack.enabled = !pC.attack.enabled;
        } else {
            pC.interact.enabled = !pC.interact.enabled;
        }
    }
}
