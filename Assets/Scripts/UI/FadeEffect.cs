using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeEffect : BaseBehaviour
{
    [SerializeField] private TextMeshProUGUI _fadeText;
    [SerializeField] private float _fadeTime = 0.5f;
    private void Start()
    {
        StartCoroutine(CoFadeEffect(false));
    }


    private IEnumerator CoFadeEffect(bool fade)
    {
        float time = 0;
        Color targetColor;
        Color curColor = _fadeText.color;
        if (fade)
        {
            targetColor = new Color(1, 1, 1, 1);
        }
        else
        {
            targetColor = new Color(1, 1, 1, 0);
        }
        while (time < _fadeTime)
        {
            _fadeText.color = Color.Lerp(curColor, targetColor, time / _fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        _fadeText.color = targetColor;

        StartCoroutine(CoFadeEffect(!fade));
    }


#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _fadeText = GetComponentInChildren<TextMeshProUGUI>();

    }
#endif
}
