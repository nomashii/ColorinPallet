using System.Collections;
using UnityEngine;

public class InkReveal : MonoBehaviour
{
    [SerializeField] private RectTransform revealMask;

    [SerializeField] private float duration = 0.35f;

    [SerializeField] private float endScale = 5f;

    public void Play()
{
    StopAllCoroutines();
    StartCoroutine(Reveal());
}

    private IEnumerator Reveal()
{
    revealMask.localScale = Vector3.zero;

    float t = 0;

    while(t<0.25f)
    {
        t += Time.unscaledDeltaTime;

        revealMask.localScale =
            Vector3.Lerp(Vector3.zero,
            Vector3.one * 2.5f,
            t/0.25f);

        yield return null;
    }

    revealMask.localScale = Vector3.one * 2.5f;
}
}