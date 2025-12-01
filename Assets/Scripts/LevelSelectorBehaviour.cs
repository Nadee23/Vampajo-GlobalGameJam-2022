using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorBehaviour : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject levelSelector;

    public void ShowLevelSelector() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(false);
        }
        levelSelector.SetActive(true);
    }

    public void HideLevelSelector() {
        levelSelector.SetActive(false);
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(true);
        }
    }
}
