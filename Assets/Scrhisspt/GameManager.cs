using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake() {
        if (instance != null) {
            instance = this;
        }
    }


    public float ninnaValue = 0;
    public float victoryNinnaValue = 6.66f;
    public Slider ninnaBar;

    public PlayerController playerController;

    void Update() {
        if (playerController.mask.isMaskDown) {
            if (ninnaValue < victoryNinnaValue) {
                ninnaValue += Time.deltaTime;
            }
            else {
                SceneManager.LoadScene("YouWin");
            }
        }
        else {
            if (ninnaValue > 0) {
                ninnaValue -= Time.deltaTime;
            }
        }

        if (ninnaBar != null) {
            ninnaBar.value = ninnaValue / victoryNinnaValue;
        }

    }
}
