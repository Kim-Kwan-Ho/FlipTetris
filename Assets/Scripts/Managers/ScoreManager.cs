using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public class ScoreManager : BaseBehaviour
{
    public static ScoreManager Instance;
    private List<(int, string)> _ranking = new List<(int, string)>();
    public int Score;
    public string NickName;
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (PlayerPrefs.HasKey("BestScore"))
        {
            Score = PlayerPrefs.GetInt("BestScore");
            NickName = PlayerPrefs.GetString("Nickname");
        }
        else
        {
            Score = 0;
        }
    }
    public void UpdateScore()
    {
        Score = PlayerPrefs.GetInt("BestScore");
        NickName = PlayerPrefs.GetString("Nickname");
    }


    public void GetScoreRanking(GetLeaderboardResult result)
    {
        _ranking = new List<(int, string)>();

        foreach (var VARIABLE in result.Leaderboard)
        {
            _ranking.Add((VARIABLE.StatValue, VARIABLE.DisplayName));
        }

        var rankboard = FindObjectOfType<RankBoard>();
        rankboard.SetRankBoard(_ranking);
    }
}
