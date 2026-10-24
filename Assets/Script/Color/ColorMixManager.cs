using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ColorMixManager : MonoBehaviour
{
    [SerializeField] private PlayerFormController playerFormController;
    [SerializeField] private CanvasGroup mixCanvas;

    [Header("MixArea画像")]
    [SerializeField] private Sprite whiteMixSprite;
    [SerializeField] private Sprite redMixSprite;
    [SerializeField] private Sprite blueMixSprite;
    [SerializeField] private Sprite yellowMixSprite;
    [SerializeField] private Sprite purpleMixSprite;
    [SerializeField] private Sprite greenMixSprite;
    [SerializeField] private Sprite orangeMixSprite;


    [Header("混色エリア")]
    [SerializeField] private Image mixAreaImage;


    [Header("混色結果を表示する場所")]
    [SerializeField] private Image purpleResultImage;
    [SerializeField] private Image greenResultImage;
    [SerializeField] private Image orangeResultImage;

private ColorType? firstColor;
private bool isMixedColorLocked;


    void Start()
    {
        ResetToWhite(false);


        if (purpleResultImage != null)
            purpleResultImage.gameObject.SetActive(false);


        if (greenResultImage != null)
            greenResultImage.gameObject.SetActive(false);


        if (orangeResultImage != null)
            orangeResultImage.gameObject.SetActive(false);
    }




public void PutColor(ColorType droppedColor)
{
    Debug.Log("PutColor受信: " + droppedColor);
    if (droppedColor == ColorType.White)
     {
        ResetToWhite();
        return;
     }
      if (droppedColor == ColorType.None)
    {
        return;
    }


    if (isMixedColorLocked)
    {
        Debug.Log("混色済みです。白にリセットしてから新しい色を置いてください。");
        return;
    }


    if (firstColor == null)
    {
        firstColor = droppedColor;

        ChangePlayerColor(droppedColor);


        ChangeMixAreaSprite(droppedColor);

        Debug.Log("1色目：" + droppedColor);
        return;
    }


    ColorType mixedColor = GetMixedColor(firstColor.Value, droppedColor);

ChangePlayerColor(mixedColor);

UnlockMixedColor(mixedColor);


    if (mixAreaImage != null)
    {
        ChangeMixAreaSprite(mixedColor);
    }


    Debug.Log(firstColor + " + " + droppedColor + " = " + mixedColor);


    isMixedColorLocked = true;
    firstColor = null;
}


public void ResetToWhite(bool playSE = true)
{
    firstColor = null;
    isMixedColorLocked = false;

    if (playSE)
        AudioManager.Instance.PlayColorChangeSE();

    ChangePlayerColor(ColorType.White);

    if (mixAreaImage != null)
    {
        ChangeMixAreaSprite(ColorType.White);
    }

    Debug.Log("白にリセットしました");
}

    private void ChangePlayerColor(ColorType color)
{
    if (playerFormController == null)
        return;

    // 同じ色なら何もしない
    if (playerFormController.GetCurrentColor() == color)
        return;

    AudioManager.Instance.PlayColorChangeSE();

    playerFormController.ChangeForm(color);
}


    private void UnlockMixedColor(ColorType color)
    {
        if (color == ColorType.Purple && purpleResultImage != null)
        {
            purpleResultImage.color = ConvertToUnityColor(ColorType.Purple);
            purpleResultImage.gameObject.SetActive(true);
        }
        else if (color == ColorType.Green && greenResultImage != null)
        {
            greenResultImage.color = ConvertToUnityColor(ColorType.Green);
            greenResultImage.gameObject.SetActive(true);
        }
        else if (color == ColorType.Orange && orangeResultImage != null)
        {
            orangeResultImage.color = ConvertToUnityColor(ColorType.Orange);
            orangeResultImage.gameObject.SetActive(true);
        }
    }


    private ColorType GetMixedColor(ColorType colorA, ColorType colorB)
{
    if (colorA == ColorType.White) return colorB;
    if (colorB == ColorType.White) return colorA;

    if (IsPair(colorA, colorB, ColorType.Red, ColorType.Blue))
        return ColorType.Purple;

    if (IsPair(colorA, colorB, ColorType.Blue, ColorType.Yellow))
        return ColorType.Green;

    if (IsPair(colorA, colorB, ColorType.Red, ColorType.Yellow))
        return ColorType.Orange;

    return colorA; // or White（安全側）
}


    private bool IsPair(ColorType colorA, ColorType colorB, ColorType targetA, ColorType targetB)
    {
        return
            (colorA == targetA && colorB == targetB) ||
            (colorA == targetB && colorB == targetA);
    }

    private void ChangeMixAreaSprite(ColorType colorType)
{
    if (mixAreaImage == null)
        return;

    switch (colorType)
    {
        case ColorType.White:
            mixAreaImage.sprite = whiteMixSprite;
            break;

        case ColorType.Red:
            mixAreaImage.sprite = redMixSprite;
            break;

        case ColorType.Blue:
            mixAreaImage.sprite = blueMixSprite;
            break;

        case ColorType.Yellow:
            mixAreaImage.sprite = yellowMixSprite;
            break;

        case ColorType.Purple:
            mixAreaImage.sprite = purpleMixSprite;
            break;

        case ColorType.Green:
            mixAreaImage.sprite = greenMixSprite;
            break;

        case ColorType.Orange:
            mixAreaImage.sprite = orangeMixSprite;
            break;
    }
}
    private Color ConvertToUnityColor(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.Red:
                return Color.red;
            case ColorType.Blue:
                return Color.blue;
            case ColorType.Yellow:
                return Color.yellow;
            case ColorType.Purple:
                return new Color(0.5f, 0f, 1f);
            case ColorType.Green:
                return Color.green;
            case ColorType.Orange:
                return new Color(1f, 0.5f, 0f);
            default:
                return Color.white;
        }
    }
private IEnumerator FadeMixArea(ColorType color)
{
    // 消える
    float t = 0f;

    while (t < 0.1f)
    {
        t += Time.unscaledDeltaTime;
        mixCanvas.alpha = Mathf.Lerp(1f, 0f, t / 0.1f);
        yield return null;
    }

    // 色変更
    StartCoroutine(FadeMixArea(color));

    // 出る
    t = 0f;

    while (t < 0.15f)
    {
        t += Time.unscaledDeltaTime;
        mixCanvas.alpha = Mathf.Lerp(0f, 1f, t / 0.15f);
        yield return null;
    }

    mixCanvas.alpha = 1f;
}
}
