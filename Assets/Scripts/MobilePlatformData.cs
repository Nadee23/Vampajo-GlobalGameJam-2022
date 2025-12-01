using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatformData : MonoBehaviour
{
    public Transform[ ] positions;
    PlatformsActions pA;

    private void Start() {
        pA = GetComponentInParent<PlatformsActions>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (pA.platformType == PlatformsTypes.Mobile) {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (pA.platformType == PlatformsTypes.Mobile) {
            collision.transform.SetParent(null);
        }
    }
}
