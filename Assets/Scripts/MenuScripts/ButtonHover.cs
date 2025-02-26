using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using Febucci.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _initialButtonScale;
    private float _initialTextScale;
    private Vector3 _initialRotation;
    public TMP_Text Text;

    public float buttonScaleMultiplier = 1.1f;
    public float textScaleMultiplier = 1.2f;
    public float animationDuration = 0.2f;
    public float rotationAngle = 9f;

    //private TextAnimator _textAnimator;

    private void Start()
    {
        _initialButtonScale = transform.localScale;
        _initialTextScale = Text.fontSize;
        _initialRotation = Text.rectTransform.eulerAngles;

        //_textAnimator = Text.GetComponent<TextAnimator>();

        //if (_textAnimator == null)
        //{
        //    Debug.LogWarning("TextAnimator error : doesn't exist");
        //}
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(AnimateScale(transform, _initialButtonScale * buttonScaleMultiplier, animationDuration));
        StartCoroutine(AnimateTextScale(_initialTextScale * textScaleMultiplier, animationDuration));
        StartCoroutine(AnimateRotation(_initialRotation.z + rotationAngle, animationDuration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(AnimateScale(transform, _initialButtonScale, animationDuration));
        StartCoroutine(AnimateTextScale(_initialTextScale, animationDuration));
        StartCoroutine(AnimateRotation(_initialRotation.z, animationDuration));
    }

    private IEnumerator AnimateScale(Transform target, Vector3 targetScale, float duration)
    {
        float elapsed = 0f;
        Vector3 startScale = target.localScale;

        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;
    }

    private IEnumerator AnimateTextScale(float targetFontSize, float duration)
    {
        float elapsed = 0f;
        float startFontSize = Text.fontSize;

        while (elapsed < duration)
        {
            Text.fontSize = Mathf.Lerp(startFontSize, targetFontSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Text.fontSize = targetFontSize;
    }

    private IEnumerator AnimateRotation(float targetAngle, float duration)
    {
        float elapsed = 0f;
        float startAngle = Text.rectTransform.eulerAngles.z;

        while (elapsed < duration)
        {
            float angle = Mathf.Lerp(startAngle, targetAngle, elapsed / duration);
            Text.rectTransform.eulerAngles = new Vector3(0, 0, angle);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Text.rectTransform.eulerAngles = new Vector3(0, 0, targetAngle);
    }
}
