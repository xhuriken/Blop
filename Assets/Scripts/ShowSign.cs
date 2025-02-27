using UnityEngine;
using System.Collections;

public class ShowSign : MonoBehaviour
{
    public RectTransform tutoPanel;

    public float expandDuration = 0.5f;
    public float retractDuration = 0.5f;
    public float targetScaleY = 1f;
    public float minScaleY = 0f;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        Vector3 scale = tutoPanel.localScale;
        scale.y = minScaleY;
        tutoPanel.localScale = scale;
    }

    public void SetActiveState(bool isActive)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        if (isActive)
            _currentCoroutine = StartCoroutine(AnimateScale(targetScaleY, expandDuration));
        else
            _currentCoroutine = StartCoroutine(AnimateScale(minScaleY, retractDuration));
    }

    private IEnumerator AnimateScale(float targetY, float duration)
    {
        float time = 0f;
        float startY = tutoPanel.localScale.y;
        Vector3 scale = tutoPanel.localScale;

        while (time < duration)
        {
            scale.y = Mathf.Lerp(startY, targetY, time / duration);
            tutoPanel.localScale = scale;
            time += Time.deltaTime;
            yield return null;
        }

        scale.y = targetY;
        tutoPanel.localScale = scale;
    }
}
