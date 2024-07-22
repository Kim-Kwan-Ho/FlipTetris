using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : BaseBehaviour
{
    private bool _isLoading;

    protected override void Initialize()
    {
        base.Initialize();
        _isLoading = false;
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
}
