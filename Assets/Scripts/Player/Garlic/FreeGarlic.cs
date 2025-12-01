using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGarlic : MonoBehaviour {
    public Sprite normalSprite;
    public GameObject feedBack;
    public bool trapped;

    public void VisualFeedbackWhenTrapped(bool y) {
        feedBack.SetActive(y);
    }

}
