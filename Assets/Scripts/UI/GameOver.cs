using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : BaseBehaviour
{
    [SerializeField] private GameObject _newHighScore;
    [SerializeField] private TextMeshProUGUI _newScoreText;
    [SerializeField] private GameObject _playerHighScore;
    [SerializeField] private TextMeshProUGUI _playerHighScoreText;
    [SerializeField] private TMP_InputField _idInputField;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _exitBtn;
    private bool _isActive;
    protected override void Awake()
    {
        base.Awake();
        _restartBtn.onClick.AddListener(RestartScene);
        _homeBtn.onClick.AddListener(GoToHome);
        _exitBtn.onClick.AddListener(Application.Quit);
        _applyButton.onClick.AddListener(ApplyScore);
        GetComponent<Canvas>().enabled = false;
    }

    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver += GameOverEvent;
    }

    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver -= GameOverEvent;
    }
    protected override void Initialize()
    {
        base.Initialize();
        _isActive = false;
        _playerHighScore.SetActive(false);
        _newHighScore.SetActive(false);

    }

    private void RestartScene()
    {
        if (!_isActive)
            FadeManager.Instance.ChangeScene(SceneName.GAME_SCENE);
    }

    private void GoToHome()
    {
        if (!_isActive)
            FadeManager.Instance.ChangeScene(SceneName.TITLE_SCENE);
    }

    private void ApplyScore()
    {
        PlayerPrefs.SetInt("BestScore", GameSceneManager.Instance.Score);
        PlayerPrefs.SetString("Nickname", _idInputField.text);
        ScoreManager.Instance.UpdateScore();
        ServerManager.Instance.SubmitScore(_idInputField.text, GameSceneManager.Instance.Score);
        _applyButton.gameObject.SetActive(false);
    }

    private void GameOverEvent(GameSceneEvents gameSceneEvents)
    {
        GetComponent<Canvas>().enabled = true;
        if (GameSceneManager.Instance.Score > ScoreManager.Instance.Score)
        {
            _newScoreText.text = GameSceneManager.Instance.Score.ToString();
            _newHighScore.SetActive(true);
        }
        else
        {
            _playerHighScoreText.text = ScoreManager.Instance.Score.ToString();
            _playerHighScore.SetActive(true);
        }
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _newHighScore = FindGameObjectInChildren("NewHighScore");
        _playerHighScore = FindGameObjectInChildren("PlayerHighScore");
        _newScoreText = FindGameObjectInChildren<TextMeshProUGUI>("NewScoreText");
        _playerHighScoreText = FindGameObjectInChildren<TextMeshProUGUI>("PlayerHighScoreText");
        _idInputField = FindGameObjectInChildren<TMP_InputField>("IDInput");
        _applyButton = FindGameObjectInChildren<Button>("ApplyButton");

        _restartBtn = FindGameObjectInChildren<Button>("RestartButton");
        _homeBtn = FindGameObjectInChildren<Button>("HomeButton");
        _exitBtn = FindGameObjectInChildren<Button>("ExitButton");
    }
#endif
}
