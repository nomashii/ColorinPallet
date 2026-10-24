using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject purchaseConfirmPanel;
    [SerializeField] private GameObject ShopCanvas;

    private ShopItemButton currentItem;

    private void Start()
    {
        purchaseConfirmPanel.SetActive(false);
    }

    // 商品ボタンが押された
    public void OpenConfirm(ShopItemButton item)
    {
        currentItem = item;

        purchaseConfirmPanel.SetActive(true);
    }

    // はい
    public void Buy()
    {
        if (currentItem != null)
        {
            currentItem.Purchase();
        }

        purchaseConfirmPanel.SetActive(false);
    }

    // いいえ
    public void Cancel()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayImportantTapSE();
        }

        purchaseConfirmPanel.SetActive(false);
    }

    // ショップを閉じる
    public void CloseShop()
    {
        purchaseConfirmPanel.SetActive(false); // 確認画面が開いていたら閉じる
        ShopCanvas.SetActive(false);            // ショップ画面を閉じる
        Time.timeScale = 1f;                   // 一時停止を解除
    }
}