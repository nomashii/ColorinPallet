using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public System.Action OnMoneyChanged;

    private int currentMoney;

    private const string MONEY_KEY = "CurrentMoney";

    private static MoneyManager instance;
    public static MoneyManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        currentMoney = PlayerPrefs.GetInt(MONEY_KEY, 0);
    }

    /// <summary>
    /// お金を増やす
    /// </summary>
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        AudioManager.Instance.PlayMoneySE();
        SaveMoney();

        Debug.Log(amount + "G獲得");
        Debug.Log("現在所持金 : " + currentMoney);

        OnMoneyChanged?.Invoke();
    }

    /// <summary>
    /// お金を使う
    /// </summary>
    public bool UseMoney(int amount)
    {
        if (currentMoney < amount)
        {
            Debug.Log("所持金不足");
            return false;
        }

        currentMoney -= amount;

        SaveMoney();

        Debug.Log(amount + "G使用");
        Debug.Log("現在所持金 : " + currentMoney);

        OnMoneyChanged?.Invoke();

        return true;
    }

    /// <summary>
    /// 所持金取得
    /// </summary>
    public int GetMoney()
    {
        return currentMoney;
    }

    /// <summary>
    /// 所持金保存
    /// </summary>
    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MONEY_KEY, currentMoney);
        PlayerPrefs.Save();
    }
    public void ResetMoney()
{
    currentMoney = 0;

    SaveMoney();

    OnMoneyChanged?.Invoke();

    Debug.Log("所持金リセット");
}
}