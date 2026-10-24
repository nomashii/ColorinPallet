using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageDetailPanel : MonoBehaviour
{
    [Header("パネル")]
    [SerializeField] private GameObject panelObject;

    [Header("基本情報")]
    [SerializeField] private Image stageImage;
    [SerializeField] private TMP_Text stageNumberText;
    [SerializeField] private TMP_Text stageNameText;
    [SerializeField] private TMP_Text stageDescriptionText;

    [Header("ステージ情報")]
    [SerializeField] private TMP_Text targetCountText;
    [SerializeField] private TMP_Text onayamiCountText;
    [SerializeField] private TMP_Text clearRewardText;

    [Header("星")]
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite obtainedStarSprite;
    [SerializeField] private Sprite unobtainedStarSprite;

    [Header("ボタン")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button playButton;

    private StageData selectedStageData;

    private void Awake()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Close);
        }

        if (playButton != null)
        {
            playButton.onClick.AddListener(PlaySelectedStage);
        }

        panelObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(Close);
        }

        if (playButton != null)
        {
            playButton.onClick.RemoveListener(PlaySelectedStage);
        }
    }

    public void Open(StageData stageData)
    {
        if (stageData == null)
        {
            Debug.LogError("StageDataが設定されていません。");
            return;
        }

        selectedStageData = stageData;

        if (stageImage != null)
        {
            stageImage.sprite = stageData.StageImage;
        }

        if (stageNumberText != null)
        {
            stageNumberText.text = stageData.StageNumber;
        }

        if (stageNameText != null)
        {
            stageNameText.text = stageData.StageName;
        }

        if (stageDescriptionText != null)
        {
            stageDescriptionText.text =
                stageData.StageDescription;
        }

        if (targetCountText != null)
        {
            targetCountText.text =
                stageData.TargetCount.ToString();
        }

        if (onayamiCountText != null)
        {
            onayamiCountText.text =
                stageData.OnayamiCount.ToString();
        }

        if (clearRewardText != null)
        {
            clearRewardText.text =
                stageData.ClearReward + "G";
        }

        string starKey =
            $"Stage_{stageData.StageId}_Stars";

        int savedStarCount = Mathf.Clamp(
            PlayerPrefs.GetInt(starKey, 0),
            0,
            starImages.Length
        );

        for (int i = 0; i < starImages.Length; i++)
        {
            if (starImages[i] == null)
            {
                continue;
            }

            starImages[i].sprite =
                i < savedStarCount
                    ? obtainedStarSprite
                    : unobtainedStarSprite;

            starImages[i].color = Color.white;
        }

        panelObject.SetActive(true);
        panelObject.transform.SetAsLastSibling();
    }

    private void ApplyStageData(StageData stageData)
    {
        stageImage.sprite =
            stageData.StageImage;

        stageNumberText.text =
            stageData.StageNumber;

        stageNameText.text =
            stageData.StageName;

        stageDescriptionText.text =
            stageData.StageDescription;

        targetCountText.text =
            $"{stageData.TargetCount}つ";

        onayamiCountText.text =
            $"{stageData.OnayamiCount}つ";

        clearRewardText.text =
            $"{stageData.ClearReward}G";

        int obtainedStars =
            LoadObtainedStars(stageData);

        UpdateStars(obtainedStars);
    }

    private int LoadObtainedStars(StageData stageData)
    {
        string saveKey =
            $"Stage_{stageData.StageNumber}_Stars";

        return PlayerPrefs.GetInt(
            saveKey,
            0
        );
    }

    private void UpdateStars(int obtainedStars)
    {
        obtainedStars = Mathf.Clamp(
            obtainedStars,
            0,
            starImages.Length
        );

        for (int i = 0;
             i < starImages.Length;
             i++)
        {
            bool isObtained =
                i < obtainedStars;

            starImages[i].sprite =
                isObtained
                ? obtainedStarSprite
                : unobtainedStarSprite;
        }
    }

    public void Close()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayImportantTapSE();
        }

        panelObject.SetActive(false);
        selectedStageData = null;
    }

    private void PlaySelectedStage()
    {
        if (selectedStageData == null)
        {
            Debug.LogError(
                "選択中のステージがありません。"
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(
                selectedStageData.SceneName
            ))
        {
            Debug.LogError(
                "遷移先のScene Nameが設定されていません。"
            );

            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayImportantTapSE();
        }

        SceneManager.LoadScene(
            selectedStageData.SceneName
        );
    }
}