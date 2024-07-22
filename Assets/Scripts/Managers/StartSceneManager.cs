using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : BaseBehaviour
{
    public static StartSceneManager instance;
    private bool _isLoading;
    [SerializeField] private RankBoard _rankBoard;
    protected override void Initialize()
    {
        base.Initialize();
        _isLoading = false;
        if (instance == null)
            instance = this;
    }

    private void Start()
    {

    }

    public void SetRanking(List<(int, string)> ranking)
    {
        _rankBoard.SetRankBoard(ranking);
    }

    private void Update()
    {
        if (_isLoading)
            return;


        if (Input.anyKeyDown)
        {
            FadeManager.Instance.ChangeScene(SceneName.GAME_SCENE);
            _isLoading = true;
        }
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _rankBoard = FindObjectOfType<RankBoard>();
    }
#endif
}
