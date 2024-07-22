using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameSceneEvents))]
public class GameSceneManager : BaseBehaviour
{
    public static GameSceneManager Instance;
    public GameSceneEvents GameSceneEvent;

    private int _score;
    public int Score { get { return _score; } }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;

        GameSceneEvent.OnGameStart += GameStartEvent;
        GameSceneEvent.OnAddScore += AddScoreEvent;
    }

    private void AddScoreEvent(GameSceneEvents gameSceneEvents, int amount)
    {
        _score += amount;
    }
    private void OnDestroy()
    {
        GameSceneEvent.OnGameStart -= GameStartEvent;
        GameSceneEvent.OnAddScore -= AddScoreEvent;
    }
    protected override void Initialize()
    {
        base.Initialize();
        _score = 0;
    }

    private void GameStartEvent(GameSceneEvents gameSceneEvents)
    {
        Initialize();
    }





#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        GameSceneEvent = GetComponent<GameSceneEvents>();
    }

#endif
}
