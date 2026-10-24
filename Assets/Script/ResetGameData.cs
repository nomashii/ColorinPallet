using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameData : MonoBehaviour
{
    public void ResetData()
{
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();

    if (MoneyManager.Instance != null)
    {
        MoneyManager.Instance.ResetMoney();
    }

    Debug.Log("セーブデータを初期化しました！");

    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}