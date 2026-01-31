using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MiaoMenu : MonoBehaviour
{

    public Image fadeImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayClick()
    {
        LoadScene("GameScene");
    }

    public void LoadScene(string sceneName)
    {
        if (_isLoading)
            return;

        StartCoroutine(LoadScene_CR(sceneName));
    }

    private bool _isLoading = false;
    public IEnumerator LoadScene_CR(string sceneName)
    {
        _isLoading = true;

        fadeImage.DOFade(1, 1);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
