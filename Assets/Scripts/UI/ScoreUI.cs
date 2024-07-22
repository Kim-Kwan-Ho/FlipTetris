using TMPro;
using UnityEngine;

public class ScoreUI : BaseBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private bool _updateScore;


    protected override void Initialize()
    {
        base.Initialize();
        _updateScore = false;
    }

    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver += GameOverEvent;
        GameSceneManager.Instance.GameSceneEvent.OnGameStart += GameStartEvent;
    }

    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver -= GameOverEvent;
        GameSceneManager.Instance.GameSceneEvent.OnGameStart -= GameStartEvent;
    }

    private void GameStartEvent(GameSceneEvents gameSceneEvents)
    {
        _updateScore = true;
    }
    private void GameOverEvent(GameSceneEvents gameSceneEvents)
    {
        _updateScore = false;
    }

    private void Update()
    {
        if (!_updateScore)
            return;

        _scoreText.text = GameSceneManager.Instance.Score.ToString();
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _scoreText = FindGameObjectInChildren<TextMeshProUGUI>("ScoreText");
    }
#endif

}
