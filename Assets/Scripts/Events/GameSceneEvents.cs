using System;
using UnityEngine;

public class GameSceneEvents : MonoBehaviour
{
    public Action<GameSceneEvents> OnGameOver;
    public Action<GameSceneEvents> OnGameStart;


    public void CallGameOver()
    {
        OnGameOver?.Invoke(this);
    }

    public void CallGameStart()
    {
        OnGameStart?.Invoke(this);
    }


}
