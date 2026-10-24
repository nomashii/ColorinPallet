using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [Header("リザルト画面")]
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private ResultUI resultUI;
    [SerializeField] private CanvasGroup clearCanvasGroup;

    [Header("各種Manager")]
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private StageGoalManager goalManager;
    [SerializeField] private ResultCalculator resultCalculator;

    [Header("ステージ設定")]
    [SerializeField] private int stageNumber = 1;
    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private bool isLastStage;

    private bool canOpenResult;
    private bool resultProcessed;

    private void Start()
    {
        if (isLastStage && nextStageButton != null)
        {
            nextStageButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (!canOpenResult)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OpenResult();
        }
    }

    public void ShowClearPanel()
    {
        Debug.Log("ShowClearPanelが呼ばれました。");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayClearSE();
        }

        StartCoroutine(FadeInClearPanel());
    }

    private void EnableOpenResult()
    {
        canOpenResult = true;
    }

    private void OpenResult()
    {
        // 万が一、複数回呼ばれても処理しない
        if (resultProcessed)
        {
            return;
        }

        resultProcessed = true;
        canOpenResult = false;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayImportantTapSE();
        }

        if (clearCanvasGroup != null)
        {
            clearCanvasGroup.alpha = 0f;
        }

        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }

        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
        }

        if (resultUI != null)
        {
            resultUI.RefreshUI();
        }

        SaveStageResult();
        AddRewardToMoney();

        Debug.Log("リザルト画面を表示しました。");
    }

    private void SaveStageResult()
    {
        if (resultCalculator == null)
        {
            Debug.LogError(
                "ResultManager：ResultCalculatorが設定されていません。"
            );
            return;
        }

        int earnedStars = resultCalculator.GetStarCount();

        string starKey = $"Stage_{stageNumber}_Stars";
        string clearKey = $"Stage_{stageNumber}_Cleared";

        int previousBest = PlayerPrefs.GetInt(starKey, 0);

        // 過去の記録より今回の星数が多い場合だけ更新
        if (earnedStars > previousBest)
        {
            PlayerPrefs.SetInt(starKey, earnedStars);
        }

        // ステージクリア済みとして保存
        PlayerPrefs.SetInt(clearKey, 1);

        PlayerPrefs.Save();

        int savedStars = Mathf.Max(previousBest, earnedStars);

        Debug.Log(
            $"ステージ{stageNumber}の結果を保存しました。" +
            $"今回の星数：{earnedStars}、最高星数：{savedStars}"
        );
    }

    private void AddRewardToMoney()
    {
        if (moneyManager == null)
        {
            Debug.LogError(
                "ResultManager：MoneyManagerが設定されていません。"
            );
            return;
        }

        if (resultCalculator == null)
        {
            Debug.LogError(
                "ResultManager：ResultCalculatorが設定されていません。"
            );
            return;
        }

        int totalReward = resultCalculator.GetTotalReward();

        moneyManager.AddMoney(totalReward);

        Debug.Log(
            $"リザルト報酬として{totalReward}Gを追加しました。"
        );
    }

    public int GetMoney()
    {
        if (moneyManager == null)
        {
            return 0;
        }

        return moneyManager.GetMoney();
    }

    public int GetCompletedQuest()
    {
        if (questManager == null)
        {
            return 0;
        }

        return questManager.GetCompletedQuest();
    }

    public int GetTotalQuest()
    {
        if (questManager == null)
        {
            return 0;
        }

        return questManager.GetTotalQuest();
    }

    public int GetCompletedPaint()
    {
        if (goalManager == null)
        {
            return 0;
        }

        return goalManager.GetCompletedGoals();
    }

    public int GetTotalPaint()
    {
        if (goalManager == null)
        {
            return 0;
        }

        return goalManager.GetTotalGoals();
    }

    private System.Collections.IEnumerator FadeInClearPanel()
    {
        if (clearPanel == null || clearCanvasGroup == null)
        {
            Debug.LogError(
                "ResultManager：ClearPanelまたはCanvasGroupが未設定です。"
            );
            yield break;
        }

        clearPanel.SetActive(true);
        clearCanvasGroup.alpha = 0f;

        while (clearCanvasGroup.alpha < 1f)
        {
            clearCanvasGroup.alpha += Time.deltaTime * 2f;
            yield return null;
        }

        clearCanvasGroup.alpha = 1f;

        EnableOpenResult();
    }
}