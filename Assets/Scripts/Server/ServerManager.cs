using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class ServerManager : BaseBehaviour
{
    public static ServerManager Instance;

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
        OnGuestLogin();
    }


    protected override void Initialize()
    {
        base.Initialize();

    }

    public void GetRanking()
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = "Ranking",
            StartPosition = 0,
            MaxResultsCount = 3
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnRequestFailed);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        ScoreManager.Instance.GetScoreRanking(result);
    }


    private void OnGuestLogin()
    {
        var request = new LoginWithCustomIDRequest()
        { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnRequestFailed);
    }
    public void SubmitScore(string playerName, int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate(){StatisticName = "Ranking", Value =  score}
            }
        };

        var displayNameRequest = new UpdateUserTitleDisplayNameRequest { DisplayName = playerName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(displayNameRequest, OnDisplayNameUpdated, OnRequestFailed);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnScoreSubmitted, OnRequestFailed);
    }

    private void OnScoreSubmitted(UpdatePlayerStatisticsResult result)
    {
        Debug.Log(result);
    }
    private void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("이름 업데이트 성공");
    }



    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success");
        GetRanking();
    }

    private void OnRequestFailed(PlayFabError error)
    {
        Debug.Log("Request Failed " + error.ErrorMessage);
    }

}
