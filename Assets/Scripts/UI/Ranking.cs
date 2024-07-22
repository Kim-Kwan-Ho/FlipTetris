using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : BaseBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankingText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetRanking(int rank, int name, int score)
    {
        _rankingText.text = rank.ToString();
        _nameText.text = name.ToString();
        _scoreText.text = score.ToString();
    }




#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _rankingText = FindGameObjectInChildren<TextMeshProUGUI>("RankText");
        _nameText = FindGameObjectInChildren<TextMeshProUGUI>("NameText");
        _scoreText = FindGameObjectInChildren<TextMeshProUGUI>("ScoreText");
    }
#endif
}
