using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformsTypes {
    Mobile, Appearing
}

public class PlatformsActions : MonoBehaviour {
    public PlatformsTypes platformType;
    public List<Transform> childrenPlatform;

    Animator anim;
    AudioSource aS;

    [Header("Mobile Platforms")]
    public float speed;
    public float minDistance;
    public bool isMoving;
    private int i = 0;
    MobilePlatformData mPD;
    public int index;

    private void Start() {
        Setup();
        aS = GetComponent<AudioSource>();
    }

    void Setup() {
        for (int i = 0; i < transform.childCount; i++) {
            childrenPlatform.Add(transform.GetChild(i));
        }
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (isMoving) {
            MovePlatforms(index);
        }
    }

    public void Actions() {
        anim.SetTrigger("Activated");

        aS.PlayOneShot(aS.clip);

        switch (platformType) {
            case PlatformsTypes.Mobile:
                //Plataformas móviles.
                for (int i = 0; i < transform.childCount; i++) {
                    mPD = childrenPlatform[i].GetComponent<MobilePlatformData>();
                    if (!isMoving) {
                        index = i;
                        isMoving = true;
                    }

                }
                break;
            case PlatformsTypes.Appearing:
                for (int i = 0; i < transform.childCount; i++) {
                    childrenPlatform[i].gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    //IEnumerator MovePlatforms(int index) {
    //    isMoving = true;
    //    while (true) {
    //        if (Vector2.Distance(childrenPlatform[index].transform.position, mPD.positions[i].position) < minDistance) {
    //            i++;
    //            if (i == mPD.positions.Length) {
    //                i = 0;
    //            }
    //        }
    //        childrenPlatform[index].transform.position = Vector2.MoveTowards(childrenPlatform[index].transform.position, mPD.positions[i].position, speed * Time.deltaTime);
    //        yield return null;
    //    }
    //}

    void MovePlatforms(int index) {

        if (Vector2.Distance(childrenPlatform[index].transform.position, mPD.positions[i].position) < minDistance) {
            i++;
            if (i == mPD.positions.Length) {
                i = 0;
            }
        }
        childrenPlatform[index].transform.position = Vector2.MoveTowards(childrenPlatform[index].transform.position, mPD.positions[i].position, speed * Time.deltaTime);
    }

}
