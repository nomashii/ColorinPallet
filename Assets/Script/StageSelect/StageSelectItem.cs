using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectItem : MonoBehaviour
{
    [Header("ステージ情報")]
    [SerializeField] private int stageNumber = 1;
    [SerializeField] private string sceneName;
    [SerializeField] private string displayName = "1-1";

    [Header("基本UI")]
    [SerializeField] private Button stageButton;
    [SerializeField] private TMP_Text stageNameText;

    [Header("解放・未解放表示")]
    [SerializeField] private Image stageButtonImage;
    [SerializeField] private Sprite unlockedStageSprite;
    [SerializeField] private Sprite lockedStageSprite;
    [SerializeField] private GameObject lockImage;

    [Header("星フレーム")]
    [SerializeField] private Image starFrameImage;

    [Tooltip("解放時のStarFrameの色")]
    [SerializeField] private Color unlockedStarFrameColor = Color.white;

    [Tooltip("未解放時のStarFrameの色")]
    [SerializeField]
    private Color lockedStarFrameColor =
        new Color(0.62f, 0.62f, 0.62f, 1f);

    [SerializeField]
    private bool isImplemented = true;

    [Header("星")]
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite grayStarSprite;
    [SerializeField] private Sprite yellowStarSprite;

    private void Start()
    {
        RefreshStageCard();
    }

    private void OnEnable()
    {
        RefreshStageCard();
    }

    private void RefreshStageCard()
    {
        if (stageNameText != null)
        {
            stageNameText.text = displayName;
        }

        bool isUnlocked = IsStageUnlocked();

        UpdateLockState(isUnlocked);

        int savedStarCount = GetSavedStarCount();
        UpdateStars(isUnlocked ? savedStarCount : 0);
    }

    private void UpdateLockState(bool isUnlocked)
    {
        if (stageButton != null)
        {
            stageButton.interactable = isUnlocked;
        }

        if (stageButtonImage != null)
        {
            stageButtonImage.sprite =
                isUnlocked
                    ? unlockedStageSprite
                    : lockedStageSprite;

            stageButtonImage.color = Color.white;
        }

        if (lockImage != null)
        {
            lockImage.SetActive(!isUnlocked);
        }

        if (starFrameImage != null)
        {
            starFrameImage.color =
                isUnlocked
                    ? unlockedStarFrameColor
                    : lockedStarFrameColor;
        }
    }

    public void SelectStage()
    {
        if (!IsStageUnlocked())
        {
            return;
        }

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError(
                $"{gameObject.name}：Scene Nameが設定されていません。"
            );
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private bool IsStageUnlocked()
    {
        // 未実装なら常にロック
        if (!isImplemented)
        {
            return false;
        }

        // ステージ1は最初から解放
        if (stageNumber == 1)
        {
            return true;
        }

        string previousClearKey =
            $"Stage_{stageNumber - 1}_Cleared";

        return PlayerPrefs.GetInt(previousClearKey, 0) == 1;
    }

    private int GetSavedStarCount()
    {
        string starKey =
            $"Stage_{stageNumber}_Stars";

        return Mathf.Clamp(
            PlayerPrefs.GetInt(starKey, 0),
            0,
            starImages.Length
        );
    }

    private void UpdateStars(int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (starImages[i] == null)
            {
                continue;
            }

            starImages[i].sprite =
                i < starCount
                    ? yellowStarSprite
                    : grayStarSprite;
        }
    }
}