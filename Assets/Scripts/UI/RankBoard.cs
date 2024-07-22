using UnityEngine;

public class RankBoard : BaseBehaviour
{
    [SerializeField] private Ranking[] _rankings = new Ranking[RankSystem.RANK_MAX_PLAYER];




#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();


        for (int i = 0; i < _rankings.Length; i++)
        {
            _rankings[i] = FindGameObjectInChildren<Ranking>("Rank" + (i+1).ToString());
        }
    }


#endif
}
