using TMPro;
using UnityEngine;

public class ScoreUI : BaseBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;




    private void Update()
    {
        _scoreText.text = GameSceneManager.Instance.Score.ToString();
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _scoreText = FindGameObjectInChildren<TextMeshProUGUI>("ScoreText");
    }
#endif

}
