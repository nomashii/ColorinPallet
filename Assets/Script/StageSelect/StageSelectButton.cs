using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    [Header("ステージ情報")]
    [SerializeField] private StageData stageData;

    [Header("詳細パネル")]
    [SerializeField]
    private StageDetailPanel stageDetailPanel;

    [Header("ボタン")]
    [SerializeField] private Button stageButton;

    [Header("カード上の表示")]
    [SerializeField] private Image stageImage;
    [SerializeField] private TMP_Text stageNumberText;

    private void Awake()
    {
        ApplyStageDataToButton();

        if (stageButton != null)
        {
            stageButton.onClick.AddListener(
                OpenStageDetail
            );
        }
    }

    private void OnDestroy()
    {
        if (stageButton != null)
        {
            stageButton.onClick.RemoveListener(
                OpenStageDetail
            );
        }
    }

    private void ApplyStageDataToButton()
    {
        if (stageData == null)
        {
            return;
        }

        if (stageImage != null)
        {
            stageImage.sprite =
                stageData.StageImage;
        }

        if (stageNumberText != null)
        {
            stageNumberText.text =
                stageData.StageNumber;
        }
    }

    public void OpenStageDetail()
    {
        if (stageData == null)
        {
            Debug.LogError(
                $"{gameObject.name}にStageDataが設定されていません。"
            );

            return;
        }

        if (stageDetailPanel == null)
        {
            Debug.LogError(
                "StageDetailPanelが設定されていません。"
            );

            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPopupSE();
        }

        stageDetailPanel.Open(stageData);
    }
}