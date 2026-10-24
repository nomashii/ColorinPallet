using UnityEngine;
using TMPro; // TextMeshProを使うための必須の合言葉！

public class CurrentColorUI : MonoBehaviour
{
    private TextMeshProUGUI colorText;
    private PlayerFormController PlayerFormController;

    void Start()
    {
        // 自分がくっついているTextMeshProの部品を自動で見つける
        colorText = GetComponent<TextMeshProUGUI>();
        
        // シーン内にいる PlayerFormController（管理人）を自動で見つける
        PlayerFormController = FindObjectOfType<PlayerFormController>();

        if (PlayerFormController == null)
        {
            Debug.LogWarning("CurrentColorUI: PlayerFormControllerが見つかりません！");
        }
    }

    void Update()
    {
        // 管理人とテキストの両方が無事に見つかっていれば、毎フレーム文字を更新する
        if (PlayerFormController != null && colorText != null)
        {
            // PlayerFormControllerから今の色を聞き出して、テキストに表示！
            colorText.text = "現在の色: " + PlayerFormController.GetCurrentColor().ToString();
            
        }
    }
}