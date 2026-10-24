using UnityEngine;
using System.Collections.Generic;

public class PaintableObject : MonoBehaviour
{
    [Header("各色の見た目")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite whiteSprite;
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite yellowSprite;
    [SerializeField] private Sprite purpleSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private InkManager inkManager;
    private PlayerFormController playerFormController;
    [SerializeField] private int moneyReward = 10;
    private MoneyManager moneyManager;

    [Header("近づいた時の見た目")]
    [SerializeField] private GameObject highlightObject;
    private StageGoalManager goalManager;
    [SerializeField] private bool hasQuest;
[SerializeField] private ColorType questColor;
[SerializeField] private NPCQuestController targetNPC;
[SerializeField] private ParticleSystem paintEffect;



private bool goalCounted = false;

    private bool canPaint;
    private ColorType paintedColor = ColorType.None;
    [SerializeField]private bool disableGoalForTest;

    void Start()
{
    Debug.Log("default=" + defaultSprite);
Debug.Log("white=" + whiteSprite);
Debug.Log("red=" + redSprite);
Debug.Log("blue=" + blueSprite);
Debug.Log("yellow=" + yellowSprite);
Debug.Log("purple=" + purpleSprite);
Debug.Log("green=" + greenSprite);
Debug.Log("orange=" + orangeSprite);
    moneyManager = FindFirstObjectByType<MoneyManager>();
    goalManager = FindFirstObjectByType<StageGoalManager>();
    inkManager = FindFirstObjectByType<InkManager>();
    playerFormController = FindFirstObjectByType<PlayerFormController>();

    Debug.Log(defaultSprite);
    paintedColor = ColorType.None;

    UpdateObjectSprite(ColorType.None);
    if (paintEffect != null)
{
    paintEffect.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
}

    if (highlightObject != null)
    {
        highlightObject.SetActive(false);
    }
    
}

    public void TryPaint()
{
    if (GameStartManager.Instance != null &&
        GameStartManager.Instance.IsOpening)
    {
        return;
    }
    // 一度塗ったオブジェクトはもう塗れない
if (paintedColor != ColorType.None)
{
    Debug.Log("すでに塗られています");
    return;
}
    Debug.Log("ここまで到達");

    Debug.Log("canPaint = " + canPaint);

    if (!canPaint)
    {
        Debug.Log("canPaintで停止");
        return;
    }

    ColorType currentColor = playerFormController.GetCurrentColor();

    Debug.Log("現在色 = " + currentColor);

        if (!inkManager.UseInk(currentColor, 1f))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayFailedPaintSE();
            }

            if (MessageUIManager.Instance != null)
            {
                MessageUIManager.Instance.ShowMessage("インクが足りないよ！");
            }

            Debug.Log("インク不足で停止");
            return;
        }

        Debug.Log("インク消費成功");
    
    if (hasQuest)
{
    Debug.Log("現在色：" + currentColor);
Debug.Log("正解色：" + questColor);
    if (currentColor == questColor)
{
    Debug.Log("正解！");
    targetNPC.CompleteQuest();
}
else
{
    Debug.Log("不正解！");
    targetNPC.FailQuest();
}
}

paintedColor = currentColor;

UpdateObjectSprite(currentColor);

Debug.Log("① UpdateObjectSprite終了");

// ←ここから追加
if (paintEffect != null)
{
    var main = paintEffect.main;

    switch (currentColor)
    {
        case ColorType.Red:
            main.startColor = Color.red;
            break;

        case ColorType.Blue:
            main.startColor = Color.blue;
            break;

        case ColorType.Yellow:
            main.startColor = Color.yellow;
            break;

        case ColorType.Green:
            main.startColor = Color.green;
            break;

        case ColorType.Purple:
            main.startColor = new Color(0.6f, 0f, 1f);
            break;

        case ColorType.Orange:
            main.startColor = new Color(1f, 0.5f, 0f);
            break;

        default:
            main.startColor = Color.white;
            break;
    }

    paintEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
paintEffect.Play();

}

// ←ここまで追加

playerFormController.PlayPaintMotion(currentColor);

Debug.Log("② PlayPaintMotion終了");

if (!goalCounted)
{
    if (moneyManager != null)
    {
        moneyManager.AddMoney(moneyReward);
    }

    goalManager.CompleteGoal();
    goalCounted = true;
    Debug.Log("③ Goal処理終了");
}

Debug.Log("スプライト変更完了");

Debug.Log(currentColor + "で塗りました");
}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            canPaint = true;

            if (highlightObject != null)
            {
                highlightObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            canPaint = false;

            if (highlightObject != null)
            {
                highlightObject.SetActive(false);
            }
        }
    }

    private Color ConvertToUnityColor(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.None:
                return Color.gray;
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
    public ColorType GetPaintedColor()
{
    return paintedColor;
}
private void UpdateObjectSprite(ColorType colorType)
{
    switch (colorType)
    {
        case ColorType.None:
            spriteRenderer.sprite = defaultSprite;
            break;

        case ColorType.White:
            spriteRenderer.sprite = whiteSprite;
            break;

        case ColorType.Red:
            spriteRenderer.sprite = redSprite;
            break;

        case ColorType.Blue:
            spriteRenderer.sprite = blueSprite;
            break;

        case ColorType.Yellow:
            spriteRenderer.sprite = yellowSprite;
            break;

        case ColorType.Purple:
            spriteRenderer.sprite = purpleSprite;
            break;

        case ColorType.Green:
            spriteRenderer.sprite = greenSprite;
            break;

        case ColorType.Orange:
            spriteRenderer.sprite = orangeSprite;
            break;
    }
}
}
