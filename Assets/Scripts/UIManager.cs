using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [Header("Enemies")]
    public int numberOfEnemies;
    public int killedEnemies;
    public Text killed, maxEnemies;
    public Transform enemyContainer;

    [Header("Timer")]
    public float maxTime;
    public float starterTime;
    public Text timer;

    [Header("Score")]
    public Sprite scoreSprite;
    public Image[] scoreRenderers;

    public static UIManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        numberOfEnemies = enemyContainer.childCount;
        maxEnemies.text = numberOfEnemies.ToString();
        starterTime = maxTime;
    }

    private void Update() {
        Timer();
    }

    public void EnemyKilled() {
        killed.text = (++killedEnemies).ToString();
        if (killedEnemies == numberOfEnemies) {
            GameManager.instance.Victory();
        }
    }

    void Timer() {
        if (maxTime > 0f) {
            maxTime -= Time.deltaTime;
        }

        timer.text = ((maxTime / 60) - 1).ToString("00") + " : " + (maxTime % 60).ToString("00");

    }

    public void Score() {
        float result = starterTime / maxTime;

        if (result >= 0.66f) {
            for (int i = 0; i < 3f; i++) {
                scoreRenderers[i].sprite = scoreSprite;
            }
        } else if (result < 0.66f && result >= 0.33f) {
            for (int i = 0; i < 2f; i++) {
                scoreRenderers[i].sprite = scoreSprite;
            }
        } else if (result < 0.33f) {
            for (int i = 0; i < 1f; i++) {
                scoreRenderers[i].sprite = scoreSprite;
            }
        }

    }
}
