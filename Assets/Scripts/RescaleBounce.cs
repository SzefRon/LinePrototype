using UnityEngine;
using System.Collections;

public class RescaleBounce : MonoBehaviour
{
    public float duration = 1f;
    public float damping = 0.9f;
    public float frequency;

    private Vector3 originalScale;
    private Coroutine rescaleCoroutine;

    public float maxAmplitude;

    void Start()
    {
        originalScale = transform.localScale;

    }

    public float DampedVibration(float t)
    {
        return maxAmplitude * Mathf.Exp(-damping * t / 2) * Mathf.Cos(frequency * t + Mathf.PI /2 ) + originalScale.y;
    }

    public void  StartBounce()
    {
        if (rescaleCoroutine != null)
            StopCoroutine(rescaleCoroutine);

        rescaleCoroutine = StartCoroutine(BounceCoroutine());
    }

    IEnumerator BounceCoroutine()
    {
        float timeElapsed = 0f;
        Vector3 currentScale = originalScale;

        while (timeElapsed < duration)
        {
            currentScale.y = DampedVibration(timeElapsed);
            transform.localScale = currentScale;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //transform.localScale = originalScale;
    }
}
