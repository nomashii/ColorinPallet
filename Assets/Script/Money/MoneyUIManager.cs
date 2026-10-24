using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private MoneyManager moneyManager;

    private Vector3 originalScale;
    private Coroutine popCoroutine;

    private bool hasStarted;

    private void Awake()
    {
        if (moneyText == null)
        {
            Debug.LogError(
                $"{gameObject.name}ÅFMoney TextÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB"
            );
            return;
        }

        originalScale = moneyText.transform.localScale;
    }

    private void Start()
    {
        hasStarted = true;

        UpdateMoneyUIWithoutAnimation();
    }

    private void OnEnable()
    {
        if (moneyManager == null)
        {
            Debug.LogError(
                $"{gameObject.name}ÅFMoney ManagerÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB"
            );
            return;
        }

        // èdï°ìoò^ñhé~
        moneyManager.OnMoneyChanged -= UpdateMoneyUI;
        moneyManager.OnMoneyChanged += UpdateMoneyUI;

        // Startå„Ç…çƒï\é¶Ç≥ÇÍÇΩèÍçáÇæÇØÅAåªç›ílÇîΩâf
        if (hasStarted)
        {
            UpdateMoneyUIWithoutAnimation();
        }
    }

    private void OnDisable()
    {
        if (moneyManager != null)
        {
            moneyManager.OnMoneyChanged -= UpdateMoneyUI;
        }

        if (popCoroutine != null)
        {
            StopCoroutine(popCoroutine);
            popCoroutine = null;
        }

        if (moneyText != null)
        {
            moneyText.transform.localScale = originalScale;
        }
    }

    private void UpdateMoneyUI()
    {
        if (moneyText == null || moneyManager == null)
        {
            return;
        }

        moneyText.text = moneyManager.GetMoney() + "G";

        if (!isActiveAndEnabled ||
            !gameObject.activeInHierarchy ||
            !moneyText.gameObject.activeInHierarchy)
        {
            return;
        }

        if (popCoroutine != null)
        {
            StopCoroutine(popCoroutine);
        }

        popCoroutine = StartCoroutine(PopAnimation());
    }

    private void UpdateMoneyUIWithoutAnimation()
    {
        if (moneyText == null || moneyManager == null)
        {
            return;
        }

        moneyText.text = moneyManager.GetMoney() + "G";
        moneyText.transform.localScale = originalScale;
    }

    private IEnumerator PopAnimation()
    {
        Transform textTransform = moneyText.transform;

        textTransform.localScale =
            originalScale * 1.25f;

        yield return new WaitForSecondsRealtime(0.08f);

        textTransform.localScale =
            originalScale * 0.95f;

        yield return new WaitForSecondsRealtime(0.05f);

        textTransform.localScale = originalScale;

        popCoroutine = null;
    }
}