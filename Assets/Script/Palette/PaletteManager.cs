using System.Collections;
using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    public bool IsPaletteOpen { get; private set; }

    private PlayerController playerController;

    [SerializeField] private GameObject smallPaletteButton;
    [SerializeField] private GameObject bigPalette;

    [SerializeField] private float animationDuration = 0.25f;

    private RectTransform bigPaletteRect;
    private Coroutine animationCoroutine;
    private Vector2 originalPosition;

    void Start()
    {
        Debug.Log("PaletteManager Start");
        playerController = Object.FindFirstObjectByType<PlayerController>();

        if (bigPalette != null)
        {
            bigPaletteRect = bigPalette.GetComponent<RectTransform>();
            originalPosition = bigPaletteRect.anchoredPosition;

            bigPalette.SetActive(false);
        }

        if (smallPaletteButton != null)
            smallPaletteButton.SetActive(true);
    }

    // パレットを開く
    public void OpenPalette()
    {
        Debug.Log("OpenPalette");
        if (GameStartManager.Instance.IsOpening)
{
    return;
}
AudioManager.Instance.PlayPopupSE();
        IsPaletteOpen = true;

        playerController.ResetInput();
        playerController.enabled = false;

        bigPalette.SetActive(true);

        // 元の位置で開く
        bigPaletteRect.anchoredPosition = originalPosition;
        bigPaletteRect.localScale = Vector3.zero;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine =
            StartCoroutine(AnimatePalette(
                Vector3.zero,
                Vector3.one,
                true));

        smallPaletteButton.SetActive(false);
    }

    // パレットを閉じる
    public void ClosePalette()
    {
        AudioManager.Instance.PlayPopupSE();
        Time.timeScale = 1f;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine =
            StartCoroutine(AnimatePalette(
                Vector3.one,
                Vector3.zero,
                false));
    }

    IEnumerator AnimatePalette(
        Vector3 startScale,
        Vector3 endScale,
        bool opening)
    {
        float t = 0f;

        bigPaletteRect.localScale = startScale;

        while (t < animationDuration)
        {
            t += Time.unscaledDeltaTime;

            float p = t / animationDuration;
            p = Mathf.Sin(p * Mathf.PI * 0.5f);

            bigPaletteRect.localScale =
                Vector3.Lerp(startScale, endScale, p);

            yield return null;
        }

        bigPaletteRect.localScale = endScale;

        if (opening)
        {
            Time.timeScale = 0f;
        }
        else
        {
            bigPalette.SetActive(false);
            smallPaletteButton.SetActive(true);

            playerController.enabled = true;

            IsPaletteOpen = false;
        }
    }
}