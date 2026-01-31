using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public float loseTime = 10.0f;

    public float ninnaValue = 0;
    public float victoryNinnaValue = 6.66f;
    public Slider ninnaBar;

    public PlayerController playerController;
    public PecoraMinigame pecoraMinigame;

    private void Start() {
        pecoraMinigame.onPecoraExit += OnPecoraPoint;
    }

    void Update() {
        if (!playerController.mask.isMaskDown) {
            if (ninnaValue > 0) {
                ninnaValue -= Time.deltaTime;
            }
        }

        if (ninnaBar != null) {
            ninnaBar.value = ninnaValue / victoryNinnaValue;
        }

    }

    public void OnPecoraPoint() {
        if (ninnaValue < victoryNinnaValue) {
            //ninnaValue += Time.deltaTime;
            ninnaValue += 0.5f;
        }
        else {
            SceneManager.LoadScene("YouWin");
        }
    }

    public void LoseGame() {
        SceneManager.LoadScene("YouLose");
    }
}
