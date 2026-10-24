using System.Collections;
using TMPro;
using UnityEngine;

public class MessageUIManager : MonoBehaviour
{
    public static MessageUIManager Instance;

    [SerializeField]
    private GameObject messageObject;

    [SerializeField]
    private TMP_Text messageText;

    [SerializeField]
    private float displayTime = 1.5f;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        // 同じシーンに複数存在する場合は、後から生成された方を削除
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning(
                $"MessageUIManagerが重複しています。" +
                $"削除対象：{gameObject.name}",
                this
            );

            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (messageObject == null)
        {
            Debug.LogError(
                $"{gameObject.name}：" +
                "Message Objectが設定されていません。",
                this
            );

            enabled = false;
            return;
        }

        if (messageText == null)
        {
            Debug.LogError(
                $"{gameObject.name}：" +
                "Message Textが設定されていません。",
                this
            );

            enabled = false;
            return;
        }

        // 親のMessageUIManagerではなく、
        // 子のErrorMessageCanvasだけを非表示にする
        messageObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void ShowMessage(string text)
    {
        if (!enabled ||
            messageObject == null ||
            messageText == null)
        {
            return;
        }

        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine =
            StartCoroutine(Show(text));
    }

    private IEnumerator Show(string text)
    {
        messageText.text = text;
        messageObject.SetActive(true);

        yield return new WaitForSecondsRealtime(
            displayTime
        );

        messageObject.SetActive(false);
        messageCoroutine = null;
    }
}