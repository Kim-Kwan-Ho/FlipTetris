using System;
using UnityEngine;

public class GameSceneEvents : MonoBehaviour
{
    public Action<GameSceneEvents> OnGameOver;
    public Action<GameSceneEvents> OnGameStart;
    public Action<GameSceneEvents, int> OnAddScore;



    public void CallGameOver()
    {
        OnGameOver?.Invoke(this);
    }

    public void CallGameStart()
    {
        OnGameStart?.Invoke(this);
    }

    public void CallOnAddScore(int amount)
    {
        OnAddScore?.Invoke(this, amount);
    }

}
