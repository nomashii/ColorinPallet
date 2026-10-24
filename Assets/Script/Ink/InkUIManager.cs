using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InkUIManager : MonoBehaviour
{
    [SerializeField] private InkManager inkManager;

    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Slider yellowSlider;

    [SerializeField] private Image redGauge;
    [SerializeField] private Image blueGauge;
    [SerializeField] private Image yellowGauge;

    [SerializeField] private TMP_Text redInkText;
    [SerializeField] private TMP_Text blueInkText;
    [SerializeField] private TMP_Text yellowInkText;

    [SerializeField] private TMP_Text SmallredInkText;
    [SerializeField] private TMP_Text SmallblueInkText;
    [SerializeField] private TMP_Text SmallyellowInkText;

    

    private void Update()
    {
        UpdateInkUI();
    }



    private void UpdateInkUI()
    {
        if (inkManager == null)
        {
            inkManager = InkManager.Instance;

            if (inkManager == null)
            {
                return;
            }
        }

        float red = inkManager.GetRedInk();
        float blue = inkManager.GetBlueInk();
        float yellow = inkManager.GetYellowInk();
        float max = inkManager.MaxInk;

        if (max <= 0f)
        {
            Debug.LogError("InkUIManager：MaxInkが0以下です。");
            return;
        }

        // Sliderは設定されているものだけ更新
        if (redSlider != null)
        {
            redSlider.maxValue = max;
            redSlider.value = red;
        }

        if (blueSlider != null)
        {
            blueSlider.maxValue = max;
            blueSlider.value = blue;
        }

        if (yellowSlider != null)
        {
            yellowSlider.maxValue = max;
            yellowSlider.value = yellow;
        }

        // 色付きゲージ画像
        if (redGauge != null)
        {
            redGauge.fillAmount = red / max;
        }

        if (blueGauge != null)
        {
            blueGauge.fillAmount = blue / max;
        }

        if (yellowGauge != null)
        {
            yellowGauge.fillAmount = yellow / max;
        }

        // 「現在値/最大値」の表示
        if (redInkText != null)
        {
            redInkText.text =
                Mathf.CeilToInt(red) + "/" + Mathf.CeilToInt(max);
        }

        if (blueInkText != null)
        {
            blueInkText.text =
                Mathf.CeilToInt(blue) + "/" + Mathf.CeilToInt(max);
        }

        if (yellowInkText != null)
        {
            yellowInkText.text =
                Mathf.CeilToInt(yellow) + "/" + Mathf.CeilToInt(max);
        }

        // 小さいインク数値表示
        if (SmallredInkText != null)
        {
            SmallredInkText.text =
                Mathf.CeilToInt(red).ToString();
        }

        if (SmallblueInkText != null)
        {
            SmallblueInkText.text =
                Mathf.CeilToInt(blue).ToString();
        }

        if (SmallyellowInkText != null)
        {
            SmallyellowInkText.text =
                Mathf.CeilToInt(yellow).ToString();
        }
    }
}