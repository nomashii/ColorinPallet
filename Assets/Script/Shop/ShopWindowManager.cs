using UnityEngine;

public class ShopWindowManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    private void Start()
    {
        shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        AudioManager.Instance.PlayPopupSE();

        shopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        AudioManager.Instance.PlayImportantTapSE();

        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}