using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeTime = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }


    private IEnumerator FadeOut(string sceneName)
    {
        Color curColor = _fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1);
        float time = 0;

        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(curColor, targetColor, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        _fadeImage.color = targetColor;
        SceneManager.LoadScene(sceneName);
        StartCoroutine(FadeIn());
    }


    private IEnumerator FadeIn()
    {
        Color curColor = _fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 0);
        float time = 0;

        while (time < _fadeTime)
        {
            _fadeImage.color = Color.Lerp(curColor, targetColor, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        _fadeImage.color = targetColor;
        ActivateScene();
        yield break;
    }

    private void ActivateScene()
    {
        if (SceneManager.GetActiveScene().name == SceneName.GAME_SCENE)
        {
            GameSceneManager.Instance.GameSceneEvent.CallGameStart();
        }
        else if (SceneManager.GetActiveScene().name == SceneName.TITLE_SCENE)
        {

        }
    }
}
