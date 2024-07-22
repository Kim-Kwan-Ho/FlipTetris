using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : BaseBehaviour
{
    [SerializeField] private RankBoard _rankBoard;

    private void Start()
    {
        ServerManager.Instance.GetRanking();
    }



#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _rankBoard = GetComponentInChildren<RankBoard>();
    }
#endif



}
