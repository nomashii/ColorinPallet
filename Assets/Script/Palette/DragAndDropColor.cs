using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropColor :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [SerializeField] private ColorType myColor;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 startPosition;

    public ColorType GetColorType()
    {
        return myColor;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
{
    startPosition = rectTransform.anchoredPosition;

    canvasGroup.blocksRaycasts = false;
    canvasGroup.alpha = 0.7f;

    transform.SetAsLastSibling();
}

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startPosition;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Debug.Log("ドラッグ終了：" + myColor);
    }
}
