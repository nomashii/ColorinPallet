using System.Collections;
using UnityEngine;

public class InkPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject smallButton;
    [SerializeField] private GameObject bigPanel;

    [SerializeField] private float animationDuration = 0.15f;

    private RectTransform panelRect;
    private Coroutine animationCoroutine;
    private Vector2 originalPosition;

   void Start()
    {
    panelRect = bigPanel.GetComponent<RectTransform>();

    originalPosition = panelRect.anchoredPosition;

    bigPanel.SetActive(false);
    }

    public void OpenPanel()
{
    if (GameStartManager.Instance.IsOpening)
    {
        return;
    }
    AudioManager.Instance.PlayPopupSE();
    bigPanel.SetActive(true);

    // 毎回元の位置に戻す
    panelRect.anchoredPosition = originalPosition;
    panelRect.localScale = Vector3.zero;

    if (animationCoroutine != null)
        StopCoroutine(animationCoroutine);

    animationCoroutine =
        StartCoroutine(AnimatePanel(
            Vector3.zero,
            Vector3.one,
            true));

    smallButton.SetActive(false);
    Time.timeScale = 0f;
}

    public void ClosePanel()
{
    AudioManager.Instance.PlayPopupSE();
    Time.timeScale = 1f;
    if (animationCoroutine != null)
        StopCoroutine(animationCoroutine);

    animationCoroutine =
        StartCoroutine(AnimatePanel(
            Vector3.one,
            Vector3.zero,
            false));
}

    IEnumerator AnimatePanel(
    Vector3 startScale,
    Vector3 endScale,
    bool opening)
{
    float t = 0;

    panelRect.localScale = startScale;

    while (t < animationDuration)
    {
        t += Time.unscaledDeltaTime;

        float p = t / animationDuration;

        // 少し気持ちいい動き
        p = Mathf.Sin(p * Mathf.PI * 0.5f);

        panelRect.localScale =
            Vector3.Lerp(startScale, endScale, p);

        yield return null;
    }

    panelRect.localScale = endScale;

    if (opening)
{
    Time.timeScale = 0f;
}
else
{
    bigPanel.SetActive(false);
    smallButton.SetActive(true);
}
}
}