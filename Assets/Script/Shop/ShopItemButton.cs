using UnityEngine;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    [Header("商品情報")]
    [SerializeField] private int price;

    [SerializeField] private int addInk;

    [SerializeField] private ColorType inkColor;

    [SerializeField] private ShopManager shopManager;

    [SerializeField] private MoneyManager moneyManager;

    [SerializeField] private TMP_Text priceText;

    private void Start()
    {
        if (priceText != null)
        {
            priceText.text = price + "G";
        }
    }

    public void ClickItem()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPopupSE();
        }

        shopManager.OpenConfirm(this);
    }

    public void Purchase()
    {
        if (!moneyManager.UseMoney(price))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMissSE();
            }

            if (MessageUIManager.Instance != null)
            {
                MessageUIManager.Instance.ShowMessage(
                    "おかねが足りないよ！"
                );
            }

            Debug.Log("所持金不足");
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayShoppingSE();
        }

        // インク追加
        InkManager.Instance.AddInk(
            inkColor,
            addInk
        );

        Debug.Log("購入成功");
    }
}

public enum InkColor
{
    Red,
    Blue,
    Yellow,
    AllColor
}