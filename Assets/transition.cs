using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Vector3 maxScale = new Vector3(100, 100, 100);
    private Vector3 minScale = Vector3.zero;
    private float transitionTime = 2f;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(TransitionGrow());
    }

    public IEnumerator TransitionGrow()
    {
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;

        while (elapsedTime < transitionTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, maxScale, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = maxScale; 

        yield return new WaitForSeconds(1f);
        StartCoroutine(TransitionShrink());
    }

    public IEnumerator TransitionShrink()
    {
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;

        while (elapsedTime < transitionTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, minScale, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = minScale;
        Destroy(this.gameObject);
    }
}
