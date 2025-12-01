using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject gameOverMenu, pauseMenu, victoryMenu, levelHUD;
    public bool isPaused;

    AudioSource aS;
    AudioList aL;

    public static GameManager instance;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        aL = GetComponent<AudioList>();
        aS = GetComponentInChildren<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.activeInHierarchy && SceneManager.GetActiveScene().name != "MainMenu") {
            PauseGame();
        }
        if (SceneManager.GetActiveScene().name != "MainMenu" && UIManager.instance.maxTime <= 0) {
            GameOver();
        }
    }

    public void GameOver() {
        if (!gameOverMenu.activeInHierarchy) {
            gameOverMenu.SetActive(true);
            aS.Stop();
            aS.PlayOneShot(aL.audioClips[0]);
            Time.timeScale = 0f;
        }
    }

    public void ResetLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Victory() {
        if (SceneManager.GetActiveScene().name != "Level 6") {
            levelHUD.SetActive(false);
            UIManager.instance.Score();
            victoryMenu.SetActive(true);
            aS.Stop();
            aS.PlayOneShot(aL.audioClips[2]);
            victoryMenu.transform.GetChild(0).Find("Timer").GetComponent<Text>().text = "Time left: " + UIManager.instance.timer.text;
            Time.timeScale = 0f;
        } else {
            levelHUD.transform.parent.Find("Credits").gameObject.SetActive(true);
        }
    }

    public void PauseGame() {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void NextLevel(string nextScene) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
