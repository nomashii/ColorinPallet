using UnityEngine;

public class InkManager : MonoBehaviour
{
    public static InkManager Instance { get; private set; }

    public System.Action OnInkChanged;

    [Header("現在のインク残量")]
    [SerializeField] private float redInk = 100f;
    [SerializeField] private float blueInk = 100f;
    [SerializeField] private float yellowInk = 100f;
    [SerializeField] private float maxInk = 100f;

    public float MaxInk => maxInk;

    private const string RED_KEY = "RedInk";
    private const string BLUE_KEY = "BlueInk";
    private const string YELLOW_KEY = "YellowInk";

    private void Awake()
    {
        Debug.Log("InkManager Awake");

        // すでに別のInkManagerが存在する場合
        if (Instance != null && Instance != this)
        {
            // Managersプレハブ全体の重複を削除
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;


        // 保存されているインク量を読み込む
        redInk = PlayerPrefs.GetFloat(RED_KEY, redInk);
        blueInk = PlayerPrefs.GetFloat(BLUE_KEY, blueInk);
        yellowInk = PlayerPrefs.GetFloat(YELLOW_KEY, yellowInk);

        // 保存値が範囲外だった場合に補正
        redInk = Mathf.Clamp(redInk, 0f, maxInk);
        blueInk = Mathf.Clamp(blueInk, 0f, maxInk);
        yellowInk = Mathf.Clamp(yellowInk, 0f, maxInk);
    }

    private void Start()
    {
        Debug.Log("赤インク：" + redInk);
        Debug.Log("青インク：" + blueInk);
        Debug.Log("黄インク：" + yellowInk);

        // ゲーム開始時の値をUIへ通知
        OnInkChanged?.Invoke();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void SaveInk()
    {
        PlayerPrefs.SetFloat(RED_KEY, redInk);
        PlayerPrefs.SetFloat(BLUE_KEY, blueInk);
        PlayerPrefs.SetFloat(YELLOW_KEY, yellowInk);
        PlayerPrefs.Save();
    }

    public float GetRedInk()
    {
        return redInk;
    }

    public float GetBlueInk()
    {
        return blueInk;
    }

    public float GetYellowInk()
    {
        return yellowInk;
    }

    public void AddInk(ColorType color, float amount)
    {
        switch (color)
        {
            case ColorType.Red:
                redInk += amount;
                break;

            case ColorType.Blue:
                blueInk += amount;
                break;

            case ColorType.Yellow:
                yellowInk += amount;
                break;

            case ColorType.AllColor:
                redInk += amount;
                blueInk += amount;
                yellowInk += amount;
                break;
        }

        redInk = Mathf.Clamp(redInk, 0f, maxInk);
        blueInk = Mathf.Clamp(blueInk, 0f, maxInk);
        yellowInk = Mathf.Clamp(yellowInk, 0f, maxInk);

        Debug.Log(
            "インク補充 赤:" + redInk +
            " 青:" + blueInk +
            " 黄:" + yellowInk
        );

        SaveInk();
        OnInkChanged?.Invoke();
    }

    public bool UseInk(ColorType color, float amount)
    {
        if (!CanUseColor(color, amount))
        {
            return false;
        }

        switch (color)
        {
            case ColorType.Red:
                redInk -= amount * 10f;
                break;

            case ColorType.Blue:
                blueInk -= amount * 10f;
                break;

            case ColorType.Yellow:
                yellowInk -= amount * 10f;
                break;

            case ColorType.Purple:
                redInk -= amount * 5f;
                blueInk -= amount * 5f;
                break;

            case ColorType.Green:
                blueInk -= amount * 5f;
                yellowInk -= amount * 5f;
                break;

            case ColorType.Orange:
                redInk -= amount * 5f;
                yellowInk -= amount * 5f;
                break;
        }

        redInk = Mathf.Clamp(redInk, 0f, maxInk);
        blueInk = Mathf.Clamp(blueInk, 0f, maxInk);
        yellowInk = Mathf.Clamp(yellowInk, 0f, maxInk);

        Debug.Log(
            "赤:" + redInk +
            " 青:" + blueInk +
            " 黄:" + yellowInk
        );

        SaveInk();
        OnInkChanged?.Invoke();

        return true;
    }

    public bool CanUseColor(ColorType color, float amount)
    {
        switch (color)
        {
            case ColorType.Red:
                return redInk >= amount * 10f;

            case ColorType.Blue:
                return blueInk >= amount * 10f;

            case ColorType.Yellow:
                return yellowInk >= amount * 10f;

            case ColorType.Purple:
                return redInk >= amount * 5f &&
                       blueInk >= amount * 5f;

            case ColorType.Green:
                return blueInk >= amount * 5f &&
                       yellowInk >= amount * 5f;

            case ColorType.Orange:
                return redInk >= amount * 5f &&
                       yellowInk >= amount * 5f;

            case ColorType.White:
                return true;

            default:
                return false;
        }
    }
}