using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public static GameStartManager Instance;

    public bool IsOpening { get; private set; } = true;

    private void Awake()
    {
        Instance = this;
    }

    public void EndOpening()
    {
        IsOpening = false;

        Debug.Log("ゲーム開始演出終了！");
    }
}