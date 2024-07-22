using System.Collections.Generic;
using UnityEngine;

public class RankBoard : BaseBehaviour
{
    [SerializeField] private Ranking[] _rankings = new Ranking[RankSystem.RANK_MAX_PLAYER];


    private void Start()
    {

    }
    public void SetRankBoard(List<(int, string)> list)
    {
        for (int i = 0; i < Mathf.Min(_rankings.Length, list.Count); i++)
        {
            if (ScoreManager.Instance.NickName == list[i].Item2 && ScoreManager.Instance.Score == list[i].Item1)
            {
                _rankings[i].SetRanking(list[i].Item2 + "(player)", list[i].Item1);
            }
            else
            {
                _rankings[i].SetRanking(list[i].Item2, list[i].Item1);
            }
        }
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();


        for (int i = 0; i < _rankings.Length; i++)
        {
            _rankings[i] = FindGameObjectInChildren<Ranking>("Rank" + (i + 1).ToString());
        }
    }


#endif
}
