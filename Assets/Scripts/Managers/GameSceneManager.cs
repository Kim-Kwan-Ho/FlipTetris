using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameSceneEvents))]
public class GameSceneManager : BaseBehaviour
{
    public static GameSceneManager Instance;
    public GameSceneEvents GameSceneEvent;


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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameSceneEvent.CallGameStart();
        }
    }




#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        GameSceneEvent = GetComponent<GameSceneEvents>();
    }

#endif
}
