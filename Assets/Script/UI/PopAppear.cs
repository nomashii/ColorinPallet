using System.Collections;
using UnityEngine;

public class PopAppear : MonoBehaviour
{
    [Header("アニメーション")]
    [SerializeField] private float startScale = 3f;
    [SerializeField] private float overshootScale = 1.15f;
    [SerializeField] private float animationTime = 0.18f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Play()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        transform.localScale = originalScale * startScale;

        float t = 0f;

        while (t < animationTime)
        {
            t += Time.deltaTime;

            transform.localScale = Vector3.Lerp(
                originalScale * startScale,
                originalScale * 0.8f,
                t / animationTime);

            yield return null;
        }

        t = 0f;

        while (t < animationTime * 0.5f)
        {
            t += Time.deltaTime;

            transform.localScale = Vector3.Lerp(
                originalScale * 0.8f,
                originalScale * overshootScale,
                t / (animationTime * 0.5f));

            yield return null;
        }

        t = 0f;

        while (t < animationTime * 0.5f)
        {
            t += Time.deltaTime;

            transform.localScale = Vector3.Lerp(
                originalScale * overshootScale,
                originalScale,
                t / (animationTime * 0.5f));

            yield return null;
        }

        transform.localScale = originalScale;
    }
}